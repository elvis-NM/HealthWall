
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace secrets.Models
{
    public class SecretDashboard
    {
        public Secret NewSecret { get; set; }
        public List<Secret> RecentSecrets { get; set; }
        public User CurrentUser { get; set; }


    }
}