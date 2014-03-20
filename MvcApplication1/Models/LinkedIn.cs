using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class LinkedIn
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string State { get; set; }
        public AccessToken AccessToken { get; set; }

        public LinkedIn Initiate()
        {
            this.ConsumerKey = "";
            this.ConsumerSecret = "";
            this.State = "";
            return this;

        }
    }
}