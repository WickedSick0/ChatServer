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
using ChatServerASP.Models.Tables;
using ChatServerASP.Models;

namespace ChatServerASP.Controllers
{
    public class FRIEND_REQUESTController : ApiController
    {
        private MyContext db = new MyContext();

        // GET api/FRIEND_REQUEST
        public IQueryable<FRIEND_REQUEST> GetFriend_Requests()
        {
            return db.Friend_Requests;
        }

        // GET api/FRIEND_REQUEST/5
        [ResponseType(typeof(FRIEND_REQUEST))]
        public async Task<IHttpActionResult> GetFRIEND_REQUEST(int id)
        {
            FRIEND_REQUEST friend_request = await db.Friend_Requests.FindAsync(id);
            if (friend_request == null)
            {
                return NotFound();
            }

            return Ok(friend_request);
        }

        // PUT api/FRIEND_REQUEST/5
        public async Task<IHttpActionResult> PutFRIEND_REQUEST(int id, FRIEND_REQUEST friend_request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friend_request.Id)
            {
                return BadRequest();
            }

            db.Entry(friend_request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FRIEND_REQUESTExists(id))
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

        // POST api/FRIEND_REQUEST
        [ResponseType(typeof(FRIEND_REQUEST))]
        public async Task<IHttpActionResult> PostFRIEND_REQUEST(FRIEND_REQUEST friend_request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friend_Requests.Add(friend_request);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = friend_request.Id }, friend_request);
        }

        // DELETE api/FRIEND_REQUEST/5
        [ResponseType(typeof(FRIEND_REQUEST))]
        public async Task<IHttpActionResult> DeleteFRIEND_REQUEST(int id)
        {
            FRIEND_REQUEST friend_request = await db.Friend_Requests.FindAsync(id);
            if (friend_request == null)
            {
                return NotFound();
            }

            db.Friend_Requests.Remove(friend_request);
            await db.SaveChangesAsync();

            return Ok(friend_request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FRIEND_REQUESTExists(int id)
        {
            return db.Friend_Requests.Count(e => e.Id == id) > 0;
        }
    }
}