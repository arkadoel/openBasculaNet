using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using openBasculaNet.Core.Structures;
using openBasculaNet.BusinessLogic.openBascula;
using openBasculaNet.Core.Utiles;

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
        /// <param name="filtro">Permite filtrar por matricula de cabina o remolque</param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/GetTransitos")]
        public List<TRANSITO_ACTUALES> GetTransitos(string filtro="")
        {
            return Logic_Transitos.ListarTransitosActuales(filtro);
        }

        /// <summary>
        /// Guarda los datos de un transito en base de datos
        /// </summary>
        /// <param name="jsonTransito"></param>
        /// <param name="fechaEntrada"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/openBascula/GuardarTransito")]       
        public bool GuardarTransito(TRANSITO_ACTUALES jsonTransito, string fechaEntrada)
        {
            jsonTransito.FECHA_ENTRADA = Convertidores.DateFromString(fechaEntrada);
            jsonTransito.MAT_CABINA = jsonTransito.MAT_CABINA.ToUpper();
            jsonTransito.MAT_REMOLQUE = jsonTransito.MAT_REMOLQUE.ToUpper();
            return Logic_Transitos.GuardarTransito(jsonTransito);
        }

        /// <summary>
        /// Carga un transito desde base de datos
        /// </summary>
        /// <param name="idTransito"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/VerTransito")]
        public TRANSITO_ACTUALES VerTransito(string matCabina, Nullable<int> idTransito)
        {
            TRANSITO_ACTUALES tactual = null;
            var listaTransitos = Logic_Transitos.ListarTransitosActuales(matCabina);

            if (idTransito.HasValue)
            {
                tactual = listaTransitos.Where(x => x.ID_TRANSITO == idTransito).FirstOrDefault();
            }
            return tactual;
        }
    }
}
