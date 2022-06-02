using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosSolicitudOperacionesDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string PersonalAprobacion { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public bool Aprobado { get; set; }
        public bool EsCancelado { get; set; }
        public string ComentarioSolicitante { get; set; }
        public string Observacion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string UrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string ObservacionEncargado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Usuario { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
    }

    public class DatosAlumnosEnvioAutomaticoOperacionesDTO
    {
        public int IdSolicitudOperaciones { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdOportunidad { get; set; }
        public string ValorAnteriorSubEstado { get; set; }
        public string ValorNuevoSubEstado { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }        
        public int IdPersonalSolicitante { get; set; }       
        public DateTime FechaAprobacion { get; set; }
        public bool? EnvioAutomatico { get; set; }
        public string NombrePais { get; set; }
        public int? ZonaHoraria { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
        public string Email { get; set; }
        public string ValorAnteriorEstado { get; set; }
        public string ValorNuevoEstado { get; set; }

    }
}
