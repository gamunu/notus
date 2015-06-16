using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Notus.Hub.Models;

namespace Notus.Hub.Controllers
{
    public class CausesController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Causes
        public IQueryable<object> GetCauses()
        {
            /* return (from c in db.Causes
                    where c.ParentCause == null
                    select new
                    {
                        id = c.Id,
                        text = c.Name,
                        children = from c2 in db.Causes
                                   where c2.ParentCause == c.Id
                                   select new
                                   {
                                       id = c2.Id,
                                       text = c2.Name,
                                       children = from c3 in db.Causes
                                                  where c3.ParentCause == c.Id
                                                  select new
                                                  {
                                                      id = c3.Id,
                                                      text = c3.Name
                                                  }
                                   }
                    });*/
            return db.Causes;
        }

        // GET: api/Causes/5
        [ResponseType(typeof (Cause))]
        public async Task<IHttpActionResult> GetCause(int id)
        {
            var cause = await db.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }

            return Ok(cause);
        }

        // PUT: api/Causes/5
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutCause(long id, Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cause.Id)
            {
                return BadRequest();
            }

            db.Entry(cause).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauseExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Causes
        [ResponseType(typeof (Cause))]
        public async Task<IHttpActionResult> PostCause(Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Causes.Add(cause);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new {id = cause.Id}, cause);
        }

        // DELETE: api/Causes/5
        [ResponseType(typeof (Cause))]
        public async Task<IHttpActionResult> DeleteCause(int id)
        {
            var cause = await db.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }

            db.Causes.Remove(cause);
            await db.SaveChangesAsync();

            return Ok(cause);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauseExists(long id)
        {
            return db.Causes.Count(e => e.Id == id) > 0;
        }
    }
}