using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MESSAGE_READING_INFO
    {
        public int ID_Chatroom { get; set; }

        public int ID_Message { get; set; }

        public int ID_Read_By_User { get; set; }
    }
}