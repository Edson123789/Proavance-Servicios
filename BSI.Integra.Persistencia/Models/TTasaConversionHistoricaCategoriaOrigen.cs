using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTasaConversionHistoricaCategoriaOrigen
    {
        public int Id { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string ValorVariable { get; set; }
        public int? OcurrenciasCerradas { get; set; }
        public int? InscritosMatriculados { get; set; }
        public decimal? TasaConversion { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
