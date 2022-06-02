using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class TipoInteraccionMandrilBO : BaseBO
    {
        public string Nombre { get; set; }
        public int IdTipoInteraccion { get; set; }
        public string Descripcion { get; set; }
    }
}
