using System.Collections.Generic;
using System.Web.Http;

namespace Notus.Hub.Controllers
{
    public class WorldMapController : ApiController
    {
        // GET: api/WordMap
        public Dictionary<string, object> Get()
        {
            return new Dictionary<string, object>
            {
                {"USA", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")},
                {"JPN", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")},
                {"ITA", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")},
                {"CRI", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")},
                {"KOR", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")},
                {"DEU", new KeyValuePair<string, object>("fillKey", "authorHasTraveledTo")}
            };
        }

        // GET: api/WordMap/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WordMap
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/WordMap/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/WordMap/5
        public void Delete(int id)
        {
        }
    }
}