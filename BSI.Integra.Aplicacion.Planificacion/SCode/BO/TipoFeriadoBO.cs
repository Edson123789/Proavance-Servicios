using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TipoFeriadoBO: BaseEntity
    {
        public int CodigoTipo {get; set; }
        public string NombreTipo { get; set; }
        
    }
}
