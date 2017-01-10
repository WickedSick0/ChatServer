using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class USER_FRIENDS
    {
        public int ID { get; set; }

        public int ID_Friend { get; set; }

        public int ID_Friendlist_Owner { get; set; }
    }
}