using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralEscalaBO : BaseBO
    {
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal ValorMinimo { get; set; }
        public int IdModeloGeneral { get; set; }
        public Guid IdMigracion { get; set; }
    }
}
