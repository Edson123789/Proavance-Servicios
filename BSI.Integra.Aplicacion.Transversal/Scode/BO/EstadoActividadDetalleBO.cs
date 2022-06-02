using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class EstadoActividadDetalleBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }
		public Guid? IdMigracion { get; set; }

		public static int EstadoActividadDetalleEjecutado = 2;
        public static int EstadoActividadDetalleNoEjecutado = 1;
    }
}
