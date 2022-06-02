using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCajaEgreso
    {
        public int Id { get; set; }
        public int? IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int? IdFur { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public int? IdCajaEgresoAprobado { get; set; }
        public bool EsEnviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdPersonalResponsable { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int? IdComprobantePago { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
