using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreCalculadaCambioFase
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseOportunidadOrigen { get; set; }
        public int? IdFaseOportunidadDestino { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdCampania { get; set; }
        public int Contador { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
