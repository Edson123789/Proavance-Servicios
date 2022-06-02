using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TodoSolicitudOperacionesDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public string EmailSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string PersonalAprobacion { get; set; }
        public string EmailAprobador { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
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
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string CodigoMatricula { get; set; }
        public string CentroCosto { get; set; }
        public string Pespecifico { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Usuario { get; set; }
    }

    public class LIstaDiasProgramadosDTO
    {
        public string ObservacionEncargado { get; set; }
    }
}
