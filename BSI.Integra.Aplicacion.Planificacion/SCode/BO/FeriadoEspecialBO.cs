using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class FeriadoEspecialBO
    {
        public DateTime Dia { get; set; }
        public string Motivo { get; set; }
        public int IdCiudad { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
