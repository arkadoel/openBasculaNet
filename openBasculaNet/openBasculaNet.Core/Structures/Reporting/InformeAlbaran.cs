using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openBasculaNet.Core.Structures.Reporting
{
    public class InformeAlbaran : ReportParameters
    {
        public int IdHistorico { get; set; }

        public Dictionary<string, string> ParametrosReporte
        {
            get;
            set;
        }
    }
}
