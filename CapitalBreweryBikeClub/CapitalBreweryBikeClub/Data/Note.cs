using System;
using System.ComponentModel.DataAnnotations;

namespace CapitalBreweryBikeClub.Model
{
    public class Note
    {
        [Key]
        public int Id
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
        public virtual MemberInformation CreatedBy
        {
            get; set;
        }
    }
}
