
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using openBasculaNet.BusinessLogic.openBascula;
using openBasculaNet.Core.Structures;

namespace openBasculaNetWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Datos de contacto:";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Principal()
        {            
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult VehiculosEnTransito()
        {
            List<TRANSITO_ACTUALES> transitos = Logic_Transitos.ListarTransitosActuales();
            ViewBag.vehiculosEnTransito = transitos;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoBusqueda"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Buscador(Enumeraciones.TipoBuscador tipoBusqueda)
        {
            ViewBag.TipoBusqueda = tipoBusqueda;
            return View();
        }
    }
}