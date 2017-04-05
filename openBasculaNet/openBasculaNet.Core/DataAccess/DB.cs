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
        private static openBasculaNet.Core.Structures.openBasculaNet2017Entities1 bbdd = null;
        public static openBasculaNet2017Entities1 Tablas
        {
            get
            {
                if (bbdd == null)
                { 
                    bbdd = new openBasculaNet2017Entities1();
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
