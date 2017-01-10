using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class USER
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Nick { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }
    }
}