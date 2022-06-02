using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FeedbackGrupoPreguntaProgramaEspecificoDTO
    {
        public int Id { get; set; }
        //public int IdFeedbackConfigurarGrupoPregunta { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
    }
}
