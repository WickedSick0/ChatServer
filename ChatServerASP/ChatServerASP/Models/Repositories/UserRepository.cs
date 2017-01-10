using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class UserRepository
    {
        private MyContext _context = new MyContext();

        public List<USER> FindAll()
        {
            return this._context.Users.ToList<USER>();
        }

        public USER FindById(int id)
        {
            return this._context.Users.Find(id);
        }

        public void InsertUser(USER u)
        {
            this._context.Users.Add(u);
            this._context.SaveChanges();
        }

        public void UpdateUser(USER u)
        {
            USER utemp = this.FindById(u.ID);

            utemp.Login = u.Login;
            utemp.Nick = u.Nick;
            utemp.Password = u.Password;
            utemp.Photo = u.Photo;

            this._context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            USER u = this.FindById(id);
            this._context.Users.Remove(u);
            this._context.SaveChanges();
        }
    }
}