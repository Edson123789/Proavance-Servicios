using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PuestoTrabajoGrupoComparacionBO : BaseBO
    {
        public int IdPuestoTrabajo { get; set; }
        public int IdGrupoComparacionProcesoSeleccion { get; set; }
    }
}
