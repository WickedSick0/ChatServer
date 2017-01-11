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

namespace ChatServerASP.Controllers
{
    public class CHATROOMsController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/CHATROOMs
        public IQueryable<CHATROOM> GetChatrooms()
        {
            return db.Chatrooms;
        }

        // GET: api/CHATROOMs/5
        [ResponseType(typeof(CHATROOM))]
        public async Task<IHttpActionResult> GetCHATROOM(int id)
        {
            CHATROOM cHATROOM = await db.Chatrooms.FindAsync(id);
            if (cHATROOM == null)
            {
                return NotFound();
            }

            return Ok(cHATROOM);
        }

        // PUT: api/CHATROOMs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCHATROOM(int id, CHATROOM cHATROOM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cHATROOM.Id)
            {
                return BadRequest();
            }

            db.Entry(cHATROOM).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CHATROOMExists(id))
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

        // POST: api/CHATROOMs
        [ResponseType(typeof(CHATROOM))]
        public async Task<IHttpActionResult> PostCHATROOM(CHATROOM cHATROOM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chatrooms.Add(cHATROOM);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cHATROOM.Id }, cHATROOM);
        }

        // DELETE: api/CHATROOMs/5
        [ResponseType(typeof(CHATROOM))]
        public async Task<IHttpActionResult> DeleteCHATROOM(int id)
        {
            CHATROOM cHATROOM = await db.Chatrooms.FindAsync(id);
            if (cHATROOM == null)
            {
                return NotFound();
            }

            db.Chatrooms.Remove(cHATROOM);
            await db.SaveChangesAsync();

            return Ok(cHATROOM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHATROOMExists(int id)
        {
            return db.Chatrooms.Count(e => e.Id == id) > 0;
        }
    }
}