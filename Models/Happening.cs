using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _250ml_MVC4_2.Models
{
    public class Happening
    {
        public int HappeningId { get; set; }
        [Required]
        [StringLength(60,MinimumLength=10)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Rating> Ratings { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile Owner { get; set; }


        public bool IsOwner(int UserId) {
            return (this.UserId == UserId);
        }

        public int AverageRating() {
            int RatingSum = 0;

            if (this.Ratings.Count > 0) {
                RatingSum = this.Ratings.Sum(rating => rating.Value);
                RatingSum /= this.Ratings.Count;
            }

            return RatingSum;
        }

        public bool HasRated( int UserId) {
            return ( this.Ratings.Where(m => m.UserId == UserId).ToList().Count != 0 );
        }
    }
}