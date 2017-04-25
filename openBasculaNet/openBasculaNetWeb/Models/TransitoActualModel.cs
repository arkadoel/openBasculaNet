using openBasculaNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace openBasculaNetWeb.Models
{
    public class TransitoActualModel 
    {
        /// <summary>
        /// string que pone la ruta a la llamada para ver este elemento
        /// </summary>
        public string Ver { get; set; }

        public TRANSITO_ACTUALES data { get; set; }

        public TransitoActualModel()
        {

        }

        
    }
}