using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProcedenciaVentaCruzada
    {
        public int Id { get; set; }
        public int? IdOportunidadInicial { get; set; }
        public int IdCentroCostoInicial { get; set; }
        public int? IdOportunidadActual { get; set; }
        public int IdCentroCostoActual { get; set; }
        public int? IdOportunidadNuevo { get; set; }
        public int IdCentroCostoNuevo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
