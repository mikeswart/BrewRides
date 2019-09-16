using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalBreweryBikeClub.Internal
{
    public sealed class NotesService
    {
        public Note CurrentNote
        {
            get;
            private set;
        }

        private readonly IServiceScopeFactory serviceScopeFactory;

        public NotesService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;

            using var _ = serviceScopeFactory.CreateDatabaseContextScope(out RouteDatabaseContext databaseContext);
            CurrentNote = GetCurrentNote(databaseContext);
        }

        public void ClearCurrentNote()
        {
            CurrentNote = null;
        }

        public void AddNote(Note note)
        {
            using var _ = serviceScopeFactory.CreateDatabaseContextScope(out RouteDatabaseContext databaseContext);

            databaseContext.Notes.Add(note);
            databaseContext.SaveChanges();

            CurrentNote = databaseContext.Notes.OrderByDescending(note => note.ID).FirstOrDefault();;
        }

        private Note GetCurrentNote(RouteDatabaseContext databaseContext)
        {
            return databaseContext.Notes.OrderByDescending(note => note.ID).FirstOrDefault();
        }
    }
}
