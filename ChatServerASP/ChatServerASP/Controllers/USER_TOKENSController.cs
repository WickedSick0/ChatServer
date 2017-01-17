﻿using System;
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

namespace ChatServerASP.Controllers
{
    public class USER_TOKENSController : ApiController
    {
        private MyContext db = new MyContext();

        // GET api/USER_TOKENS
        public IQueryable<USER_TOKENS> GetUser_tokens()
        {
            return db.User_tokens;
        }

        // GET api/USER_TOKENS/5
        //[ResponseType(typeof(USER_TOKENS))]
        public async Task<IHttpActionResult> GetUSER_TOKENS(int id)
        {
            USER_TOKENS token = new USER_TOKENS();

            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            Random r = new Random();

            string timestring = DateTime.Now.ToString("{0}{1}yyyy{2}{3}MM{4}dd{5}{6}T{7}{8}HH{9}mm{10}{11}ss{12}ffff{13}");
            string hash = string.Format(timestring,
                chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],chars[r.Next(chars.Length)], chars[r.Next(chars.Length)],
                chars[r.Next(chars.Length)], chars[r.Next(chars.Length)]
                );
            return Ok(hash);
            /*USER_TOKENS user_tokens = await db.User_tokens.FindAsync(id);
            if (user_tokens == null)
            {
                return NotFound();
            }

            return Ok(user_tokens);*/
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
        [ResponseType(typeof(USER_TOKENS))]
        public async Task<IHttpActionResult> PostUSER_TOKENS(USER_TOKENS user_tokens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User_tokens.Add(user_tokens);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user_tokens.Id }, user_tokens);
        }

        // DELETE api/USER_TOKENS/5
        [ResponseType(typeof(USER_TOKENS))]
        public async Task<IHttpActionResult> DeleteUSER_TOKENS(int id)
        {
            USER_TOKENS user_tokens = await db.User_tokens.FindAsync(id);
            if (user_tokens == null)
            {
                return NotFound();
            }

            db.User_tokens.Remove(user_tokens);
            await db.SaveChangesAsync();

            return Ok(user_tokens);
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