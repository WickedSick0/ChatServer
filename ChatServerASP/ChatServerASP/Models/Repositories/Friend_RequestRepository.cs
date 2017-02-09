using ChatServerASP.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models.Repositories
{
    public class Friend_RequestRepository
    {
        private MyContext _context = new MyContext();

        public List<FRIEND_REQUEST> FindAll()
        {
            return this._context.Friend_Requests.ToList<FRIEND_REQUEST>();
        }

        public FRIEND_REQUEST FindById(int id)
        {
            return this._context.Friend_Requests.Find(id);
        }

        public void InsertMessage_reading_info(FRIEND_REQUEST fr)
        {
            this._context.Friend_Requests.Add(fr);
            this._context.SaveChanges();
        }

        public FRIEND_REQUEST FindByOwnerSenderId(int idOwner)
        {
            return this._context.Friend_Requests.Where(x => x.Id_Friendlist_Owner_sender == idOwner).FirstOrDefault();
        }

        public bool duplicityfriend(int idowner, int idfriend)
        {
            if (_context.Friend_Requests.Where(x => x.Id_Friendlist_Owner_sender == idowner && x.Id_Friend_receiver == idfriend).FirstOrDefault() == null) return false;

            return true;
        }

        public void UpdateFRIEND_REQUEST(FRIEND_REQUEST fr)
        {
            FRIEND_REQUEST frtemp = this.FindById(fr.Id);

            frtemp.Id_Friend_receiver = fr.Id_Friend_receiver;
            frtemp.Id_Friendlist_Owner_sender = fr.Id_Friendlist_Owner_sender;
            frtemp.Send_Time = fr.Send_Time;
            frtemp.Accepted = fr.Accepted;

            this._context.SaveChanges();
        }

        public void DeleteFRIEND_REQUEST(int id)
        {
            FRIEND_REQUEST fr = this.FindById(id);
            this._context.Friend_Requests.Remove(fr);
            this._context.SaveChanges();
        }
    }
}