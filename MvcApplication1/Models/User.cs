using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string Email { get; set; }
        public long LinkedInUId { get; set; }


       
    }
}