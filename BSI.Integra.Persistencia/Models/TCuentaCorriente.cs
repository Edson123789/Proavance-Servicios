using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCuentaCorriente
    {
        public TCuentaCorriente()
        {
            TFurPago = new HashSet<TFurPago>();
        }

        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public int? IdCiudad { get; set; }
        public string Sucursal { get; set; }
        public int IdMoneda { get; set; }
        public string Cuenta { get; set; }
        public int IdBanco { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TFurPago> TFurPago { get; set; }
    }
}
