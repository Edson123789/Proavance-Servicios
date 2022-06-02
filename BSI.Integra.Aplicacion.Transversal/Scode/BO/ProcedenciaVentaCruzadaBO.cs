using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProcedenciaVentaCruzadaBO : BaseBO
    {
        public int? IdOportunidadInicial { get; set; }
        public int IdCentroCostoInicial { get; set; }
        public int? IdOportunidadActual { get; set; }
        public int IdCentroCostoActual { get; set; }
        public int? IdOportunidadNuevo { get; set; }
        public int IdCentroCostoNuevo { get; set; }
    }
}
