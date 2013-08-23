using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _250ml_MVC4_2.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        [StringLength(300,MinimumLength=3)]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public int HappeningId { get; set; }
        public virtual Happening Happening { get; set; }

        public bool IsOwner(int UserId)
        {
            return (this.UserId == UserId);
        }
    }
}