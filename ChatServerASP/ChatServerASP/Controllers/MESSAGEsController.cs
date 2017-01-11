using ChatServerASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServerASP.Controllers
{
    public class MESSAGEsController : ApiController
    {
        public MessageRepository rep = new MessageRepository();

        // GET: api/MESSAGEs
        public List<MESSAGE> Get()
        {
            return this.rep.FindAll();
        }

        // GET: api/MESSAGEs/5
        public MESSAGE Get(int id)
        {
            return this.rep.FindById(id);
        }

        // POST: api/MESSAGEs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MESSAGEs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MESSAGEs/5
        public void Delete(int id)
        {
            this.rep.DeleteMessage(id);
        }
    }
}
