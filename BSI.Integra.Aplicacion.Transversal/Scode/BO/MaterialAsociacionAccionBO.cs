using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialAsociacionAccionBO : BaseBO
    {
        public int IdMaterialTipo { get; set; }
        public int IdMaterialAccion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
