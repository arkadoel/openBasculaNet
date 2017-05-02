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
                            obnDB.Tablas.Entry(acDB).Reload();
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
                        TRANSITO_ACTUALES t = new TRANSITO_ACTUALES();
                        obj.ID_TRANSITO = 0;
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

        /// <summary>
        /// Lista unos productos dependiendo de un filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static List<PRODUCTOS> ListarProductos(string filtro = "")
        {
            List<PRODUCTOS> productos = new List<PRODUCTOS>();

            if (manzana.LicenciaActiva())
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    productos = obnDB.Tablas.PRODUCTOS.ToList();
                }
                else
                {
                    filtro = filtro.ToUpper();
                    productos = obnDB.Tablas.PRODUCTOS
                                    .Where(
                                            x => (x.NOMBRE.Contains(filtro))
                                    ).ToList();
                }

            }
            return productos;
        }

        public static List<EMPRESAS> ListarEmpresas(string filtro)
        {
            List<EMPRESAS> clientes = new List<EMPRESAS>();

            if (manzana.LicenciaActiva())
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    clientes = obnDB.Tablas.EMPRESAS.ToList();
                }
                else
                {
                    filtro = filtro.ToUpper();
                    clientes = obnDB.Tablas.EMPRESAS
                                    .Where(
                                            x => (x.NOMBRE.Contains(filtro))
                                    ).ToList();
                }

            }
            return clientes;
        }

      

       
    }
}
