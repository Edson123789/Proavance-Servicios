using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralCargoBO : BaseBO
    {
        public int IdModeloGeneral { get; set; }
        public int IdCargo { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
