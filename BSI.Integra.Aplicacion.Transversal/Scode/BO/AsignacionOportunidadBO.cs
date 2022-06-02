using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AsignacionOportunidadBO : BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }//IdAsesor
        public int? IdCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        //Hijos
        public AsignacionOportunidadLogBO AsignacionOportunidadLog { get; set; }
        //comentado carlos porque  salia error
        //public AsignacionOportunidadBO() {
        //    AsignacionOportunidadLog = new AsignacionOportunidadLogBO();
        //}
    }
}
