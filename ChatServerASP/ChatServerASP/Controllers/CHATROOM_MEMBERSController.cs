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
using ChatServerASP.Models.Repositories;

namespace ChatServerASP.Controllers
{
    public class CHATROOM_MEMBERSController : ApiController
    {
        private MyContext db = new MyContext();
        private User_tokensRepository utRepository = new User_tokensRepository();
        private Chatroom_membersRepository chmR = new Chatroom_membersRepository();
        private User_friendsRepository ufR = new User_friendsRepository();
        UserRepository uRep = new UserRepository();

        /// <summary>
        /// return all chatroom_members
        /// </summary>
        /// <returns>all chatroom_members</returns>
        // GET: api/CHATROOM_MEMBERS
        public IQueryable<CHATROOM_MEMBERS> GetChatroom_members()
        {
            return db.Chatroom_members;
        }

        /// <summary>
        /// return list of users in chatroom
        /// </summary>
        /// <param name="id">id of chatroom</param>
        /// <param name="token">token of logged user</param>
        /// <param name="IDUser">id of logged user</param>
        /// <returns>list of users in chatroom</returns>
        // GET: api/CHATROOM_MEMBERS/5?token=fdsakfjl
        [ResponseType(typeof(List<USER>))]
        public async Task<IHttpActionResult> GetCHATROOM_MEMBERS(int id, string token, int IDUser)
        {
            if (utRepository.CheckToken(token, IDUser) == false)
            {
                return BadRequest("Invalid token. Please login again!");
            }
            if (db.Chatroom_members.Where(x => x.Id_Chatroom == id && x.Id_User == IDUser).FirstOrDefault() == null)
            {
                return BadRequest("You dont have permissions for this!");
            }
            
            List<USER> UsersInChatroom = new List<USER>();
            USER u = new USER();
            foreach (CHATROOM_MEMBERS item in chmR.FindAll())
            {
                if (id == item.Id_Chatroom)
                {
                    u = uRep.FindById(item.Id_User);
                    u.Password = null;
                    UsersInChatroom.Add(u);
                }
            }

            return Ok(UsersInChatroom);

            //original
            //CHATROOM_MEMBERS cHATROOM_MEMBERS = await db.Chatroom_members.FindAsync(id);
            //if (cHATROOM_MEMBERS == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cHATROOM_MEMBERS);
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

        public class DeleteAddMember
        {
            public int chatroomID { get; set; }
            public int[] ChatroomMembersID { get; set; }
            public int IDUser { get; set; }
            public string Token { get; set; }
        }


        // POST: api/CHATROOM_MEMBERS
        [ResponseType(typeof(CHATROOM_MEMBERS))]
        public async Task<IHttpActionResult> PostCHATROOM_MEMBERS(DeleteAddMember cHATROOM_MEMBERS)
        {
            if (cHATROOM_MEMBERS == null)
            {
                return NotFound();
            }
            if (utRepository.CheckToken(cHATROOM_MEMBERS.Token, cHATROOM_MEMBERS.IDUser) == false)
            {
                return BadRequest("Invalid token. Please login again!");
            }

            if (db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS.chatroomID && x.Id_User == cHATROOM_MEMBERS.IDUser).FirstOrDefault() == null)
            {
                return BadRequest("You dont have permissions for add to this chatroom!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CHATROOM_MEMBERS chatroomMember = new CHATROOM_MEMBERS();
            chatroomMember.Id_Chatroom = cHATROOM_MEMBERS.chatroomID;
            foreach (var item in cHATROOM_MEMBERS.ChatroomMembersID)
            {
                if (db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS.chatroomID && x.Id_User == item).FirstOrDefault() == null)
                {
                    if (ufR.checkMutualFriendship(cHATROOM_MEMBERS.IDUser, item))
                    {
                        chatroomMember.Id_User = item;
                        chmR.InsertChatroom_members(chatroomMember);
                    }
                }
            }
            return Ok("Members added.");
        }

        [ResponseType(typeof(DeleteAddMember))]
        [HttpPost]
        [Route("api/CHATleave")]
        public async Task<IHttpActionResult> CHATROOM_MEMBERSleave(DeleteAddMember cHATROOM_MEMBERS_Delete) //need this for forms
        {
            if (utRepository.CheckToken(cHATROOM_MEMBERS_Delete.Token, cHATROOM_MEMBERS_Delete.IDUser) == false)
            {
                return BadRequest("Invalid token. Please login again!");
            }
            if (db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS_Delete.chatroomID && x.Id_User == cHATROOM_MEMBERS_Delete.IDUser).FirstOrDefault() == null)
            {
                return BadRequest("You dont have permissions to remove user from this chatroom!");
            }
            List<CHATROOM_MEMBERS> cHATROOM_MEMBERS = db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS_Delete.chatroomID && cHATROOM_MEMBERS_Delete.ChatroomMembersID.Contains(x.Id_User)).ToList();
            if (cHATROOM_MEMBERS == null)
            {
                return NotFound();
            }
            foreach (var item in cHATROOM_MEMBERS)
            {
                db.Chatroom_members.Remove(item);
                db.SaveChanges();
            }

            return Ok(cHATROOM_MEMBERS + "deleted");
        }

        // DELETE: api/CHATROOM_MEMBERS/5
        [ResponseType(typeof(DeleteAddMember))]
        public async Task<IHttpActionResult> DeleteCHATROOM_MEMBERS(DeleteAddMember cHATROOM_MEMBERS_Delete)
        {
            if (utRepository.CheckToken(cHATROOM_MEMBERS_Delete.Token, cHATROOM_MEMBERS_Delete.IDUser) == false)
            {
                return BadRequest("Invalid token. Please login again!");
            }
            if (db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS_Delete.chatroomID && x.Id_User == cHATROOM_MEMBERS_Delete.IDUser).FirstOrDefault() == null)
            {
                return BadRequest("You dont have permissions to remove user from this chatroom!");
            }
            List<CHATROOM_MEMBERS> cHATROOM_MEMBERS = db.Chatroom_members.Where(x => x.Id_Chatroom == cHATROOM_MEMBERS_Delete.chatroomID && cHATROOM_MEMBERS_Delete.ChatroomMembersID.Contains(x.Id_User)).ToList();
            if (cHATROOM_MEMBERS == null)
            {
                return NotFound();
            }
            foreach (var item in cHATROOM_MEMBERS)
            {
                db.Chatroom_members.Remove(item);
                db.SaveChanges();
            }

            return Ok(cHATROOM_MEMBERS + "deleted");
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