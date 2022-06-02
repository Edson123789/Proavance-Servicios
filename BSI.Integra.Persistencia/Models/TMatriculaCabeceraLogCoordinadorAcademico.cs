using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraLogCoordinadorAcademico
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdEstadoPagoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdPersonalCoordinadorAcademico { get; set; }
        public int? IdPersonalAsesor { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string FechaSuspendido { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string UsuarioCoordinadorSupervision { get; set; }
        public int? IdCronograma { get; set; }
        public int? IdPeriodo { get; set; }
        public string UsuarioCoordinadorPreAsignacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
