using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TActividadCertificadoGeneradoAutomaticoLog
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public string Host { get; set; }
        public string Accion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
