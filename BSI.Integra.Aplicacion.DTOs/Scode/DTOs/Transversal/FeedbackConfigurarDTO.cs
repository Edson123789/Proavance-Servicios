using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FeedbackConfigurarRegistroDTO
    {
        public FeedbackConfigurarDTO FeedbackConfigurar { get; set; }
        public List<FeedbackConfigurarDetalleDTO> FeedbackConfigurarDetalle { get; set; }
        public List<FeedbackConfigurarDetalleDTO> FeedbackConfigurarDetalleEliminar { get; set; }
        public string Usuario { get; set; }
    }

    public class FeedbackConfigurarDTO
    {
        public int Id { get; set; }
        public int IdFeedbackTipo { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }

    public class FeedbackConfigurarFiltroDTO
    {
        public int Id { get; set; }
        public int IdFeedbackTipo { get; set; }
        public string NombreFeedbackTipo { get; set; }
        public string Nombre { get; set; }
    }
}
