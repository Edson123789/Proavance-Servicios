using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoCambioCol
    {
        public TTipoCambioCol()
        {
            TTipoCambioMoneda = new HashSet<TTipoCambioMoneda>();
        }

        public int Id { get; set; }
        public double PesosDolares { get; set; }
        public double DolaresPesos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TTipoCambioMoneda> TTipoCambioMoneda { get; set; }
    }
}
