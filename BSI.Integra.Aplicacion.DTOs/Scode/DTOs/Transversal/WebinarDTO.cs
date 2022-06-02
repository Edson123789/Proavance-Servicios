using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WebinarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCursoCompleto { get; set; }
        public int IdExpositor { get; set; }
        public int? IdWebinarCategoriaConfirmacionAsistencia { get; set; }
        public int IdPersonal { get; set; }
        public int IdFrecuencia { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string LinkAulaVirtual { get; set; }
        public bool Activo { get; set; }
        public string NombreUsuario { get; set; }
        public List<WebinarCentroCostoDTO> ListaWebinarCentroCosto { get; set; }
        public List<WebinarDetalleDTO> ListaWebinarDetalle { get; set; }
        public WebinarDTO()
        {
            ListaWebinarCentroCosto = new List<WebinarCentroCostoDTO>();
            ListaWebinarDetalle = new List<WebinarDetalleDTO>();
        }
    }

    public class WebinarCentroCostoDTO
    {
        public int Id { get; set; }
        public int IdWebinar { get; set; }
        public int IdCentroCosto { get; set; }
        public bool Activo { get; set; }
    }

    public class WebinarDetalleDTO
    {
        public int Id { get; set; }
        public int IdWebinar { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Grupo { get; set; }
        public string Link { get; set; }
        public bool EsCancelado { get; set; }
    }
    public class WebinarDetalleCompuestoDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public List<WebinarAsistenciaCompuestoDTO> ListaWebinarAsistencia { get; set; }
        public WebinarDetalleCompuestoDTO()
        {
            ListaWebinarAsistencia = new List<WebinarAsistenciaCompuestoDTO>();
        }
    }

    public class WebinarAsistenciaCompuestoDTO
    {
        public int Id { get; set; }
        public int IdWebinarDetalle { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool? ConfirmoAsistencia { get; set; }
        public bool Asistio { get; set; }
    }
}
