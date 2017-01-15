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
    public class CHATROOM_MEMBERSController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/CHATROOM_MEMBERS
        public IQueryable<CHATROOM_MEMBERS> GetChatroom_members()
        {
            return db.Chatroom_members;
        }

        // GET: api/CHATROOM_MEMBERS/5
        [ResponseType(typeof(CHATROOM_MEMBERS))]
        public async Task<List<CHATROOM>> GetCHATROOM_MEMBERS(int id)
        {
            Chatroom_membersRepository rep = new Chatroom_membersRepository();

            return rep.FindChatroomByUser(id).ToList();

            //original
            /*
            CHATROOM_MEMBERS cHATROOM_MEMBERS = await db.Chatroom_members.FindAsync(id);
            if (cHATROOM_MEMBERS == null)
            {
                return NotFound();
            }

            return Ok(cHATROOM_MEMBERS);
            */
        }

        // PUT: api/CHATROOM_MEMBERS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCHATROOM_MEMBERS(int id, CHATROOM_MEMBERS cHATROOM_MEMBERS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cHATROOM_MEMBERS.Id)
            {
                return BadRequest();
            }

            db.Entry(cHATROOM_MEMBERS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CHATROOM_MEMBERSExists(id))
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

        // POST: api/CHATROOM_MEMBERS
        [ResponseType(typeof(CHATROOM_MEMBERS))]
        public async Task<IHttpActionResult> PostCHATROOM_MEMBERS(CHATROOM_MEMBERS cHATROOM_MEMBERS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Chatroom_members.Add(cHATROOM_MEMBERS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cHATROOM_MEMBERS.Id }, cHATROOM_MEMBERS);
        }

        // DELETE: api/CHATROOM_MEMBERS/5
        [ResponseType(typeof(CHATROOM_MEMBERS))]
        public async Task<IHttpActionResult> DeleteCHATROOM_MEMBERS(int id)
        {
            CHATROOM_MEMBERS cHATROOM_MEMBERS = await db.Chatroom_members.FindAsync(id);
            if (cHATROOM_MEMBERS == null)
            {
                return NotFound();
            }

            db.Chatroom_members.Remove(cHATROOM_MEMBERS);
            await db.SaveChangesAsync();

            return Ok(cHATROOM_MEMBERS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHATROOM_MEMBERSExists(int id)
        {
            return db.Chatroom_members.Count(e => e.Id == id) > 0;
        }
    }
}