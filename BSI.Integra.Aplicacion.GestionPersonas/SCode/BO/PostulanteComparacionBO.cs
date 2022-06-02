using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PostulanteComparacionBO : BaseBO
    {
        public int? IdPostulante { get; set; }
        public int? IdGrupoComparacionProcesoSeleccion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
