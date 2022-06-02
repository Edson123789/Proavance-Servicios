using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRecordAreaComercial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
        public int IdMonedaRecord { get; set; }
        public int IdTableroComercialUnidad { get; set; }
        public decimal Bono { get; set; }
        public int IdMonedaBono { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public bool EsVigente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
