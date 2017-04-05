using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace openBasculaNetWeb.API
{
    public class openBasculaController : ApiController
    {
        // GET: api/openBascula
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/openBascula/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/openBascula
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/openBascula/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/openBascula/5
        public void Delete(int id)
        {
        }
    }
}
