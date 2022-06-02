using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FeedbackConfigurarDetalleDTO
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public int IdSexo { get; set; }
        public int Puntaje { get; set; }
        public string NombreVideo { get; set; }
    }
}
