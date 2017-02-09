using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsForms.Tables
{
    public class PostRequest
    {
        public string token { get; set; }
        public int Id_Friendlist_Owner_sender { get; set; }
        public int Id_Friend_receiver { get; set; }
    }
}
