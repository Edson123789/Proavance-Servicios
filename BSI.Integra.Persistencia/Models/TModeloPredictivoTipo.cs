using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloPredictivoTipo
    {
        public TModeloPredictivoTipo()
        {
            TModeloPredictivoProbabilidad = new HashSet<TModeloPredictivoProbabilidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public virtual ICollection<TModeloPredictivoProbabilidad> TModeloPredictivoProbabilidad { get; set; }
    }
}
