using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class FrecuenciaFeriadoBO:BaseEntity
    {
        public int CodigoFrecuencia { get; set; }
        public string NombreFrecuencia { get; set; }
    }
}
