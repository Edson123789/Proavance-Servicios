using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class EmbudoPreBO : BaseBO
    {
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
        public int? IdMigracion { get; set; }

        public EmbudoPreBO() {
        }
    }
}
