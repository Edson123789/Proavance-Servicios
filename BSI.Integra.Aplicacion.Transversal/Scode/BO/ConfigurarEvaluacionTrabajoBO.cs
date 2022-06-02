using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfigurarEvaluacionTrabajoBO : BaseBO
    {
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdDocumentoPw { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoCarpeta { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        public string DescripcionPregunta { get; set; }
        public int OrdenCapitulo { get; set; }
        public bool HabilitarInstrucciones { get; set; }
        public bool HabilitarArchivo { get; set; }
        public bool HabilitarPreguntas { get; set; }
    }

    public class listaConfigurarEvaluacionTrabajoBO
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
        public int OrdenCapitulo { get; set; }
        public bool HabilitarInstrucciones { get; set; }
        public bool HabilitarArchivo { get; set; }
        public bool HabilitarPreguntas { get; set; }
        public string DescripcionPregunta { get; set; }
        public string NombreTipoEvaluacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class registroConfigurarEvaluacionTrabajoBO
    {
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdDocumentoPw { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoCarpeta { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        public string DescripcionPregunta { get; set; }

        public List<registroPreguntaEvaluacionTrabajoBO> listaPreguntas { get; set; }
    }

    public class registrosConfigurarEvaluacionTrabajoBO
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
        public int OrdenCapitulo { get; set; }
        public bool HabilitarInstrucciones { get; set; }
        public bool HabilitarArchivo { get; set; }
        public bool HabilitarPreguntas { get; set; }
        public string DescripcionPregunta { get; set; }
        public string NombreTipoEvaluacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }

        public List<registroPreguntaTipoEvaluacionBO> PreguntasEvaluacion { get; set; }
        public List<registroDocumentoSeccionesBO> InstruccionesEvaluacion { get; set; }
    }

    public class registroDocumentoSeccionesBO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int OrdenWeb { get; set; }
        public int ZonaWeb { get; set; }
    }

    public class registroPreguntaTipoEvaluacionBO
    {
        public int IdPreguntaEvaluacionTrabajo { get; set; }
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdPEspecifico { get; set; }
    }
}
