using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EvaluacionAgrupadaComponenteDTO
    {
        public int IdAsignacionPreguntaExamen { get; set; }
        public int IdComponente { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdEvaluacion { get; set; }
        public int IdPregunta { get; set; }
        public string NombreComponente { get; set; }
        public string NombreGrupoComponenteEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int NroOrden { get; set; }
    }
    public class NombreEvaluacionAgrupadaComponenteDTO
    {
        public int IdProcesoSeleccion{ get; set; }
        public bool CalificacionTotal { get; set; }
        public int? IdEvaluacion { get; set; }
        public string NombreEvaluacion  { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo{ get; set; }
        public int? IdComponente { get; set; }
        public string NombreComponente { get; set; }
        public decimal? Puntaje { get; set; }
        public bool CalificaPorCentil { get; set; }
        public bool CalificaAgrupadoNoIndependiente { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public bool EsCalificable { get; set; }
    }
    public class NombreEvaluacionesAgrupadaIndependienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool CalificacionTotal { get; set; }
        public bool CalificaAgrupadoNoIndependiente { get; set; }
    }

    public class PuntajeEvaluacionAgrupadaComponenteDTO
    {
        public List<NombreEvaluacionAgrupadaComponenteDTO> ListaPuntaje { get; set; }
        public string Usuario { get; set; }
    }
}
