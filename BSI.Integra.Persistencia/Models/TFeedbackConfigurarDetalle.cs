using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFeedbackConfigurarDetalle
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public int IdSexo { get; set; }
        public int Puntaje { get; set; }
        public string NombreVideo { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? OrdenVideo { get; set; }

        public virtual TFeedbackConfigurar IdFeedbackConfigurarNavigation { get; set; }
        public virtual TSexo IdSexoNavigation { get; set; }
    }
}
