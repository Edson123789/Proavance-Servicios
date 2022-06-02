using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosListaPespecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBanco { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public int? EstadoPId { get; set; }
        public int? TipoId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string TipoAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
		public string TipoSesion { get; set; }
		public int? IdCursoMoodlePrueba { get; set; }
        public string TipoProgramaGeneral { get; set; }
    }
}
