using System;
using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public sealed class Note
    {
        [Key]
        public int ID
        {
            get; set;
        }

        [Required()]
        public string Text
        {
            get; set;
        }

        [Required()]
        public DateTime Created
        {
            get; set;
        }

        [Required()]
        public MemberInformation CreatedBy
        {
            get; set;
        }
    }
}
