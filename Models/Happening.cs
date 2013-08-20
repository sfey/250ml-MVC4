using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _250ml_MVC4_2.Models
{
    public class Happening
    {
        public int HappeningId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Rating> Ratings { get; set; }
    }
}