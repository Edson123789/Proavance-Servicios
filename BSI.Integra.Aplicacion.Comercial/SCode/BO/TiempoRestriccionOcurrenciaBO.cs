using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class TiempoRestriccionOcurrenciaBO : BaseEntity
    {
        public int Segundos { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
