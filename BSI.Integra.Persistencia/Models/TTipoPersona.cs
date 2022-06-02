using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoPersona
    {
        public TTipoPersona()
        {
            TClasificacionPersona = new HashSet<TClasificacionPersona>();
            TCriterioEvaluacionTipoPersona = new HashSet<TCriterioEvaluacionTipoPersona>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreTablaOriginal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TClasificacionPersona> TClasificacionPersona { get; set; }
        public virtual ICollection<TCriterioEvaluacionTipoPersona> TCriterioEvaluacionTipoPersona { get; set; }
    }
}
