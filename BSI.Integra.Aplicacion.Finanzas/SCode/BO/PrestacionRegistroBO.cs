using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class PrestacionRegistroBO : BaseBO
    {
        public int IdPrestacionTipo { get; set; }
        public string Nombre { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
