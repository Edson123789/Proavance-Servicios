using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTarifarioDetalleAlterno
    {
        public int Id { get; set; }
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public int? IdPais { get; set; }
        public decimal? Monto { get; set; }
        public bool? AplicaCuota { get; set; }
        public string Descripcion { get; set; }
        public string TipoCantidad { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdMoneda { get; set; }
        public bool? VisualizarPortalWeb { get; set; }
    }
}
