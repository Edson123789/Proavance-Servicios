using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class FlujoOcurrenciaBO : BaseBO
    {
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public bool CerrarSeguimiento { get; set; }
        public int? IdFaseDestino { get; set; }
        public int? IdFlujoActividadSiguiente { get; set; }
    }
}
