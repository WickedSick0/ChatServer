using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class MessageRepository
    {
        private MyContext _context = new MyContext();

        public List<MESSAGE> FindAll()
        {
            return this._context.Messages.ToList<MESSAGE>();
        }

        public MESSAGE FindById(int id)
        {
            return this._context.Messages.Find(id);
        }

        public void InsertMessage(MESSAGE m)
        {
            this._context.Messages.Add(m);
            this._context.SaveChanges();
        }

        public void UpdateMessage(MESSAGE m)
        {
            MESSAGE mtemp = this.FindById(m.Id);

            mtemp.Id_Chatroom = m.Id_Chatroom;
            mtemp.Id_User_Post = m.Id_User_Post;
            mtemp.Message_text = m.Message_text;

            this._context.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            MESSAGE m = this.FindById(id);
            this._context.Messages.Remove(m);
            this._context.SaveChanges();
        }
    }
}