using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCrucigramaProgramaCapacitacion
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string CodigoCrucigrama { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
