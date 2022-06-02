using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class IndicadorReporteCambioFaseBO : BaseBO
    {
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int Meta { get; set; }
        public bool Estado { get; set; }
        public Guid IdMigracion { get; set; }

    }
}
