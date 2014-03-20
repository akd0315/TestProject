using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class AccessToken
    {
        public string expires_in { get; set; }
        public string access_token { get; set; }
    }
}