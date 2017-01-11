using ChatServerASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServerASP.Controllers
{
    public class CHATROOMsController : ApiController
    {
        public ChatroomRepository rep = new ChatroomRepository();

        // GET: api/CHATROOMs
        public List<CHATROOM> Get()
        {
            return this.rep.FindAll();
        }

        // GET: api/CHATROOMs/5
        public CHATROOM Get(int id)
        {
            return this.rep.FindById(id);
        }

        // POST: api/CHATROOMs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CHATROOMs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CHATROOMs/5
        public void Delete(int id)
        {
            this.rep.DeleteChatroom(id);
        }
    }
}
