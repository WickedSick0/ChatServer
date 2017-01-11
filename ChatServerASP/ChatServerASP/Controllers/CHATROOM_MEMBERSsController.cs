using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServerASP.Controllers
{
    public class CHATROOM_MEMBERSsController : ApiController
    {
        // GET: api/CHATROOM_MEMBERSs
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CHATROOM_MEMBERSs/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CHATROOM_MEMBERSs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CHATROOM_MEMBERSs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CHATROOM_MEMBERSs/5
        public void Delete(int id)
        {
        }
    }
}
