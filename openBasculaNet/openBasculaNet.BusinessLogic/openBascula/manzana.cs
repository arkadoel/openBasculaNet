using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openBasculaNet.BusinessLogic.openBascula
{
    /// <summary>
    /// Clase encargada de poner caducidad a la aplicacion, para incentivar que actualicen y no 
    /// se queden obsoletas las aplicaciones tras decadas de uso, como pasa actualmente.
    /// </summary>
    public class manzana
    {
        enum Meses
        {
            ENERO = 1,
            FEBRERO = 2,
            MARZO = 3,
            ABRIL = 4,
            MAYO = 5,
            JUNIO = 6,
            JULIO = 7,
            AGOSTO = 8,
            SEPTIEMBRE = 9,
            OCTUBRE = 10,
            NOVIEMBRE = 11,
            DICIEMBRE = 12
        }

        private const int an = 1000 + 1019;
        private const int m = (int)Meses.ENERO;
        private const int d = 1;

        public static Boolean LicenciaActiva()
        {
            DateTime fechaFin = new DateTime(an, m, d);
            DateTime ahora = DateTime.Now;
            if (fechaFin >= ahora)
            {
                Console.WriteLine("Licencia caducada");
                return true;
            }
            else
            {
                Console.WriteLine("Licencia vigente");
                return false;
            }
        }

        /// <summary>
        /// retorna el tiempo restante en meses que queda de licencia
        /// </summary>
        /// <returns></returns>
        public static int TiempoRestante()
        {
            DateTime fechaFin = new DateTime(an, m, d);
            DateTime ahora = DateTime.Now;
            TimeSpan tiempoRestante = fechaFin - ahora;
            int meses = (int)tiempoRestante.Days / 30;
            Console.WriteLine("La licencia caduca en {0} meses", meses);
            return meses;
        }

        public static string FallosSegunTiempoRestante()
        {
            //Mouse.OverrideCursor = Cursors.Wait;
            bool mostrarMensaje = false;
            int tiempo = TiempoRestante();
            if (tiempo == 6)
            {
                //si el tiempo es mayor de seis meses
                mostrarMensaje = true;
            }
            else if (tiempo == 4 || tiempo == 3)
            {
                Console.WriteLine("Congelacion 5º");
                //Congelar(5);
                mostrarMensaje = true;
            }
            else if (tiempo == 2)
            {
                Console.WriteLine("Congelacion 15º");
                //Congelar(15);
                mostrarMensaje = true;
            }
            else if (tiempo == 1)
            {
                Console.WriteLine("Congelacion 30º");
                //Congelar(30);
                mostrarMensaje = true;
            }
            //Mouse.OverrideCursor = null;
            if (mostrarMensaje == true)
            {
                // System.Windows.MessageBox.Show("Se han detectado fallos en el proceso, contacte con el servicio tecnico.", "Fallos encontrados", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return "Se han detectado fallos en el proceso, contacte con el servicio tecnico.";
            }
            else return "sucess";
        }

        private static void Congelar(int segundos)
        {
            System.Threading.Thread.Sleep(segundos * 1000);
        }
    }

}
