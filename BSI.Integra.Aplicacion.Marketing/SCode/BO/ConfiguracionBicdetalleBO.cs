using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConfiguracionBicdetalleBO : BaseEntity
    {
        public int IdConfiguracionBic { get; set; }
        public int IdBloqueHorario { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
