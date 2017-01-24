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
using System.Web.Mvc;
using ChatServerASP.Models.Tables;
using System.Web;
using ChatServerASP.Models.Repositories;

namespace ChatServerASP.Controllers
{
    public class USER_FRIENDSController : ApiController
    {
        private MyContext db = new MyContext();
        private User_tokensRepository utRepository = new User_tokensRepository();
        private Chatroom_membersRepository chMRepository = new Chatroom_membersRepository();
        private User_friendsRepository ufrepository = new User_friendsRepository();
        // GET: api/USER_FRIENDS
        /*public IQueryable<USER_FRIENDS> GetUser_friends()
        {
            return db.User_friends;
        }*/

        // GET: api/USER_FRIENDS/5?token=AASDFASDF
        [ResponseType(typeof(List<USER>))]
        public async Task<IHttpActionResult> GetUSER_FRIENDS(int id, string token)
        {
            if (utRepository.CheckToken(token, id) == false)
            {
                return NotFound();
            }
            User_friendsRepository rep = new User_friendsRepository();

            return Ok(rep.FindFriendsByOwner(id).ToList());



            //original
            /*
            USER_FRIENDS uSER_FRIENDS = await db.User_friends.FindAsync(id);
            if (uSER_FRIENDS == null)
            {
                return NotFound();
            }

            return Ok(uSER_FRIENDS);
            */
        }

        // PUT: api/USER_FRIENDS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSER_FRIENDS(int id, USER_FRIENDS uSER_FRIENDS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSER_FRIENDS.Id)
            {
                return BadRequest();
            }

            db.Entry(uSER_FRIENDS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USER_FRIENDSExists(id))
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

        // POST: api/USER_FRIENDS
        [ResponseType(typeof(USER_FRIENDS))]
        public async Task<IHttpActionResult> PostUSER_FRIENDS(USER_FRIENDS uSER_FRIENDS, string token)
        {
            if (utRepository.CheckToken(token, uSER_FRIENDS.Id_Friendlist_Owner) == false)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ufrepository.duplicityfriend(uSER_FRIENDS.Id_Friendlist_Owner, uSER_FRIENDS.Id_Friend))
            {
                return BadRequest("You have this user already in your friendlist!");
            }

            db.User_friends.Add(uSER_FRIENDS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSER_FRIENDS.Id }, uSER_FRIENDS);
        }

        // DELETE: api/USER_FRIENDS/5
        [ResponseType(typeof(USER_FRIENDS))]
        public async Task<IHttpActionResult> DeleteUSER_FRIENDS(int id)
        {
            USER_FRIENDS uSER_FRIENDS = await db.User_friends.FindAsync(id);
            if (uSER_FRIENDS == null)
            {
                return NotFound();
            }

            db.User_friends.Remove(uSER_FRIENDS);
            await db.SaveChangesAsync();

            return Ok(uSER_FRIENDS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USER_FRIENDSExists(int id)
        {
            return db.User_friends.Count(e => e.Id == id) > 0;
        }
    }
}