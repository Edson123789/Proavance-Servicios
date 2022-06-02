using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraLogFinanzas
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdEstadoPagoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string EmpresaRuc { get; set; }
        public string EmpresaNombre { get; set; }
        public string EmpresaContacto { get; set; }
        public string EmpresaEmail { get; set; }
        public bool? EmpresaPaga { get; set; }
        public string EmpresaObservaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdPersonalCoordinadorAcademico { get; set; }
        public int? IdPersonalAsesor { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string FechaSuspendido { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string ObservacionGeneralOperaciones { get; set; }
        public string UsuarioCoordinadorSupervision { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
        public int? IdPeriodo { get; set; }
        public string UsuarioCoordinadorPreAsignacion { get; set; }
        public bool? VerificacionConforme { get; set; }
        public bool? FechaMatriculaValidada { get; set; }
        public bool? FechaPagoValidada { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? GrupoCurso { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdPaquete { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
    }
}
