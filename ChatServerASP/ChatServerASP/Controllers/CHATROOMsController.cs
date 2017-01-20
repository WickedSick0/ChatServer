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
using ChatServerASP.Models.Tables;

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
        [ResponseType(typeof(List<CHATROOM>))]
        public async Task<List<CHATROOM>> GetCHATROOM(int id, string token)
        {

            List<USER_TOKENS> Uts = db.User_tokens.Where(x => x.Id_User == id).ToList();
            foreach (var item in Uts)
            {
                if (item.Token == token)
                {
                    Chatroom_membersRepository rep = new Chatroom_membersRepository();

                    return rep.FindChatroomByUser(id).ToList();
                }
            }
            var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
            throw new HttpResponseException(response);

            //original
            /*CHATROOM cHATROOM = await db.Chatrooms.FindAsync(id);
            if (cHATROOM == null)
            {
                return NotFound();
            }

            return Ok(cHATROOM);*/
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

            //db.Chatroom_members.RemoveRange(db.Chatroom_members.Where(x => x.Id_Chatroom == id));
            List<CHATROOM_MEMBERS> chrMembers = db.Chatroom_members.Where(x => x.Id_Chatroom == id).ToList<CHATROOM_MEMBERS>();
            //db.Chatroom_members.RemoveRange(chrMembers);
            

            Chatroom_membersRepository chmR = new Chatroom_membersRepository();
            foreach (var item in chrMembers)
            {
                chmR.DeleteChatroom_members(item.Id);
            }

            List<MESSAGE_READING_INFO> msgRDinfo = db.Message_reading_infos.Where(x => x.Id_Chatroom == id).ToList<MESSAGE_READING_INFO>();

            Message_reading_infoRepository mreadINFOrepository = new Message_reading_infoRepository();
            foreach (var item in msgRDinfo)
            {
                mreadINFOrepository.DeleteMessage_reading_info(item.Id);
            }

            List<MESSAGE> msges = db.Messages.Where(x => x.Id_Chatroom == id).ToList<MESSAGE>();
            MessageRepository mesR = new MessageRepository();
            foreach (var item in msges)
            {
                mesR.DeleteMessage(item.Id);
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