//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace openBasculaNet.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class VEHICULOS
    {
        public int ID_VEHICULO { get; set; }
        public string MATRICULA { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public Nullable<int> ID_CONDUCTOR { get; set; }
        public Nullable<System.DateTime> FECHA_ALTA { get; set; }
        public Nullable<System.DateTime> FECHA_BAJA { get; set; }
    }
}
