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
                return BadRequest("Incorrect Token!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ufrepository.duplicityfriend(uSER_FRIENDS.Id_Friendlist_Owner, uSER_FRIENDS.Id_Friend))
            {
                return BadRequest("You have this user already in your friendlist!");
            }

            if (uSER_FRIENDS.Id_Friend == uSER_FRIENDS.Id_Friendlist_Owner) return BadRequest("Can't add yourself");

            //db.User_friends.Add(uSER_FRIENDS);
            await db.SaveChangesAsync();

            FRIEND_REQUESTController fr = new FRIEND_REQUESTController();
            ChatServerASP.Controllers.FRIEND_REQUESTController.PostRequest pr = new FRIEND_REQUESTController.PostRequest();
            pr.Id_Friendlist_Owner_sender = uSER_FRIENDS.Id_Friendlist_Owner;
            pr.Id_Friend_receiver = uSER_FRIENDS.Id_Friend;
            pr.token = token;
            fr.PostFRIEND_REQUEST(pr);

            return CreatedAtRoute("DefaultApi", new { id = uSER_FRIENDS.Id }, uSER_FRIENDS);
        }

        // DELETE: api/USER_FRIENDS/5
        [ResponseType(typeof(USER_FRIENDS))]
        [Route("api/USER_FRIENDS/{id}/{idfriend}/{token}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUSER_FRIENDS(int id, int idfriend, string token) //moje id, id frienda, token
        {
            if (utRepository.CheckToken(token, id) == false)
            {
                return BadRequest("Incorrect token!");
            }
            //need to delete both users from friend list
            USER_FRIENDS uSER_FRIENDS = db.User_friends.Where(x => x.Id_Friendlist_Owner == id && x.Id_Friend == idfriend).FirstOrDefault();            
            USER_FRIENDS uSER_FRIENDS2 = db.User_friends.Where(x => x.Id_Friendlist_Owner == idfriend && x.Id_Friend == id).FirstOrDefault();
            if (uSER_FRIENDS == null && uSER_FRIENDS2 == null) //both not found (friendship doesn't exist)
            {
                return NotFound();
            }
            /*one not found (need this for bugged users ONLY - one user has him in friends, but the other doesn't)*/
            else if(uSER_FRIENDS != null && uSER_FRIENDS2 == null)
            {
                db.User_friends.Remove(uSER_FRIENDS);
            }
            /*else if(uSER_FRIENDS == null && uSER_FRIENDS2 != null) //uSER_FRIENDS will never be null
            {
                db.User_friends.Remove(uSER_FRIENDS2);
            }*/
            else if (uSER_FRIENDS != null && uSER_FRIENDS2 != null)//both found (friendship exists)
            {
                db.User_friends.Remove(uSER_FRIENDS);
                db.User_friends.Remove(uSER_FRIENDS2);
            }
            await db.SaveChangesAsync();



            return Ok();
            /*
            USER_FRIENDS uSER_FRIENDS = await db.User_friends.FindAsync(id);
            if (uSER_FRIENDS == null)
            {
                return NotFound();
            }

            db.User_friends.Remove(uSER_FRIENDS);
            await db.SaveChangesAsync();

            return Ok(uSER_FRIENDS);*/
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