using ChatServerASP.Models;
using ChatServerASP.Models.Repositories;
using ChatServerASP.Models.Tables;
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

namespace ChatServerASP.Controllers
{
    public class MESSAGEsController : ApiController
    {
        private MyContext db = new MyContext();
        private User_tokensRepository utRepository = new User_tokensRepository();
        private Chatroom_membersRepository chMRepository = new Chatroom_membersRepository();

        // GET: api/MESSAGEs
        /*public IQueryable<MESSAGE> GetMessages() //nechat zakomentovane jinak uniknou data
        {
            return db.Messages;
        }*/
        
        // GET: api/MESSAGEs/5
        [ResponseType(typeof(List<MESSAGE>))]
        public async Task<IHttpActionResult> GetMESSAGE(int id, string token)
        {
            if (utRepository.CheckToken(token,db.User_tokens.Where(x => x.Token == token).Select(x => x.Id_User).FirstOrDefault()) == false)
            {
                return NotFound();
            }
            if (chMRepository.CheckChatroomMembership(id, token))
            {
                List<MESSAGE> msglist = db.Messages.Where(x => x.Id_Chatroom == id).ToList();
                if (msglist == null)
                {
                    return NotFound();
                }

                return Ok(msglist);
            }
            return NotFound();


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

        public class MessageAutorization // nutno vytvorit, jinak neni mozne vubec vyhledat metodu POST, pokud ma vice jak jeden parameter (PICOVINA ASP!)
        {
            public string token { get; set; }
            public int Id_Chatroom { get; set; }

            public int Id_User_Post { get; set; }

            public string Message_text { get; set; }

            public DateTime Send_time { get; set; }
        }


        // POST: api/MESSAGEs
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostMESSAGE(MessageAutorization mAutorization)
        {
            if (utRepository.CheckToken(mAutorization.token,mAutorization.Id_User_Post) == false)// overeni tokenu
            {
                return BadRequest();
            }
            if (chMRepository.CheckChatroomMembership(mAutorization.Id_Chatroom, mAutorization.token)) // overeni pravomoci postovat do dane roomky
            {
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                MESSAGE mESSAGE = new MESSAGE();
                mESSAGE.Id_Chatroom = mAutorization.Id_Chatroom;
                mESSAGE.Id_User_Post = mAutorization.Id_User_Post;
                mESSAGE.Message_text = mAutorization.Message_text;
                mESSAGE.Send_time = mAutorization.Send_time;
                


                db.Messages.Add(mESSAGE);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = mESSAGE.Id }, mESSAGE);

            }
            return BadRequest();
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