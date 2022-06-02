using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PostulanteAccesoTemporalAulaVirtualDTO
    {
        public class EnviarAccesoPostulanteDTO
        {
            public int IdPostulante { get; set; }
            public int IdExamen { get; set; }
            public int IdPlantilla { get; set; }
            public string Usuario { get; set; }

        }
        public class InformacionAccesoPostulanteDTO
        {
            public int? IdAlumno { get; set; }
            public string Usuario { get; set; }
            public string Clave { get; set; }
            public bool ValidacionRespuesta { get; set; }
        }
        public class RespuestaAccesosPostulanteDTO
        {
            public int? IdAlumno { get; set; }
            public string Email { get; set; }
            public string Clave { get; set; }
        }
        public class AccesosRegistradosPostulanteDTO
        {
            public int IdPostulante { get; set; }
            public string Postulante { get; set; }
            public string NroDocumento { get; set; }
            public bool? EstadoAcceso { get; set; }
            public int? IdExamen { get; set; }
            public string Examen { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }
        public class FiltroReporteAccesosTemporalesDTO
        {
            public List<int> ListaPostulante { get; set; }
            public int? IdProcesoSeleccion { get; set; }
            public List<int> ListaEtapaProceso { get; set; }
            public List<int> ListaEstadoEtapa { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
            public int? EstadoAcceso { get; set; }
            public string NroDocumento { get; set; }
        }
        public class RespuestaProcesoSeleccionPostulanteDTO
        {
            public int IdPostulante { get; set; }
            public int IdProcesoSeleccion { get; set; }
        }
        public class RespuestaIdPostulanteDTO
        {
            public int IdPostulante { get; set; }
        }
        public class RespuestaAccesoDTO
        {
            public int IdPostulante { get; set; }
            public string Postulante { get; set; }
            public string NroDocumento { get; set; }
            public List<RespuestaAccesoAgrupadoDTO> Agrupado { get; set; }

        }
        public class RespuestaAccesoAgrupadoDTO
        {
            public int? IdExamen { get; set; }
            public string Examen { get; set; }
            public bool EstadoAcceso { get; set; }
            public DateTime? FechaInicio { get; set; }
            public DateTime? FechaFin { get; set; }
        }
    }
}
