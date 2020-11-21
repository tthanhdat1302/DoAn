using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DoAn2.Models;

namespace DoAn2.Controllers
{
    [Route("v1/")]
    public class SachesController : ApiController
    {
        private BOOKSTOREEntities db = new BOOKSTOREEntities();

        
        // GET: api/Saches
        [HttpGet]
        public IQueryable<Sach> GetSaches()
        {
            return db.Saches;
        }
        [HttpGet]
        // GET: api/Saches/5
        [ResponseType(typeof(Sach))]
        public IHttpActionResult GetSach(int id)
        {
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }

            return Ok(sach);
        }
        [HttpPost]
        [Route("PutSach")]
        // PUT: api/Saches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSach(int id, Sach sach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sach.SachID)
            {
                return BadRequest();
            }

            db.Entry(sach).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(id))
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

        // POST: api/Saches
        [HttpPost]
        [ResponseType(typeof(Sach))]
        public IHttpActionResult PostSach(Sach sach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saches.Add(sach);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sach.SachID }, sach);
        }

        // DELETE: api/Saches/5
        [ResponseType(typeof(Sach))]
        public IHttpActionResult DeleteSach(int id)
        {
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return NotFound();
            }

            db.Saches.Remove(sach);
            db.SaveChanges();

            return Ok(sach);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SachExists(int id)
        {
            return db.Saches.Count(e => e.SachID == id) > 0;
        }
    }
}