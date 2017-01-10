using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MESSAGE
    {
        public int ID { get; set; }

        public int ID_Chatroom { get; set; }

        public int ID_User_Post { get; set; }

        public string Message_text { get; set; }

    }
}