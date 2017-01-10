using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class User_friendsRepository
    {
        private MyContext _context = new MyContext();

        public List<USER_FRIENDS> FindAll()
        {
            return this._context.User_friends.ToList<USER_FRIENDS>();
        }

        public USER_FRIENDS FindById(int id)
        {
            return this._context.User_friends.Find(id);
        }

        public void InsertUser_friends(USER_FRIENDS uf)
        {
            this._context.User_friends.Add(uf);
            this._context.SaveChanges();
        }

        public void UpdateUser_friends(USER_FRIENDS uf)
        {
            USER_FRIENDS uftemp = this.FindById(uf.Id);

            uftemp.Id_Friend = uf.Id_Friend;
            uftemp.Id_Friendlist_Owner = uf.Id_Friendlist_Owner;

            this._context.SaveChanges();
        }

        public void DeleteUser_friends(int id)
        {
            USER_FRIENDS uf = this.FindById(id);
            this._context.User_friends.Remove(uf);
            this._context.SaveChanges();
        }
    }
}