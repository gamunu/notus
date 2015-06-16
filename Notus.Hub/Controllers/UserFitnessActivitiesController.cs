using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Notus.Hub.Models;

namespace Notus.Hub.Controllers
{
    [Authorize]
    [RoutePrefix("Api/Fit/Activities")]
    public class FitnessActivitiesController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly IQueryable<UserFitnessActivity> _userFitnessActivities;
        private readonly string _userId;
        private readonly HealthProfile _healthProfile;

        public FitnessActivitiesController()
        {
            _userId = User.Identity.GetUserId();

            _userFitnessActivities = _db.UserFitnessActivities.Where(activity => activity.UserId == _userId);

            _healthProfile = _db.HealthProfiles.FirstOrDefault(u => u.UserId == _userId);
        }

        [Route("Types")]
        [HttpGet]
        // GET: api/FitnessActivities
        public IQueryable<FitnessActivity> GetTypes()
        {
            return _db.FitnessActivities;
        }

        [Route("")]
        [HttpGet]
        // GET: api/UserFitnessActivities
        public async Task<object> GetUserFitnessActivities()
        {
            return new
            {
                //Data
                Data = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.Activity.Id,
                        Total = _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).GroupBy(x => 1)
                            .Select(activity => new
                            {
                                Steps = activity.Sum(c => c.Steps),
                                Energy = activity.Sum(c => c.Energy),
                                Distance = activity.Sum(c => c.Distance),
                                Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                            }),
                        Activities = _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).Select(d => new
                        {
                            d.Distance,
                            d.Steps,
                            d.Energy,
                            d.StartTime,
                            d.EndTime
                        })
                    }),
                //Total
                Total = await _userFitnessActivities
                    .GroupBy(x => 1)
                    .Select(activity => new
                    {
                        Steps = activity.Sum(c => c.Steps),
                        Energy = activity.Sum(c => c.Energy),
                        Distance = activity.Sum(c => c.Distance),
                        Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime)),
                        MinTime = activity.Min(c => c.StartTime),
                        MaxTime = activity.Max(c => c.EndTime)
                    }).FirstOrDefaultAsync()
            };
        }


        [Route("Only")]
        [HttpGet]
        // GET: api/UserFitnessActivities
        public async Task<object> Only()
        {
            var today = DateTime.Today.ToUniversalTime();
            var endoftheday = today.AddMinutes(1440);
            return new
            {
                //Data
                Data = _userFitnessActivities.Where(e => e.StartTime >= today)
                .Where(e => e.EndTime <= endoftheday)
                    .GroupBy(g => g.ActivityId, s => new
                    {
                        s.Distance,
                        s.Steps,
                        s.Energy,
                        s.StartTime,
                        s.EndTime
                    }).ToList(),
                //Total
                Total = await _userFitnessActivities.Where(e => e.StartTime >= today)
                .Where(e => e.EndTime <= endoftheday)
                    .GroupBy(x => 1)
                    .Select(activity => new
                    {
                        Steps = activity.Sum(c => c.Steps),
                        Energy = activity.Sum(c => c.Energy),
                        Distance = activity.Sum(c => c.Distance),
                        Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime)),
                        MinTime = activity.Min(c => c.StartTime),
                        MaxTime = activity.Max(c => c.EndTime)
                    }).FirstOrDefaultAsync()
            };
        }


        [Route("Only/{type}")]
        [HttpGet]
        // GET: api/UserFitnessActivities
        public async Task<object> Only(string type)
        {
            DateTime today = DateTime.Today.ToUniversalTime();
            DateTime endoftheday = today.AddMinutes(1440);

            Expression<Func<UserFitnessActivity, object>> typeEt = s => new
            {
                ActiveTime = SqlFunctions.DateDiff("n", s.StartTime, s.EndTime),
                s.StartTime,
                s.EndTime
            };

            switch (type.ToLower())
            {
                case "steps":
                    typeEt = s => new
                    {
                        s.Steps,
                        s.StartTime,
                        s.EndTime
                    };
                    break;
                case "distance":
                    typeEt = s => new
                    {
                        s.Distance,
                        s.StartTime,
                        s.EndTime
                    };
                    break;
                case "energy":
                    typeEt = s => new
                    {
                        s.Energy,
                        s.StartTime,
                        s.EndTime
                    };
                    break;
            }


            return new
            {
                //Data
                Data = _userFitnessActivities.Where(e => e.StartTime >= today)
                .Where(e => e.EndTime <= endoftheday)
                    .GroupBy(g => g.ActivityId, typeEt).ToList(),
                //Total
                Total = await _userFitnessActivities.Where(e => e.StartTime >= today)
                .Where(e => e.EndTime <= endoftheday)
                    .GroupBy(x => 1)
                    .Select(activity => new
                    {
                        Steps = activity.Sum(c => c.Steps),
                        Energy = activity.Sum(c => c.Energy),
                        Distance = activity.Sum(c => c.Distance),
                        Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime)),
                        MinTime = activity.Min(c => c.StartTime),
                        MaxTime = activity.Max(c => c.EndTime)
                    }).FirstOrDefaultAsync()
            };
        }

        [Route("Total/Group/Date")]
        [HttpGet]
        public async Task<object> TotalGroupDate()
        {
            return new
            {
                //Activities group wise
                Groups = _userFitnessActivities
                    .OrderByDescending(e => e.StartTime)
                    .GroupBy(g => g.StartTime.Day)
                    .OrderByDescending(e => e.Key)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.StartTime,
                        Total = _userFitnessActivities.Where(e => e.StartTime.Day == act.StartTime.Day).GroupBy(x => 1)
                            .Select(activity => new
                            {
                                Steps = activity.Sum(c => c.Steps),
                                Energy = activity.Sum(c => c.Energy),
                                Distance = activity.Sum(c => c.Distance),
                                Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                            }),
                        Activities = _userFitnessActivities.Where(e => e.StartTime.Day == act.StartTime.Day)
                        .OrderByDescending(e => e.StartTime)
                        .Select(activity => new
                            {
                                activity.Activity.Name,
                                activity.Activity.Id,
                                activity.StartTime,
                                activity.EndTime,
                                activity.Steps,
                                activity.Energy,
                                activity.Distance,
                                Minutes = SqlFunctions.DateDiff("n", activity.StartTime, activity.EndTime)
                            })
                    }),

                //Total of overall activities
                Total = await _userFitnessActivities
                    .GroupBy(x => 1)
                    .Select(activity => new
                    {
                        Steps = activity.Sum(c => c.Steps),
                        Energy = activity.Sum(c => c.Energy),
                        Distance = activity.Sum(c => c.Distance),
                        Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                    }).FirstOrDefaultAsync(),
                Goal = new
                {
                    Steps = _healthProfile.FitnessGloalSteps,
                    Duration = _healthProfile.FitnessGloalDuration,
                    Distance = _healthProfile.FitnessGloalDistance,
                    Energy = _healthProfile.FitnessGloalCaloriesBurned
                }
            };
        }

        [Route("Total/Group")]
        [HttpGet]
        public async Task<object> UserFitnessGroups()
        {
            return new
            {
                //Activities group wise
                Groups = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.Activity.Id,
                        Total = _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).GroupBy(x => 1)
                            .Select(activity => new
                            {
                                Steps = activity.Sum(c => c.Steps),
                                Energy = activity.Sum(c => c.Energy),
                                Distance = activity.Sum(c => c.Distance),
                                Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                            })
                    }),

                //Total of overall activities
                Total = await _userFitnessActivities
                    .GroupBy(x => 1)
                    .Select(activity => new
                    {
                        Steps = activity.Sum(c => c.Steps),
                        Energy = activity.Sum(c => c.Energy),
                        Distance = activity.Sum(c => c.Distance),
                        Minutes = activity.Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                    }).FirstOrDefaultAsync(),
                Goal = new
                {
                    Steps = _healthProfile.FitnessGloalSteps,
                    Duration = _healthProfile.FitnessGloalDuration,
                    Distance = _healthProfile.FitnessGloalDistance,
                    Energy = _healthProfile.FitnessGloalCaloriesBurned
                }
            };
        }

        [Route("Total/Steps")]
        [HttpGet]
        public async Task<object> TotalSteps()
        {
            return new
            {
                Groups = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.ActivityId,
                        Total = _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).Sum(d => d.Steps)
                    }),
                Total = await _userFitnessActivities.SumAsync(e => e.Steps),
                Goal = _healthProfile.FitnessGloalSteps
            };
        }

        [Route("Total/Energy")]
        [HttpGet]
        public async Task<object> TotalEnergy()
        {
            return new
            {
                Groups = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.ActivityId,
                        Total = _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).Sum(d => d.Energy)
                    }),
                Total = await _userFitnessActivities.SumAsync(e => e.Energy),
                Goal = _healthProfile.FitnessGloalCaloriesBurned
            };
        }

        [Route("Total/Distance")]
        [HttpGet]
        public async Task<object> TotalDistance()
        {
            return new
            {
                Groups = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.ActivityId,
                        Total =
                            _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId).Sum(d => d.Distance)
                    }),
                Total = await _userFitnessActivities.SumAsync(e => e.Distance),
                Goal = _healthProfile.FitnessGloalDistance
            };
        }

        [Route("Total/Minutes")]
        [HttpGet]
        public async Task<object> TotalMinutes()
        {
            return new
            {
                Groups = _userFitnessActivities
                    .GroupBy(g => g.ActivityId)
                    .Select(first => first.FirstOrDefault())
                    .Select(act => new
                    {
                        act.Activity.Name,
                        act.ActivityId,
                        Total =
                            _userFitnessActivities.Where(e => e.ActivityId == act.ActivityId)
                                .Sum(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime))
                    }),
                Total = await _userFitnessActivities.SumAsync(c => SqlFunctions.DateDiff("n", c.StartTime, c.EndTime)),
                Goal = _healthProfile.FitnessGloalDuration
            };
        }

        // GET: api/UserFitnessActivities/5
        [ResponseType(typeof(UserFitnessActivity))]
        public async Task<IHttpActionResult> GetUserFitnessActivity(long id)
        {
            var userFitnessActivity = await _db.UserFitnessActivities.FindAsync(id);
            if (userFitnessActivity == null)
            {
                return NotFound();
            }

            return Ok(userFitnessActivity);
        }

        // PUT: api/UserFitnessActivities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserFitnessActivity(long id, UserFitnessActivity userFitnessActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userFitnessActivity.Id)
            {
                return BadRequest();
            }

            _db.Entry(userFitnessActivity).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFitnessActivityExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserFitnessActivities
        [ResponseType(typeof(UserFitnessActivity))]
        public async Task<IHttpActionResult> PostUserFitnessActivity(UserFitnessActivity userFitnessActivity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.UserFitnessActivities.Add(userFitnessActivity);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userFitnessActivity.Id }, userFitnessActivity);
        }

        // DELETE: api/UserFitnessActivities/5
        [ResponseType(typeof(UserFitnessActivity))]
        public async Task<IHttpActionResult> DeleteUserFitnessActivity(long id)
        {
            var userFitnessActivity = await _db.UserFitnessActivities.FindAsync(id);
            if (userFitnessActivity == null)
            {
                return NotFound();
            }

            _db.UserFitnessActivities.Remove(userFitnessActivity);
            await _db.SaveChangesAsync();

            return Ok(userFitnessActivity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserFitnessActivityExists(long id)
        {
            return _db.UserFitnessActivities.Count(e => e.Id == id) > 0;
        }
    }
}