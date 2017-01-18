using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models.Tables
{
    public class USER_TOKENS
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string Token { get; set; }
    }
}