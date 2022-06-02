using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralCategoriaDatoBO : BaseBO
    {
        public int IdModeloGeneral { get; set; }
        public int IdAsociado { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int IdCategoriaDato { get; set; } 
    }
}
