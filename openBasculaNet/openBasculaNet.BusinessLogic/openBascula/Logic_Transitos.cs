using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using openBasculaNet.Core.Structures;
using openBasculaNet.Core.DataAccess;

namespace openBasculaNet.BusinessLogic.openBascula
{
    public class Logic_Transitos
    {
        /// <summary>
        /// Guarda los datos para un camion en transito
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GuardarTransito(TRANSITO_ACTUALES obj)
        {
            if (manzana.LicenciaActiva())
            {
                try
                {
                    TRANSITO_ACTUALES acDB = null;
                    if (obj.ID_TRANSITO != -1)
                    {
                        acDB = obnDB.Tablas.TRANSITO_ACTUALES.Where(x => x.ID_TRANSITO == obj.ID_TRANSITO).FirstOrDefault();
                    }
                   
                    if (acDB != null)
                    {
                        if (acDB.Equals(obj) == false)
                        {
                            //obnDB.Tablas.Entry(acDB).Reload();
                            try
                            {
                                obnDB.Tablas.Entry(acDB).CurrentValues.SetValues(obj);
                                obnDB.Save();
                            }
                            catch (Exception ex)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        obnDB.Tablas.TRANSITO_ACTUALES.Add(obj);
                        obnDB.Save();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }

        /// <summary>
        /// Listar los vehiculos en transito
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static List<TRANSITO_ACTUALES> ListarTransitosActuales(string filtro = "")
        {
            List<TRANSITO_ACTUALES> transitos = new List<TRANSITO_ACTUALES>();

            if (manzana.LicenciaActiva())
            {                
                if (string.IsNullOrWhiteSpace(filtro))
                {                    
                    transitos = obnDB.Tablas.TRANSITO_ACTUALES.ToList();
                }
                else
                {
                    filtro = filtro.ToUpper();
                    transitos = obnDB.Tablas.TRANSITO_ACTUALES
                                    .Where(
                                            x => (x.MAT_CABINA.Contains(filtro) || x.MAT_REMOLQUE.Contains(filtro))
                                    ).ToList();
                }

            }
            return transitos;
        }
    }
}
