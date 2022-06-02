using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConfiguracionLlamadaOcurrenciaBO : BaseBO
    {
        public int IdOcurrencia { get; set; }
        public int? IdConectorOcurrenciaLlamada { get; set; }
        public int NumeroLlamada { get; set; }
        public int IdCondicionOcurrenciaLlamada { get; set; }
        public int? IdFaseTiempoLlamada { get; set; }
        public int Duracion { get; set; }
        public Guid? IdMigracion { get; set; }

        public ConfiguracionLlamadaOcurrenciaBO()
        {

        }
    }
}
