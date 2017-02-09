using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWindowsForms.Tables
{
    public class CreateChatroom
    {
        public string chatroomName { get; set; }
        public int[] ChatroomMembersID { get; set; }
        public int IDUser { get; set; }
        public string Token { get; set; }
    }
}
