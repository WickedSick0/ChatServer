using ChatServerASP.Models;
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
        public UserRepository _rep = new UserRepository();

        // GET: api/USERs
        public List<USER> Get()
        {          
            return _rep.FindAll();
        }

        // GET: api/USERs/5
        public USER Get(int id)
        {
            return this._rep.FindById(id);
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
