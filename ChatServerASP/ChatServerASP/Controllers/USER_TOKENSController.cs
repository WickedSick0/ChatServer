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
    public class USER_TOKENSController : ApiController
    {
        private MyContext db = new MyContext();
        private User_tokensRepository utrep = new User_tokensRepository();

        // GET api/USER_TOKENS
        public IQueryable<USER_TOKENS> GetUser_tokens()
        {
            return db.User_tokens;
        }

        // GET api/USER_TOKENS/5
        [ResponseType(typeof(USER_TOKENS))]
        public async Task<IHttpActionResult> GetUSER_TOKENS(int id)
        {
            USER_TOKENS user_tokens = await db.User_tokens.FindAsync(id);
            if (user_tokens == null)
            {
                return NotFound();
            }

            return Ok(user_tokens);
        }

        // PUT api/USER_TOKENS/5
        public async Task<IHttpActionResult> PutUSER_TOKENS(int id, USER_TOKENS user_tokens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user_tokens.Id)
            {
                return BadRequest();
            }

            db.Entry(user_tokens).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USER_TOKENSExists(id))
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

        // POST api/USER_TOKENS
        [ResponseType(typeof(LoginModel))]
        public async Task<IHttpActionResult> PostUSER_TOKENS(LoginModel lmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            USER_TOKENS token = new USER_TOKENS();

            UserRepository rep = new UserRepository();
            foreach (USER item in rep.FindAll())
            {
                if (lmodel.Username.ToLower() == item.Login.ToLower() && lmodel.Password == item.Password)
                {
                    char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    Random r = new Random();

                    string timestring = DateTime.Now.ToString("{0}{17}{1}yyyy{2}{16}{3}MM{4}dd{5}{6}T{7}{15}{8}HH{9}mm{10}{11}ss{12}ffff{13}{14}");
                    string hash = string.Format(timestring,
                        chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                        chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                        chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                        chars[r.Next(chars.Length)], chars[r.Next(chars.Length)], r.Next(1000, 9999), r.Next(1000, 9999), r.Next(1000, 9999), r.Next(1000, 9999)
                        );

                    token.Id_User = item.Id;
                    token.Token = hash;

                    db.User_tokens.Add(token);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = token.Id }, token);
                }
                
            }
            return BadRequest();
            //return CreatedAtRoute("DefaultApi", new { id = token.Id }, token);
            //return Ok(token.Token);
            //return await GetUSER_TOKENS(token.Id);
        }

        // DELETE api/USER_TOKENS/5
        [ResponseType(typeof(USER_TOKENS))]
        public async Task<IHttpActionResult> DeleteUSER_TOKENS(int id,string token)
        {

            USER_TOKENS user_tokens = utrep.FindByToken(token);
            if (user_tokens == null)
            {
                return NotFound();
            }

            if (utrep.CheckToken(token, id) == false)
            {
                return BadRequest("Bad user token.");
            }
                      
            utrep.DeleteUser_tokens(user_tokens.Id);              
            
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USER_TOKENSExists(int id)
        {
            return db.User_tokens.Count(e => e.Id == id) > 0;
        }
    }
}