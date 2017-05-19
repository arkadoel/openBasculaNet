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
        [Route("API/openBascula/GetTransitosTabla")]
        public List<Models.TransitoActualModel> GetTransitosTabla(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<TRANSITO_ACTUALES> transitos = Logic_Transitos.ListarTransitosActuales(filtro);
            List<Models.TransitoActualModel> listado = new List<Models.TransitoActualModel>();

            Models.TransitoActualModel tm = null;
            foreach (TRANSITO_ACTUALES transito in transitos)
            {
                tm = new Models.TransitoActualModel();
                tm.data = transito;
                tm.Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verTransito(" + tm.data.ID_TRANSITO + ", '"+ tm.data.MAT_CABINA + "')\"></span>";
                listado.Add(tm);
            }
            
            return listado;
        }

        /// <summary>
        /// Obtiene la lista de transitos
        /// </summary>
        /// <param name="filtro">Permite filtrar por matricula de cabina o remolque</param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/GetTransitos")]
        public List<TRANSITO_ACTUALES> GetTransitos(string filtro)
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
        /// Guarda los datos de un transito en base de datos
        /// </summary>
        /// <param name="jsonTransito"></param>
        /// <param name="fechaEntrada"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("API/openBascula/GuardarEnHistorico")]
        public int GuardarEnHistorico(TRANSITO_ACTUALES jsonTransito, string fechaEntrada)
        {
            jsonTransito.FECHA_ENTRADA = Convertidores.DateFromString(fechaEntrada);
            jsonTransito.MAT_CABINA = jsonTransito.MAT_CABINA.ToUpper();
            jsonTransito.MAT_REMOLQUE = jsonTransito.MAT_REMOLQUE.ToUpper();
            bool guardadoEnDB = Logic_Transitos.GuardarTransito(jsonTransito);

            if (guardadoEnDB)
            {
                return Logic_Transitos.GuardarTransitoEnHistorico(jsonTransito.ID_TRANSITO);
            }
            else return -1;

        }

        /// <summary>
        /// Carga un transito desde base de datos
        /// </summary>
        /// <param name="matCabina"></param>
        /// <param name="idTransito"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/VerTransito")]
        public Models.TransitoActualModel VerTransito(string matCabina, Nullable<int> idTransito)
        {
            TRANSITO_ACTUALES tactual = null;
            Models.TransitoActualModel mod = new Models.TransitoActualModel();
            var listaTransitos = Logic_Transitos.ListarTransitosActuales(matCabina);

            if (idTransito.HasValue)
            {
                tactual = listaTransitos.Where(x => x.ID_TRANSITO == idTransito).FirstOrDefault();
                mod.data = tactual;
                mod.TEXTO_PRODUCTO = Logic_Transitos.ObtenerProductoPorID(tactual.ID_PRODUCTO).NOMBRE;
                mod.TEXTO_AGENCIA = Logic_Transitos.ObtenerEmpresaPorID(tactual.ID_AGENCIA).NOMBRE;
                mod.TEXTO_CLIENTE = Logic_Transitos.ObtenerEmpresaPorID(tactual.ID_CLIENTE).NOMBRE;
                mod.TEXTO_POSEEDOR = Logic_Transitos.ObtenerEmpresaPorID(tactual.ID_POSEEDOR).NOMBRE;
                mod.TEXTO_PROVEEDOR = Logic_Transitos.ObtenerEmpresaPorID(tactual.ID_PROVEEDOR).NOMBRE;

                string nombreConductor = string.Format("{0} {1}", Logic_Transitos.ObtenerConductorPorID(tactual.ID_CONDUCTOR).NOMBRE,
                                                                  Logic_Transitos.ObtenerConductorPorID(tactual.ID_CONDUCTOR).APELLIDOS);
                mod.TEXTO_CONDUCTOR = nombreConductor;
            }
            return mod;
        }

    #region "   BUSCADORES"

        /// <summary>
        /// Carga un transito desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarPRODUCTO")]
        public List<Models.BuscadorModel> BuscarPRODUCTO(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaProductos = Logic_Transitos.ListarProductos(filtro);

            foreach(var producto in listaProductos)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + producto.ID_PRODUCTO + ", '" + producto.NOMBRE + "', 'PRODUCTO')\"></span>",
                    ID_elemento = producto.ID_PRODUCTO,
                    Descripcion = producto.NOMBRE
                };
                lista.Add(mod);
            }

            return lista;
        }

        /// <summary>
        /// Carga un cliente desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarCLIENTE")]
        public List<Models.BuscadorModel> BuscarCLIENTE(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaBuscado = Logic_Transitos.ListarEmpresas(filtro);

            foreach (var elemento in listaBuscado)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + elemento.ID_EMPRESA + ", '" + elemento.NOMBRE + "', 'CLIENTE')\"></span>",
                    ID_elemento = elemento.ID_EMPRESA,
                    Descripcion = elemento.NOMBRE
                };
                lista.Add(mod);
            }

            return lista;
        }

        /// <summary>
        /// Carga un POSEEEDOR desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarPOSEEDOR")]
        public List<Models.BuscadorModel> BuscarPOSEEDOR(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaBuscado = Logic_Transitos.ListarEmpresas(filtro);

            foreach (var elemento in listaBuscado)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + elemento.ID_EMPRESA + ", '" + elemento.NOMBRE + "', 'POSEEDOR')\"></span>",
                    ID_elemento = elemento.ID_EMPRESA,
                    Descripcion = elemento.NOMBRE
                };
                lista.Add(mod);
            }

            return lista;
        }

        /// <summary>
        /// Carga un PROVEEDOR desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarPROVEEDOR")]
        public List<Models.BuscadorModel> BuscarPROVEEDOR(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaBuscado = Logic_Transitos.ListarEmpresas(filtro);

            foreach (var elemento in listaBuscado)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + elemento.ID_EMPRESA + ", '" + elemento.NOMBRE + "', 'PROVEEDOR')\"></span>",
                    ID_elemento = elemento.ID_EMPRESA,
                    Descripcion = elemento.NOMBRE
                };
                lista.Add(mod);
            }

            return lista;
        }

        /// <summary>
        /// Carga un AGENCIA desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarAGENCIA")]
        public List<Models.BuscadorModel> BuscarAGENCIA(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaBuscado = Logic_Transitos.ListarEmpresas(filtro);

            foreach (var elemento in listaBuscado)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + elemento.ID_EMPRESA + ", '" + elemento.NOMBRE + "', 'AGENCIA')\"></span>",
                    ID_elemento = elemento.ID_EMPRESA,
                    Descripcion = elemento.NOMBRE
                };
                lista.Add(mod);
            }

            return lista;
        }

        /// <summary>
        /// Carga un CONDUCTOR desde base de datos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/BuscarCONDUCTOR")]
        public List<Models.BuscadorModel> BuscarCONDUCTOR(string filtro)
        {
            string style = "height: 24px !important; width: 24px !important; margin-right: 15px !important; font-size: 24px !important; vertical-align: middle !important; color: #00796b !important;";

            List<Models.BuscadorModel> lista = new List<Models.BuscadorModel>();
            Models.BuscadorModel mod = null;
            var listaBuscado = Logic_Transitos.ListarConductores(filtro);

            foreach (var elemento in listaBuscado)
            {
                mod = new Models.BuscadorModel()
                {
                    Ver = "<span class=\"glyphicon glyphicon-list-alt \" aria-hidden='true' style='" + style + "' onclick=\"verElemento(" + elemento.ID_CONDUCTOR + ", '" + (elemento.NOMBRE + " " + elemento.APELLIDOS) + "', 'CONDUCTOR')\"></span>",
                    ID_elemento = elemento.ID_CONDUCTOR,
                    Descripcion = elemento.NOMBRE + " " +  elemento.APELLIDOS
                };
                lista.Add(mod);
            }

            return lista;
        }

#endregion //buscadores

        /// <summary>
        /// Permite obtener los datos de una empresa dpendiendo de su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("API/openBascula/ObtenerEmpresaById")]
        public EMPRESAS ObtenerEmpresaById(int id)
        {
            EMPRESAS empresaBuscada = Logic_Transitos.ListarEmpresas().Where(x => x.ID_EMPRESA == id).FirstOrDefault();
            return empresaBuscada;
        }
    }
}
