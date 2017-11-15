using System;
using System.Collections.Generic;
using System.Text;

namespace COmpStore.Models.ViewModels
{
    public class SessionAuth
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
