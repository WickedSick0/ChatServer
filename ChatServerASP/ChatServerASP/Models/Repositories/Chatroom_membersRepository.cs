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

        public CHATROOM_MEMBERS FindById(int id)
        {
            return this._context.Chatroom_members.Find(id);
        }

        public void InsertChatroom_members(CHATROOM_MEMBERS cm)
        {
            this._context.Chatroom_members.Add(cm);
            this._context.SaveChanges();
        }

        /*
        public void UpdateChatroom_members(CHATROOM_MEMBERS cm)
        {
            CHATROOM_MEMBERS cmtemp = this.FindById(cm.ID);

            cmtemp.ID_Chatroom = cm.ID_Chatroom;
            cmtemp.ID_User = cm.ID_User;

            this._context.SaveChanges();
        }
        */
        public void DeleteChatroom_members(int id)
        {
            CHATROOM_MEMBERS cm = this.FindById(id);
            this._context.Chatroom_members.Remove(cm);
            this._context.SaveChanges();
        }
    }
}