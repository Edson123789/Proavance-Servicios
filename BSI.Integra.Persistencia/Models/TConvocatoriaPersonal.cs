using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConvocatoriaPersonal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string CuerpoConvocatoria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public int? IdArea { get; set; }
        public string UrlAviso { get; set; }
        public int? IdPersonal { get; set; }

        public virtual TArea IdAreaNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; }
        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual TSedeTrabajo IdSedeTrabajoNavigation { get; set; }
    }
}
