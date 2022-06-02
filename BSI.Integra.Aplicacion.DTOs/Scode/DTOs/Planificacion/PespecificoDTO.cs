using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PespecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Frecuencia { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public string TipoAmbiente { get; set; }
        public string Categoria { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string FechaInicioV { get; set; }
        public string FechaTerminoV { get; set; }
        public string CodigoBanco { get; set; }
        public string FechaInicioP { get; set; }
        public string FechaTerminoP { get; set; }
        public int? FrecuenciaId { get; set; }
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? CategoriaId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
		public int? IdCursoMoodlePrueba { get; set; }
		public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
    }

    public class PespecificoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProgramaGeneral { get; set; }
    }

    public class ListaProgramaEspecificoPorIdProgramaDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string Nombre { get; set; }
        public string CentroCosto { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public string Duracion { get; set; }
        public int? EstadoPId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string FechaInicioTexto { get; set; }
        public string Frecuencia { get; set; }
        public int IdCategoria { get; set; }
        public string CodigoBanco { get; set; }
    }
}
