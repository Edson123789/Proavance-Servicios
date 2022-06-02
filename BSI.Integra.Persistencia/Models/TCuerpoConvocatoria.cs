using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCuerpoConvocatoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string Cuerpo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; }
    }
}
