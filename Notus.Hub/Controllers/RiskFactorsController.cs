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
    public class RiskFactorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RiskFactors
        public IQueryable<RiskFactor> GetRiskFactors()
        {
            return db.RiskFactors;
        }

        // GET: api/RiskFactors/5
        [ResponseType(typeof(RiskFactor))]
        public async Task<IHttpActionResult> GetRiskFactor(long id)
        {
            RiskFactor riskFactor = await db.RiskFactors.FindAsync(id);
            if (riskFactor == null)
            {
                return NotFound();
            }

            return Ok(riskFactor);
        }

        // PUT: api/RiskFactors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRiskFactor(long id, RiskFactor riskFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != riskFactor.Id)
            {
                return BadRequest();
            }

            db.Entry(riskFactor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskFactorExists(id))
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

        // POST: api/RiskFactors
        [ResponseType(typeof(RiskFactor))]
        public async Task<IHttpActionResult> PostRiskFactor(RiskFactor riskFactor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RiskFactors.Add(riskFactor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = riskFactor.Id }, riskFactor);
        }

        // DELETE: api/RiskFactors/5
        [ResponseType(typeof(RiskFactor))]
        public async Task<IHttpActionResult> DeleteRiskFactor(long id)
        {
            RiskFactor riskFactor = await db.RiskFactors.FindAsync(id);
            if (riskFactor == null)
            {
                return NotFound();
            }

            db.RiskFactors.Remove(riskFactor);
            await db.SaveChangesAsync();

            return Ok(riskFactor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RiskFactorExists(long id)
        {
            return db.RiskFactors.Count(e => e.Id == id) > 0;
        }
    }
}