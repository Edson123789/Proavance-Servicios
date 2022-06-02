using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FeriadoEspecialBO : BaseBO
    {
        public DateTime Dia { get; set; }
        public string Motivo { get; set; }
        public int IdCiudad { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
