using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfigurarEvaluacionTrabajoDTO
    {

    }

    public class configurarEvaluacionDTO
    {
        public registroConfigurarEvaluacionTrabajoDTO ConfigurarEvaluacion { get; set; }
        public List<registroPreguntasProgramaEstructuraDTO> listaPreguntas { get; set; }
        public string Usuario { get; set; }
        
    }

    public class registroConfigurarEvaluacionTrabajoDTO
    {
        public int Id { get; set; }
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdDocumentoPw { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoCarpeta { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        public bool Estado { get; set; }
        public string DescripcionPregunta { get; set; }
        public int OrdenCapitulo { get; set; }
        public bool HabilitarInstrucciones { get; set; }
        public bool HabilitarArchivo { get; set; }
        public bool HabilitarPreguntas { get; set; }
        //public List<registroPreguntaEvaluacionTrabajoDTO> listaPreguntas { get; set; }

    }

    public class registroPreguntasProgramaEstructuraDTO
    {
        public int Id { get; set; }
        public string EnunciadoPregunta { get; set; }

    }

    public class registroDocumentoProgramaEstructuraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
