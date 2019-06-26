using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secrets.Models
{
    [Table("secrets")]
    public class Secret
    {

        [Key]
        public int SecretId { get; set; }

        public int UserId { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Suggestion must be 10 or more characters")]
        [MaxLength(200, ErrorMessage = "Suggestion must be less than 200 characters")]
        [Display(Name = "New Suggestion")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Navigation property
        public User Creator { get; set; }

        public List<Like> Likes { get; set; }


    }
}