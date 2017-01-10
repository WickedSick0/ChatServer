﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServerASP.Models
{
    public class Message_reading_infoRepository
    {
        private MyContext _context = new MyContext();

        public List<MESSAGE_READING_INFO> FindAll()
        {
            return this._context.Message_reading_infos.ToList<MESSAGE_READING_INFO>();
        }

        public MESSAGE_READING_INFO FindById(int id)
        {
            return this._context.Message_reading_infos.Find(id);
        }

        public void InsertMessage_reading_info(MESSAGE_READING_INFO mr)
        {
            this._context.Message_reading_infos.Add(mr);
            this._context.SaveChanges();
        }

        /*
        public void UpdateMessage_reading_info(MESSAGE_READING_INFO mr)
        {
            MESSAGE_READING_INFO mrtemp = this.FindById(mr.ID);

            mrtemp.ID_Chatroom = mr.ID_Chatroom;
            mrtemp.ID_Message = mr.ID_Message;
            mrtemp.ID_Read_By_User = mr.ID_Read_By_User;

            this._context.SaveChanges();
        }
        */
        
        public void DeleteMessage_reading_info(int id)
        {
            MESSAGE_READING_INFO mr = this.FindById(id);
            this._context.Message_reading_infos.Remove(mr);
            this._context.SaveChanges();
        }
    }
}