using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEtapaProcesoSeleccionCalificado
    {
        public int Id { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public decimal? NotaCalculada { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EsEtapaActual { get; set; }
        public bool? EsContactado { get; set; }

        public virtual TEstadoEtapaProcesoSeleccion IdEstadoEtapaProcesoSeleccionNavigation { get; set; }
        public virtual TPostulante IdPostulanteNavigation { get; set; }
    }
}
