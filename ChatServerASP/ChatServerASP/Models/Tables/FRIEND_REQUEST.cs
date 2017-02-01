using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models.Tables
{
    public class FRIEND_REQUEST
    {
        public int Id { get; set; }
        public int Id_Friendlist_Owner_sender { get; set; }
        public int Id_Friend_receiver { get; set; }
        public DateTime Send_Time { get; set; }
        public bool Accepted { get; set; }
    }
}