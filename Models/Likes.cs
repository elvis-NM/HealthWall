using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secrets.Models
{
    [Table("likes")]
    public class Like
    {
        [Key]
        public int LikedId { get; set; }
        public int UserId { get; set; }
        public int SecretId { get; set; }
        //Navigation Properties

        public User Liker { get; set; }
        public Secret LikedSecret { get; set; }
    }
}
