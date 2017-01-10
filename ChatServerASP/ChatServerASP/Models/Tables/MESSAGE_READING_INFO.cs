using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MESSAGE_READING_INFO
    {
        public int Id { get; set; }

        public int Id_Chatroom { get; set; }

        public int Id_Message { get; set; }

        public int Id_Read_By_User { get; set; }
    }
}