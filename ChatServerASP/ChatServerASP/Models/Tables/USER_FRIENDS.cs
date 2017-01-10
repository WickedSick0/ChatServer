using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class USER_FRIENDS
    {
        public int Id { get; set; }

        public int Id_Friend { get; set; }

        public int Id_Friendlist_Owner { get; set; }
    }
}