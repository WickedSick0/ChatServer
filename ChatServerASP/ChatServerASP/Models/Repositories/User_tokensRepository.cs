using ChatServerASP.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models.Repositories
{
    public class User_tokensRepository
    {
        private MyContext _context = new MyContext();

        public List<USER_TOKENS> FindAll()
        {
            return this._context.User_tokens.ToList<USER_TOKENS>();
        }
        public USER_TOKENS FindById(int id)
        {
            return this._context.User_tokens.Find(id);
        }
        public void InsertUser_tokens(USER_TOKENS ut)
        {
            this._context.User_tokens.Add(ut);
            this._context.SaveChanges();
        }
        public void UpdateUser_tokens(USER_TOKENS ut)
        {
            USER_TOKENS uttemp = this.FindById(ut.Id);

            uttemp.Id_User = ut.Id_User;
            uttemp.Token = ut.Token;

            this._context.SaveChanges();
        }
        public void DeleteUser_tokens(int id)
        {
            USER_TOKENS ut = this.FindById(id);
            this._context.User_tokens.Remove(ut);
            this._context.SaveChanges();
        }


    }
}