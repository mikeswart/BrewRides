using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace CapitalBreweryBikeClub.Internal
{
    internal sealed class LocalJsonFile<T>
    {
        private readonly string localFilePath;

        public LocalJsonFile(IFileProvider rootFileProvider, string fileName)
        {
            localFilePath = rootFileProvider.GetFileInfo(fileName).PhysicalPath;
        }

        public async void Save(IEnumerable<T> items)
        {
            await File.WriteAllTextAsync(localFilePath, JsonConvert.SerializeObject(items));
        }

        public async Task<IEnumerable<T>> Load()
        {
            return JsonConvert.DeserializeObject<T[]>(await File.ReadAllTextAsync(localFilePath));
        }

        public bool CacheExists()
        {
            return File.Exists(localFilePath);
        }
    }
}