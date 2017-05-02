using openBasculaNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openBasculaNet.Core.DataAccess
{
    public class obnDB
    {
        private static openBasculaNet.Core.Structures.openBasculaNet2017Entities bbdd = null;
        public static openBasculaNet2017Entities Tablas
        {
            get
            {
                if (bbdd == null)
                { 
                    bbdd = new openBasculaNet2017Entities();
                    bbdd.Configuration.AutoDetectChangesEnabled = true;
                    bbdd.Configuration.ValidateOnSaveEnabled = false;
                }
                return bbdd;
            }
            set { bbdd = value; }
        }
        public static void Save()
        {
            bbdd.SaveChanges();
        }
    }
}
