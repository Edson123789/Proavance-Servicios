using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabecera
    {
        public TMatriculaCabecera()
        {
            TComisionMontoPago = new HashSet<TComisionMontoPago>();
            TContenidoDatoAdicional = new HashSet<TContenidoDatoAdicional>();
            TConvalidacionNota = new HashSet<TConvalidacionNota>();
            TMatriculaCabeceraControlCondicionesComision = new HashSet<TMatriculaCabeceraControlCondicionesComision>();
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperaciones>();
            TReclamo = new HashSet<TReclamo>();
            TRecuperacionSesion = new HashSet<TRecuperacionSesion>();
            TSolicitudCertificadoFisico = new HashSet<TSolicitudCertificadoFisico>();
            TWebinarAsistencia = new HashSet<TWebinarAsistencia>();
            TWebinarExcluir = new HashSet<TWebinarExcluir>();
        }

        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int IdEstadoPagoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public string EmpresaRuc { get; set; }
        public string EmpresaNombre { get; set; }
        public string EmpresaContacto { get; set; }
        public string EmpresaEmail { get; set; }
        public string EmpresaPaga { get; set; }
        public string EmpresaObservaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdCoordinador { get; set; }
        public int? IdAsesor { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string FechaSuspendido { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string ObservacionGeneralOperaciones { get; set; }
        public string UsuarioCoordinadorSupervision { get; set; }
        public int? IdCronograma { get; set; }
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
        public string IdMigracion { get; set; }
        public int? GrupoCurso { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdPaquete { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? IdEstadoMatriculaCertificado { get; set; }
        public int? IdSubEstadoMatriculaCertificado { get; set; }
        public bool? EsInhouse { get; set; }

        public virtual ICollection<TComisionMontoPago> TComisionMontoPago { get; set; }
        public virtual ICollection<TContenidoDatoAdicional> TContenidoDatoAdicional { get; set; }
        public virtual ICollection<TConvalidacionNota> TConvalidacionNota { get; set; }
        public virtual ICollection<TMatriculaCabeceraControlCondicionesComision> TMatriculaCabeceraControlCondicionesComision { get; set; }
        public virtual ICollection<TOportunidadClasificacionOperaciones> TOportunidadClasificacionOperaciones { get; set; }
        public virtual ICollection<TReclamo> TReclamo { get; set; }
        public virtual ICollection<TRecuperacionSesion> TRecuperacionSesion { get; set; }
        public virtual ICollection<TSolicitudCertificadoFisico> TSolicitudCertificadoFisico { get; set; }
        public virtual ICollection<TWebinarAsistencia> TWebinarAsistencia { get; set; }
        public virtual ICollection<TWebinarExcluir> TWebinarExcluir { get; set; }
    }
}
