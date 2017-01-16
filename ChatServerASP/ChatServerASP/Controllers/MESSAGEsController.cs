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
    public class MESSAGEsController : ApiController
    {
        private MyContext db = new MyContext();
        //[Authorize]
        // GET: api/MESSAGEs
        public IQueryable<MESSAGE> GetMessages()
        {
            return db.Messages;
        }

        // GET: api/MESSAGEs/5
        [ResponseType(typeof(MESSAGE))]
        public async Task<IHttpActionResult> GetMESSAGE(int id)
        {
            MESSAGE mESSAGE = await db.Messages.FindAsync(id);
            if (mESSAGE == null)
            {
                return NotFound();
            }

            return Ok(mESSAGE);
        }

        // PUT: api/MESSAGEs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMESSAGE(int id, MESSAGE mESSAGE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mESSAGE.Id)
            {
                return BadRequest();
            }

            db.Entry(mESSAGE).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MESSAGEExists(id))
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

        // POST: api/MESSAGEs
        [ResponseType(typeof(MESSAGE))]
        public async Task<IHttpActionResult> PostMESSAGE(MESSAGE mESSAGE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(mESSAGE);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mESSAGE.Id }, mESSAGE);
        }

        // DELETE: api/MESSAGEs/5
        [ResponseType(typeof(MESSAGE))]
        public async Task<IHttpActionResult> DeleteMESSAGE(int id)
        {
            MESSAGE mESSAGE = await db.Messages.FindAsync(id);
            if (mESSAGE == null)
            {
                return NotFound();
            }

            db.Messages.Remove(mESSAGE);
            await db.SaveChangesAsync();

            return Ok(mESSAGE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MESSAGEExists(int id)
        {
            return db.Messages.Count(e => e.Id == id) > 0;
        }
    }
}