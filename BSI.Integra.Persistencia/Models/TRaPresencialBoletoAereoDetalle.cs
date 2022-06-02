using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaPresencialBoletoAereoDetalle
    {
        public int Id { get; set; }
        public int? IdRaPresencialBoletoAereo { get; set; }
        public int? IdRaAerolinea { get; set; }
        public DateTime? Fecha { get; set; }
        public string Origen { get; set; }
        public DateTime? HoraSalida { get; set; }
        public string Destino { get; set; }
        public DateTime? HoraLlegada { get; set; }
        public string NumeroVuelo { get; set; }
        public int? IdRaTipoBoletoAereo { get; set; }
        public string CodigoReserva { get; set; }
        public int? IdRaAgencia { get; set; }
        public double? PagoReal { get; set; }
        public bool? PagoAerolinea { get; set; }
        public int? IdFur { get; set; }
        public bool? Pagado { get; set; }
        public DateTime? FechaCompraPasaje { get; set; }
        public DateTime? FechaVencimientoReserva { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
