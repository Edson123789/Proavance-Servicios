using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProcesoSeleccionEtapaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string NombreProcesoSeleccion { get; set; }
        public int NroOrden { get; set; }
        public string Usuario { get; set; }
    }
    public class EtapaProcesoSeleccionCalificadoDTO
    {
        public int IdEtapaProcesoSeleccionCalificado { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdProcesoSeleccion { get; set; }
    }

    public class ObtenerPostulantesProcesoSeleccionDTO
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public string ProcesoSeleccionEtapa { get; set; }
        public int NroOrden { get; set; }
        public int IdEtapaProcesoSeleccionCalificado { get; set; }
        public int IdEstadoEtapaProcesoSeleccion { get; set; }
        public string EstadoEtapaProcesoSeleccion { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public bool EsContactado { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }


    public class ObtenerCalificacionCentilDTO
    {
        public int Id { get; set; }
        public bool CalificaPorCentil { get; set; }
        public bool EsCalificable { get; set; }
        public int IdProcesoSeleccionRango { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public decimal? PuntajeMinimo { get; set; }
        public int? IdCentil { get; set; }
        public decimal? Centil { get; set; }
        public int? IdSexoCentil { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
    }

    public class ObtenerEtapaCalificacionesActivasDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public bool EsEtapaActual { get; set; }
        public int IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool EstadoPostulanteProcesoSeleccion { get; set; }
    }
}
