using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public partial class SedeBO : BaseEntity
    {
        public int IdPais { get; set; }
        public string Codigo { get; set; }
        public int IdCiudad { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
