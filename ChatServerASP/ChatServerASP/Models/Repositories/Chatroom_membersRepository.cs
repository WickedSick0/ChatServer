using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class Chatroom_membersRepository
    {
        private MyContext _context = new MyContext();

        public List<CHATROOM_MEMBERS> FindAll()
        {
            return this._context.Chatroom_members.ToList<CHATROOM_MEMBERS>();
        }

        public List<CHATROOM> FindChatroomByUser(int id_User)
        {
            ChatroomRepository rep = new ChatroomRepository();
            List<CHATROOM> chatrooms = new List<CHATROOM>();
            List<int> temp = new List<int>();

            foreach (CHATROOM_MEMBERS item in this.FindAll())
            {
                if (item.Id_User == id_User)
                    temp.Add(item.Id_Chatroom);
            }

            foreach (int item in temp)
                chatrooms.Add(rep.FindById(item));

            return chatrooms;
        }
        public CHATROOM FindFriendChatroom(int id_user, int id_friend)
        {
            ChatroomRepository rep = new ChatroomRepository();
            CHATROOM ch = new CHATROOM();

            string values = id_user + "," + id_friend;

            List<CHATROOM_MEMBERS> bothinchatrooms =
            _context.Chatroom_members.SqlQuery("SELECT * FROM `CHATROOM_MEMBERS` WHERE `Id_Chatroom` IN ( SELECT `Id_Chatroom` FROM `CHATROOM_MEMBERS` where `Id_User` in(" + values + ") GROUP BY `Id_Chatroom` HAVING COUNT(`Id_Chatroom`) > 1 ) and `Id_User` in(" + values + ")").ToList();

            foreach (var item in bothinchatrooms)
            {
                if (_context.Chatroom_members.Where(x => x.Id_Chatroom == item.Id_Chatroom).Count() == 2)
                {
                    ch = rep.FindById(item.Id_Chatroom);
                    break;
                }
            }


            return ch;
        }


        public CHATROOM_MEMBERS FindById(int id)
        {
            return this._context.Chatroom_members.Find(id);
        }

        public void InsertChatroom_members(CHATROOM_MEMBERS cm)
        {
            this._context.Chatroom_members.Add(cm);
            this._context.SaveChanges();
        }

        public void UpdateChatroom_members(CHATROOM_MEMBERS cm)
        {
            CHATROOM_MEMBERS cmtemp = this.FindById(cm.Id);

            cmtemp.Id_Chatroom = cm.Id_Chatroom;
            cmtemp.Id_User = cm.Id_User;

            this._context.SaveChanges();
        }

        public void DeleteChatroom_members(int id)
        {
            CHATROOM_MEMBERS cm = this.FindById(id);
            this._context.Chatroom_members.Remove(cm);
            this._context.SaveChanges();
        }
        public bool CheckChatroomMembership(int id_chatroom, string token) // kontroluje zdali je uzivatel co vykonava(neco[POST,GET atd.]) v chatroomu
        {
            CHATROOM_MEMBERS chmember = _context.Chatroom_members.Where(x => x.Id_Chatroom == id_chatroom && x.Id_User == _context.User_tokens.Where(y => y.Token == token).FirstOrDefault().Id_User).FirstOrDefault();
            if (chmember == null)
            {
                return false;
            }
            return true;
        }
    }
}