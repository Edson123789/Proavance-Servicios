using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConfiguracionBicBO : BaseEntity
    {
        public int Dias { get; set; }
        public int Llamadas { get; set; }
        public bool Aplica { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
