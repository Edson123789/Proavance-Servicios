using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoIndustriaBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public bool Validar { get; set; }
        public int IdIndustria { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
