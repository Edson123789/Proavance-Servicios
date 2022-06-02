using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PreguntaCategoriaBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
