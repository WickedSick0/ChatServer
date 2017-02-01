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
using ChatServerASP.Models.Repositories;

namespace ChatServerASP.Controllers
{
    public class FRIEND_REQUESTController : ApiController
    {
        private MyContext db = new MyContext();
        User_tokensRepository utr = new User_tokensRepository();
        // GET api/FRIEND_REQUEST
        public IQueryable<FRIEND_REQUEST> GetFriend_Requests()
        {
            return db.Friend_Requests;
        }

        // GET api/FRIEND_REQUEST/5
        [ResponseType(typeof(List<FRIEND_REQUEST>))]
        public async Task<IHttpActionResult> GetFRIEND_REQUEST(int id,string token)
        {
            if (utr.CheckToken(token, id) == false)
            {
                return NotFound();
            }
            
            List<FRIEND_REQUEST> friend_request = db.Friend_Requests.Where(x => x.Id_Friend_receiver == id).ToList();

            if (friend_request == null)
            {
                return NotFound();
            };
            USERsController uc = new USERsController();

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
        
        public class PostRequest
        {
            public string token { get; set; }
            public int Id_Friendlist_Owner_sender { get; set; }
            public int Id_Friend_receiver { get; set; }
            /*public DateTime Send_Time { get; set; }
            public bool Accepted { get; set; }*/
        }
        [ResponseType(typeof(FRIEND_REQUEST))]
        public async Task<IHttpActionResult> PostFRIEND_REQUEST(PostRequest Postfriend_request)
        {
            if (utr.CheckToken(Postfriend_request.token, Postfriend_request.Id_Friendlist_Owner_sender) == false)
            {
                return BadRequest("Incorrect token");
            }

            FRIEND_REQUEST friend_request = new FRIEND_REQUEST();
            friend_request.Id_Friend_receiver = Postfriend_request.Id_Friend_receiver;
            friend_request.Id_Friendlist_Owner_sender = Postfriend_request.Id_Friendlist_Owner_sender;
            friend_request.Send_Time = DateTime.Now;
            friend_request.Accepted = false;

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