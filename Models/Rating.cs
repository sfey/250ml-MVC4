using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _250ml_MVC4_2.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int Value { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int HappeningId { get; set; }
        public virtual Happening Happening { get; set; }
    }
}