using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormaCalculoEvaluacion
    {
        public TFormaCalculoEvaluacion()
        {
            TEsquemaEvaluacion = new HashSet<TEsquemaEvaluacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsSuma { get; set; }
        public bool EsPromedio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TEsquemaEvaluacion> TEsquemaEvaluacion { get; set; }
    }
}
