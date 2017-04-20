using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openBasculaNet.Core.Utiles
{
    public class Convertidores
    {
        public static DateTime? DateFromString(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            else
            {
                try
                {
                    return DateTime.Parse(data);
                }
                catch { return null; }
            }
        }

        public static string StringReverseDateFromDateTime(DateTime fecha)
        {
            return string.Format("{0}/{1}/{2}", fecha.Year, fecha.Month, fecha.Day);
        }

    }
}
