using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEmbudoPre
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Desuscrito { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public DateTime Fecha { get; set; }
        public int Veces { get; set; }
        public int? IdPgeneral { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdPais { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
