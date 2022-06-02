using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class registroFeedbackConfigurarGrupoPreguntaDTO
    {
        public FeedbackConfigurarGrupoPreguntaDTO configuracionFeedbackGrupo { get; set; }
        public List<FeedbackGrupoPreguntaProgramaGeneralDTO> configuracionFeedbackProgramaGeneral { get; set; }
        public List<FeedbackGrupoPreguntaProgramaEspecificoDTO> configuracionProgramaEspecifico { get; set; }
        public string Usuario { get; set; }
    }

    public class FeedbackConfigurarGrupoPreguntaDTO
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
    }

    public class listaFeedbackConfigurarGrupoPregunta
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public string Nombre { get; set; }
    }
}
