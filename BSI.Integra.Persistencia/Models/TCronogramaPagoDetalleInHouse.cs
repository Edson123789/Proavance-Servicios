using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaPagoDetalleInHouse
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Curso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estatus { get; set; }
        public string NroOcOs { get; set; }
        public string ActaConformidad { get; set; }
        public string NroContrato { get; set; }
        public string Factura { get; set; }
        public string Moneda { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaPago { get; set; }
        public string EstadoPago { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdMatriculaCabecera { get; set; }
    }
}
