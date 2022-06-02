using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class SedeTrabajoGrupoComparacionBO : BaseBO
    {
        public int IdSedeTrabajo { get; set; }
        public int IdGrupoComparacionProcesoSeleccion { get; set; }
    }
}
