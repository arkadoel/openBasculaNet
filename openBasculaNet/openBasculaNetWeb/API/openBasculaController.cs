using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using openBasculaNet.Core.Structures;
using openBasculaNet.BusinessLogic.openBascula;

namespace openBasculaNetWeb.API
{
    public class openBasculaController : ApiController
    {
        /*// GET: api/openBascula
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
        }*/

        /// <summary>
        /// Obtiene la lista de transitos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/GetTransitos")]
        public List<TRANSITO_ACTUALES> GetTransitos(string filtro="")
        {
            return Logic_Transitos.ListarTransitosActuales(filtro);
        }

        [HttpPut]
        [Route("API/openBascula/GuardarTransito")]
        public bool GuardarTransito(TRANSITO_ACTUALES jsonTransito, string fechaEntrada)
        {
            return true;
        }
    }
}
