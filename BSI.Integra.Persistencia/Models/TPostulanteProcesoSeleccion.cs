using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteProcesoSeleccion
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
    }
}
