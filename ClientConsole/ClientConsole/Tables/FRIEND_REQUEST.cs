using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class FRIEND_REQUEST
    {
        public int Id { get; set; }
        public int Id_Friendlist_Owner_sender { get; set; }
        public int Id_Friend_receiver { get; set; }
        public DateTime Send_Time { get; set; }
        public Nullable<bool> Accepted { get; set; }
    }
}
