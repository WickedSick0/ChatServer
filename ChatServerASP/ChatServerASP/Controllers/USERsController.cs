using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatServerASP.Controllers
{
    public class USERsController : ApiController
    {
        // GET: api/USERs
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/USERs/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/USERs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/USERs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/USERs/5
        public void Delete(int id)
        {
        }
    }
}
