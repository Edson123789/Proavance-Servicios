using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoCambioMoneda
    {
        public int Id { get; set; }
        public double MonedaAdolar { get; set; }
        public double DolarAmoneda { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoCambioCol { get; set; }
        public int? IdTipoCambio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMoneda IdMonedaNavigation { get; set; }
        public virtual TTipoCambioCol IdTipoCambioColNavigation { get; set; }
    }
}
