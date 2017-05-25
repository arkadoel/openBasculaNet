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
        /// Guarda los datos de camion en transito en el historial y elimina 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GuardarTransitoEnHistorico(int idTransito)
        {
            if (manzana.LicenciaActiva())
            {
                try
                {
                    TRANSITO_ACTUALES transitoDB = null;
                    transitoDB = obnDB.Tablas.TRANSITO_ACTUALES.Where(x => x.ID_TRANSITO == idTransito).FirstOrDefault();

                    HISTORICOS historico = new HISTORICOS();
                    #region Parsear el transito a objeto historico para guardarlo en historicos

                    historico.NUM_ALBARAN = transitoDB.NUM_ALBARAN;
                    historico.MAT_CABINA = transitoDB.MAT_CABINA;
                    historico.MAT_REMOLQUE = transitoDB.MAT_REMOLQUE;
                    historico.PESO_ENTRADA = transitoDB.PESO_ENTRADA;
                    historico.PESO_SALIDA = transitoDB.PESO_SALIDA;
                    historico.NETO = transitoDB.NETO;
                        
                    historico.ORIGEN = transitoDB.ORIGEN;
                    historico.DESTINO = transitoDB.DESTINO;
                    historico.FECHA_ENTRADA = transitoDB.FECHA_ENTRADA;
                    historico.FECHA_SALIDA = DateTime.Now;

                    var agencia = obnDB.Tablas.EMPRESAS.Where(x =>x.ID_EMPRESA == transitoDB.ID_AGENCIA).First();
                    historico.CIF_AGENCIA = agencia.CIF;
                    historico.COD_CONTA_AGENCIA = agencia.CODIGO_CONTABLE;
                    historico.DIRECCION_AGENCIA = agencia.DIRECCION;
                    historico.RAZON_SOCIAL_AGENCIA = agencia.RAZON_SOCIAL;
                    historico.TLF_AGENCIA = agencia.TELEFONO;
                    historico.UBICACION_AGENCIA = agencia.PROVINCIA;

                    var cliente = obnDB.Tablas.EMPRESAS.Where(x => x.ID_EMPRESA == transitoDB.ID_CLIENTE).First();
                    historico.CIF_CLIENTE = cliente.CIF;
                    historico.COD_CONTA_CLIENTE = cliente.CODIGO_CONTABLE;
                    historico.DIRECCION_CLIENTE = cliente.DIRECCION;
                    historico.RAZON_SOCIAL_CLIENTE = cliente.RAZON_SOCIAL;
                    historico.TLF_CLIENTE = cliente.TELEFONO;
                    historico.UBICACION_CLIENTE = cliente.PROVINCIA;

                    var conductor = obnDB.Tablas.CONDUCTORES.Where(x => x.ID_CONDUCTOR == transitoDB.ID_CONDUCTOR).First();
                    historico.DIRECCION_CONDUCTOR = conductor.DIRECCION;
                    historico.NIF_CONDUCTOR = conductor.NIF;
                    historico.NOMBRE_CONDUCTOR = conductor.NOMBRE + " " + conductor.APELLIDOS;
                    historico.TLF_CONDUCTOR = conductor.TELEFONO;
                    historico.UBICACION_CONDUCTOR = conductor.PROVINCIA;

                    var POSEEDOR = obnDB.Tablas.EMPRESAS.Where(x => x.ID_EMPRESA == transitoDB.ID_POSEEDOR).First();
                    historico.CIF_POSEEDOR = POSEEDOR.CIF;
                    historico.COD_CONTA_POSEEDOR = POSEEDOR.CODIGO_CONTABLE;
                    historico.DIRECCION_POSEEDOR = POSEEDOR.DIRECCION;
                    historico.RAZON_SOCIAL_POSEEDOR = POSEEDOR.RAZON_SOCIAL;
                    historico.TLF_POSEEDOR = POSEEDOR.TELEFONO;
                    historico.UBICACION_POSEEDOR = POSEEDOR.PROVINCIA;

                    var producto = obnDB.Tablas.PRODUCTOS.Where(x=> x.ID_PRODUCTO == transitoDB.ID_PRODUCTO).First();
                    historico.IMPORTE_PRODUCTO = producto.PRECIO.ToString();
                    historico.NOMBRE_PRODUCTO = producto.NOMBRE;

                    var PROVEEDOR = obnDB.Tablas.EMPRESAS.Where(x => x.ID_EMPRESA == transitoDB.ID_PROVEEDOR).First();
                    historico.CIF_PROVEEDOR = PROVEEDOR.CIF;
                    historico.COD_CONTA_PROVEEDOR = PROVEEDOR.CODIGO_CONTABLE;
                    historico.DIRECCION_PROVEEDOR = PROVEEDOR.DIRECCION;
                    historico.RAZON_SOCIAL_PROVEEDOR = PROVEEDOR.RAZON_SOCIAL;
                    historico.TLF_PROVEEDOR = PROVEEDOR.TELEFONO;
                    historico.UBICACION_PROVEEDOR = PROVEEDOR.PROVINCIA;

                    #endregion //Parsear el transito a objeto historico para guardarlo en historicos

                    obnDB.Tablas.HISTORICOS.Add(historico);
                    obnDB.Save();

                    //Si se ha guardado con exito eliminamos el transito actual
                    obnDB.Tablas.TRANSITO_ACTUALES.Remove(transitoDB);
                    obnDB.Save();

                    return historico.ID_HISTORICO;                    
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            else return -1;
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

        /// <summary>
        /// Permite obtener un producto segun su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PRODUCTOS ObtenerProductoPorID(int? id)
        {
            PRODUCTOS obj = new PRODUCTOS();

            if (manzana.LicenciaActiva() && id.HasValue)
            {
                var query = obnDB.Tablas.PRODUCTOS.Where(x => (x.ID_PRODUCTO == id)).FirstOrDefault();

                if (query != null) obj = query;
            }
            return obj;
        }

        /// <summary>
        /// obtiene un historico
        /// </summary>
        /// <param name="idHistorico"></param>
        /// <returns></returns>
        public static HISTORICOS ObtenerHistoricoPorID(int idHistorico)
        {
            HISTORICOS obj = new HISTORICOS();

            if (manzana.LicenciaActiva())
            {
                var query = obnDB.Tablas.HISTORICOS.Where(x => (x.ID_HISTORICO == idHistorico)).FirstOrDefault();

                if (query != null) obj = query;
            }
            return obj;
        }

        /// <summary>
        /// Listar las empresas segun un filtro
        /// </summary>
        /// <param name="filtro">filtro por nombre (opcional)</param>
        /// <returns></returns>
        public static List<EMPRESAS> ListarEmpresas(string filtro = "")
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

        /// <summary>
        /// Obtener una empresa por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EMPRESAS ObtenerEmpresaPorID(int? id )
        {
            EMPRESAS cliente = new EMPRESAS();

            if (manzana.LicenciaActiva() && id.HasValue)
            {
                var empresa = obnDB.Tablas.EMPRESAS.Where(x => (x.ID_EMPRESA == id)).FirstOrDefault();

                if (empresa != null) cliente = empresa;
            }
            return cliente;
        }

        /// <summary>
        /// Listar los conductores
        /// </summary>
        /// <param name="filtro">filtro por nombre (opcional)</param>
        /// <returns></returns>
        public static List<CONDUCTORES> ListarConductores(string filtro = "")
        {
            List<CONDUCTORES> conductor = new List<CONDUCTORES>();

            if (manzana.LicenciaActiva())
            {
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    conductor = obnDB.Tablas.CONDUCTORES.ToList();
                }
                else
                {
                    filtro = filtro.ToUpper();
                    conductor = obnDB.Tablas.CONDUCTORES
                                    .Where(
                                            x => (x.NOMBRE.Contains(filtro) || x.APELLIDOS.Contains(filtro))
                                    ).ToList();
                }
            }
            return conductor;
        }

        /// <summary>
        /// Obtener un conductor por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CONDUCTORES ObtenerConductorPorID(int? id)
        {
            CONDUCTORES obj = new CONDUCTORES();

            if (manzana.LicenciaActiva() && id.HasValue)
            {
                var query = obnDB.Tablas.CONDUCTORES.Where(x => (x.ID_CONDUCTOR == id)).FirstOrDefault();

                if (query != null) obj = query;
            }
            return obj;
        }
    }
}
