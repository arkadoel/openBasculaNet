using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using openBasculaNet.Core.DataAccess;

namespace openBasculaNet.BusinessLogic.openBascula
{
    public class Logic_Administracion
    {
        /// <summary>
        /// Obtener nombre real de un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getUserRealName(string id)
        { 
            string name = "";
            try
            {
                name = obnDB.Tablas.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().RealName;
            }
            catch { }
            return (string.IsNullOrWhiteSpace(name) ? "" : name);
        }

        /// <summary>
        /// Obtener rol de un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getUserRole(string id)
        {
            try
            { 
                return obnDB.Tablas.AspNetUsers.Where(x => x.Id == id).First().AspNetRoles.First().Name.ToUpper();
            }
            catch { return "USER"; }
        }

        /// <summary>
        /// Poner nombre real a un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="realName"></param>
        public static void setUserRealName(string id, string realName)
        {
            obnDB.Tablas.AspNetUsers.Where(x => x.Id == id).First().RealName = realName;
            obnDB.Save();
        }

        /// <summary>
        /// Asignacion de rol a un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool SetRoleToUser(string userId, string roleName)
        {
            bool retorno = false;
            try
            {
                var usuario = obnDB.Tablas.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
                if (usuario != null)
                {
                    var role = obnDB.Tablas.AspNetRoles.Where(x => x.Name.ToUpper() == roleName.ToUpper()).FirstOrDefault();
                    if (role != null)
                    {
                        usuario.AspNetRoles.Add(role);
                        obnDB.Save();
                        retorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = false;
            }
            return retorno;

        }

        /// <summary>
        /// Permite eliminar un usuario sabiendo su id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool DeleteUser(string userId)
        {
            bool res = false;
            try
            {
                var userDie = obnDB.Tablas.AspNetUsers.Where(x => x.Id == userId).FirstOrDefault();
                if (userDie != null)
                {
                    obnDB.Tablas.AspNetUsers.Remove(userDie);
                    obnDB.Save();
                }
                res = true;
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }
    }

    
}
