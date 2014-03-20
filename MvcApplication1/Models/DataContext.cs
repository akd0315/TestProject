using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class LinkedInExample : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}