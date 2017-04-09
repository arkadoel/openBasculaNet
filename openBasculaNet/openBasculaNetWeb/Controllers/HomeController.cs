
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using openBasculaNet.BusinessLogic.openBascula;
using openBasculaNet.Core.Structures;

namespace openBasculaNetWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Datos de contacto:";

            return View();
        }

        public ActionResult Principal()
        {
            return View();
        }

        public ActionResult VehiculosEnTransito()
        {
            List<TRANSITO_ACTUALES> transitos = Logic_Transitos.ListarTransitosActuales();
            ViewBag.vehiculosEnTransito = transitos;
            return View();
        }
    }
}