using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ChatServerASP.Models;
using ChatServerASP.Models.Repositories;
using ChatServerASP.Models.Tables;

namespace ChatServerASP.Controllers
{
    public class USERsController : ApiController
    {
        private MyContext db = new MyContext();
        private User_tokensRepository rep = new User_tokensRepository();

        // GET: api/USERs
        /*public List<USER> GetUsers()
        {
            List<USER> users = db.Users.ToList();

            foreach (USER item in users)
            {
                item.Password = null;
            }

            return users;
        }*/

        // GET: api/USERs/5?token=fdafgfsfs
        [ResponseType(typeof(USER))]
        [Route("api/USERs/{id}")]
        public async Task<IHttpActionResult> GetUSER(int id, string token)
        {
            User_tokensRepository rep = new User_tokensRepository();
            if (rep.CheckToken(token,id) == false)
            {
                return BadRequest("Token is not valid! Please log in again!");
            }


            USER u = await db.Users.FindAsync(id);

            return Ok(u);
            /*
            User_tokensRepository rep = new User_tokensRepository();
            USER uSER = null;

            foreach (USER_TOKENS item in rep.FindAll())
            {
                if (item.Token == token && item.Id_User == id)
                {
                    uSER = await db.Users.FindAsync(id);
                }
            }

            //USER uSER = await db.Users.FindAsync(id);

            if (uSER == null)
            {
                return NotFound();
            }            

            return Ok(uSER);*/
        }

        [ResponseType(typeof(List<USER>))]
        [HttpGet]
        [Route("api/USERsearch/{search}")]
        public async Task<IHttpActionResult> FindforADD(string search ,string token, int id) // search user
        {
            if (rep.CheckToken(token, id) == false)
            {
                return BadRequest("Token is not valid! Please log in again!");
            }
            string sqlquerystring = string.Format("SELECT * FROM `USER` WHERE `Login` LIKE '%{0}%' or `Nick` LIKE '%{0}%'", search);
            List<USER> ul = db.Users.SqlQuery(sqlquerystring).ToList();
            foreach (var item in ul)
            {
                item.Password = null;
            }
            return Ok(ul);
        }
        public class GetUsersFromRequest
        {
            public int[] id_users { get; set; }
            public string token { get; set; }
            public int ID_User { get; set; }
        }


        [ResponseType(typeof(List<USER>))]
        [HttpPost]
        [Route("api/USERsrequesting")]
        public async Task<IHttpActionResult> FindforRequest(/*[FromUri]int[] id, string token, int id_user*/ GetUsersFromRequest usersrequesting) // search user from request
        {
            if (rep.CheckToken(usersrequesting.token, usersrequesting.ID_User) == false)
            {
                return BadRequest("Token is not valid! Please log in again!");
            }
            List<USER> ul = new List<USER>();
            USER u = new USER();
            foreach (var item in usersrequesting.id_users)
            {
                u = db.Users.Find(item);
                u.Password = null;
                ul.Add(u);
            }
            return Ok(ul);
        }


        // PUT: api/USERs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSER(int id, USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSER.Id)
            {
                return BadRequest();
            }

            db.Entry(uSER).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USERExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/USERs
        [ResponseType(typeof(USER))]
       // [Route("api/USERs/Register")]
        public async Task<IHttpActionResult> PostUSER(USER uSER)
        {
            UserRepository uRep = new UserRepository();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (uSER.Login == "" || uSER.Password == "" || uSER.Login == "" && uSER.Password == ""|| uSER.Login == null || uSER.Password == null || uSER.Login == null && uSER.Password == null || uSER.Login.Contains(" ") || uSER.Password.Contains(" "))
                return BadRequest();

            if (uSER.Photo == null) uSER.Photo = @"/Content/Photos/default-avatar.jpg";

            foreach (USER item in uRep.FindAll())
            {
                if (item.Login == uSER.Login)
                    return BadRequest();
            }

            db.Users.Add(uSER);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSER.Id }, uSER);
        }

        // DELETE: api/USERs/5
        [ResponseType(typeof(USER))]
        public async Task<IHttpActionResult> DeleteUSER(int id)
        {
            USER uSER = await db.Users.FindAsync(id);
            if (uSER == null)
            {
                return NotFound();
            }

            db.Users.Remove(uSER);
            await db.SaveChangesAsync();

            return Ok(uSER);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}