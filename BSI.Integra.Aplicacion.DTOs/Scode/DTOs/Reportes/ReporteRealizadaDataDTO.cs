using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteRealizadaDataDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public int? IdLlamadaWebphone { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string NombreGrabacionIntegra { get; set; }
        public string Webphone { get; set; }
        public int? IdTresCX { get; set; }
        public int? DuracionContestoTresCx { get; set; }
        public int? DuracionTimbradoTresCx { get; set; }
        public DateTime? FechaInicioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string NombreGrabacionTresCX { get; set; }
    }
}
