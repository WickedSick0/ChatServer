using ChatServerASP.Models;
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

        // GET: api/MESSAGEs
        /*public IQueryable<MESSAGE> GetMessages() //nechat zakomentovane jinak uniknou data
        {
            return db.Messages;
        }*/
        
        // GET: api/MESSAGEs/5
        [ResponseType(typeof(List<MESSAGE>))]
        public async Task<IHttpActionResult> GetMESSAGE(int id, string token)
        {
            if (CheckToken(token) == false)
            {
                return NotFound();
            }
            if (CheckChatroomMembership(id, token))
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
            if (CheckToken(mAutorization.token) == false)// overeni tokenu
            {
                return BadRequest();
            }

            if (CheckChatroomMembership(mAutorization.Id_Chatroom, mAutorization.token)) // overeni pravomoci postovat do dane roomky
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
        private bool CheckToken(string token) //kontroluje spravnost tokenu
        {
            USER_TOKENS Ut = db.User_tokens.Where(x => x.Token == token).FirstOrDefault();
            if (Ut == null || Ut.Token != token)
            {
                return false;
            }
            return true;
        }
        private bool CheckChatroomMembership(int id_chatroom, string token) // kontroluje zdali je uzivatel co vykonava(neco[POST,GET atd.]) v chatroomu
        {
            CHATROOM_MEMBERS chmember = db.Chatroom_members.Where(x => x.Id_Chatroom == id_chatroom && x.Id_User == db.User_tokens.Where(y => y.Token == token).FirstOrDefault().Id_User).FirstOrDefault();
            if (chmember == null)
            {
                return false;
            }
            return true;
        }
    }
}