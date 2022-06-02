using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ConfiguracionTipoMarcacionBO : BaseBO
    {
        public string NombreBoton { get; set; }
        public int Orden { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int? IdMigracion { get; set; }
    }
}
