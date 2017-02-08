using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class ChatroomRepository
    {
        private MyContext _context = new MyContext();

        public List<CHATROOM> FindAll()
        {
            return this._context.Chatrooms.ToList<CHATROOM>();
        }

        public CHATROOM FindById(int id)
        {
            return this._context.Chatrooms.Find(id);
        }

        public CHATROOM FindByName(string name)
        {
            return this._context.Chatrooms.Where(x => x.Chatroom_Name == name).SingleOrDefault();
        }


        public void InsertChatroom(CHATROOM cr)
        {
            this._context.Chatrooms.Add(cr);
            this._context.SaveChanges();
        }

        public void UpdateChatroom(CHATROOM cr)
        {
            CHATROOM crtemp = this.FindById(cr.Id);

            crtemp.Chatroom_Name = cr.Chatroom_Name;

            this._context.SaveChanges();
        }

        public void DeleteChatroom(int id)
        {
            CHATROOM cr = this.FindById(id);
            this._context.Chatrooms.Remove(cr);
            this._context.SaveChanges();
        }
    }
}