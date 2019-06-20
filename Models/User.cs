using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secrets.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2)]

        [Display(Name = "First Name")]
        [Column(TypeName = "VARCHAR(45)")]
        public string FirstName { get; set; }
        [MinLength(2)]
        [Required]
        [Column(TypeName = "VARCHAR(45)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Column(TypeName = "VARCHAR(45)")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MinLength(8)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Properties (NEW!)


    }

    public class LoginUser
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string LogEmail { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LogPassword { get; set; }
    }
}