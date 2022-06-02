using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ProcesoSeleccionPuntajeCalificacionBO : BaseBO
    {
        public int IdProcesoSeleccion { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public bool CalificaPorCentil { get; set; }
        public decimal? PuntajeMinimo { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsCalificable { get; set; }
    }
}
