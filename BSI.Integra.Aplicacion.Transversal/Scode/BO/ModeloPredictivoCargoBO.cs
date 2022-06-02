using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoCargoBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public bool Validar { get; set; }
        public int IdCargo { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
