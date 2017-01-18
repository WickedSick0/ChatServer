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

        // GET: api/USERs
        public IQueryable<USER> GetUsers()
        {
            return db.Users;
        }

        // GET: api/USERs/5?token=fdafgfsfs
        [ResponseType(typeof(USER))]
        public async Task<IHttpActionResult> GetUSER(int id, string token)
        {
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

            return Ok(uSER);
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
        public async Task<IHttpActionResult> PostUSER(USER uSER)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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