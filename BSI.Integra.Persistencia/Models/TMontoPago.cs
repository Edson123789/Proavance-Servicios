using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMontoPago
    {
        public TMontoPago()
        {
            TMontoPagoPlataforma = new HashSet<TMontoPagoPlataforma>();
            TMontoPagoSuscripcion = new HashSet<TMontoPagoSuscripcion>();
        }

        public int Id { get; set; }
        public decimal Precio { get; set; }
        public string PrecioLetras { get; set; }
        public int IdMoneda { get; set; }
        public decimal? Matricula { get; set; }
        public decimal? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
        public int? IdTipoDescuento { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdPais { get; set; }
        public string Vencimiento { get; set; }
        public string PrimeraCuota { get; set; }
        public bool? CuotaDoble { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public bool? PorDefecto { get; set; }
        public decimal? MontoDescontado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TMontoPagoPlataforma> TMontoPagoPlataforma { get; set; }
        public virtual ICollection<TMontoPagoSuscripcion> TMontoPagoSuscripcion { get; set; }
    }
}
