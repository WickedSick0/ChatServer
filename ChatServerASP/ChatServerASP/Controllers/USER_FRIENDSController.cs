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

namespace ChatServerASP.Controllers
{
    public class USER_FRIENDSController : ApiController
    {
        private MyContext db = new MyContext();

        // GET: api/USER_FRIENDS
        public IQueryable<USER_FRIENDS> GetUser_friends()
        {
            return db.User_friends;
        }

        // GET: api/USER_FRIENDS/5
        [ResponseType(typeof(USER_FRIENDS))]
        public async Task<IHttpActionResult> GetUSER_FRIENDS(int id)
        {
            USER_FRIENDS uSER_FRIENDS = await db.User_friends.FindAsync(id);
            if (uSER_FRIENDS == null)
            {
                return NotFound();
            }

            return Ok(uSER_FRIENDS);
        }

        /*
        //test
        // GET: api/USER_FRIENDS/5
        //[ResponseType(typeof(USER_FRIENDS))]
        public ActionResult GetUSER_FRIENDSbyUser(int id_Owner)
        {
            //USER_FRIENDS uSER_FRIENDS = await db.User_friends;

            //GetTask<List<USER_FRIENDS>> GetUserFriends = new GetTask<List<USER_FRIENDS>>();
            List<USER_FRIENDS> UserFriendsss = GetUser_friends().ToList();
            List<int> temp = new List<int>();

            foreach (USER_FRIENDS item in UserFriendsss)
            {
                if (item.Id_Friendlist_Owner == id_Owner)
                    temp.Add(item.Id_Friend);
            }

            //GetTask<USER> GetUser = new GetTask<USER>();
            List<USER> GetUsers = new List<USER>();
            USERsController u = new USERsController();
            foreach (int item in temp)
            {
                //GetUsers.Add(await GetUser.GetAsync($"api/USERs/" + item));
                GetUsers.Add(RedirectToAction("GetUSER", "USERs", item));
            }

            UserFriends = GetUsers;



            if (uSER_FRIENDS == null)
            {
                return NotFound();
            }

            return Ok(uSER_FRIENDS);
        }
        */

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
        public async Task<IHttpActionResult> PostUSER_FRIENDS(USER_FRIENDS uSER_FRIENDS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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