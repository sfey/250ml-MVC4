﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace _250ml_MVC4_2.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Happening> Happenings { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Happening> Happenings { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuelles Kennwort")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "&quote{0}&quote muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Neues Kennwort bestätigen")]
        [Compare("NewPassword", ErrorMessage = "Das neue Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [Display(Name = "Speichern?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "&quote{0}&quote muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Kennwort bestätigen")]
        [Compare("Password", ErrorMessage = "Das Kennwort entspricht nicht dem Bestätigungskennwort.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
