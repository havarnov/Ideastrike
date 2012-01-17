using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideastrike.Nancy.Models
{
    public class Idea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public User Author { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        //public virtual ICollection<VotesToUser> Votes { get; set; }
        
        public bool isValid()
        {
            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (Author == null) return false;
            return true;
        }

        public List<string> generateErrorList()
        {
            List<string> errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(Title))
            {
                errorList.Add("Title field is mandatory");
            }
            if (Author == null)
            {
                errorList.Add("Author field is mandatory");
            }
            return errorList;
        }
    }
}
