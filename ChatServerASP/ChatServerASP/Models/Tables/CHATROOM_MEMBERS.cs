﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class CHATROOM_MEMBERS
    {
        public int Id { get; set; }

        public int Id_Chatroom { get; set; }

        public int Id_User { get; set; }
    }
}