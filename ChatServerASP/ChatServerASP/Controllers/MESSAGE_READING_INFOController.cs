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
    public class MESSAGE_READING_INFOController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/MESSAGE_READING_INFO
        public IQueryable<MESSAGE_READING_INFO> GetMessage_reading_infos()
        {
            return db.Message_reading_infos;
        }

        // GET: api/MESSAGE_READING_INFO/5
        [ResponseType(typeof(MESSAGE_READING_INFO))]
        public async Task<IHttpActionResult> GetMESSAGE_READING_INFO(int id)
        {
            MESSAGE_READING_INFO mESSAGE_READING_INFO = await db.Message_reading_infos.FindAsync(id);
            if (mESSAGE_READING_INFO == null)
            {
                return NotFound();
            }

            return Ok(mESSAGE_READING_INFO);
        }

        // PUT: api/MESSAGE_READING_INFO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMESSAGE_READING_INFO(int id, MESSAGE_READING_INFO mESSAGE_READING_INFO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mESSAGE_READING_INFO.Id)
            {
                return BadRequest();
            }

            db.Entry(mESSAGE_READING_INFO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MESSAGE_READING_INFOExists(id))
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

        // POST: api/MESSAGE_READING_INFO
        [ResponseType(typeof(MESSAGE_READING_INFO))]
        public async Task<IHttpActionResult> PostMESSAGE_READING_INFO(MESSAGE_READING_INFO mESSAGE_READING_INFO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Message_reading_infos.Add(mESSAGE_READING_INFO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mESSAGE_READING_INFO.Id }, mESSAGE_READING_INFO);
        }

        // DELETE: api/MESSAGE_READING_INFO/5
        [ResponseType(typeof(MESSAGE_READING_INFO))]
        public async Task<IHttpActionResult> DeleteMESSAGE_READING_INFO(int id)
        {
            MESSAGE_READING_INFO mESSAGE_READING_INFO = await db.Message_reading_infos.FindAsync(id);
            if (mESSAGE_READING_INFO == null)
            {
                return NotFound();
            }

            db.Message_reading_infos.Remove(mESSAGE_READING_INFO);
            await db.SaveChangesAsync();

            return Ok(mESSAGE_READING_INFO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MESSAGE_READING_INFOExists(int id)
        {
            return db.Message_reading_infos.Count(e => e.Id == id) > 0;
        }
    }
}