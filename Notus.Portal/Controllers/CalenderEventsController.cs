using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Notus.Portal.Models;

namespace Notus.Portal.Controllers
{
    public class CalenderEventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CalenderEvents
        public IQueryable<CalenderEvent> GetCalenderEvents()
        {
            return db.CalenderEvents;
        }

        // GET: api/CalenderEvents/5
        [ResponseType(typeof(CalenderEvent))]
        public async Task<IHttpActionResult> GetCalenderEvent(long id)
        {
            CalenderEvent calenderEvent = await db.CalenderEvents.FindAsync(id);
            if (calenderEvent == null)
            {
                return NotFound();
            }

            return Ok(calenderEvent);
        }

        // PUT: api/CalenderEvents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCalenderEvent(long id, CalenderEvent calenderEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calenderEvent.Id)
            {
                return BadRequest();
            }

            db.Entry(calenderEvent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalenderEventExists(id))
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

        // POST: api/CalenderEvents
        [ResponseType(typeof(CalenderEvent))]
        public async Task<IHttpActionResult> PostCalenderEvent(CalenderEvent calenderEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalenderEvents.Add(calenderEvent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = calenderEvent.Id }, calenderEvent);
        }

        // DELETE: api/CalenderEvents/5
        [ResponseType(typeof(CalenderEvent))]
        public async Task<IHttpActionResult> DeleteCalenderEvent(long id)
        {
            CalenderEvent calenderEvent = await db.CalenderEvents.FindAsync(id);
            if (calenderEvent == null)
            {
                return NotFound();
            }

            db.CalenderEvents.Remove(calenderEvent);
            await db.SaveChangesAsync();

            return Ok(calenderEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalenderEventExists(long id)
        {
            return db.CalenderEvents.Count(e => e.Id == id) > 0;
        }
    }
}