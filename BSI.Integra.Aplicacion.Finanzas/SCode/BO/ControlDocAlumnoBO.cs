using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public partial class ControlDocAlumnoBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string ComisionableEditable { get; set; }
        public decimal? MontoComisionable { get; set; }
        public string ObservacionesComisionable { get; set; }
        public decimal? PagadoComisionable { get; set; }
        public int? IdMatriculaObservacion { get; set; }
        public string IdMigracion { get; set; }
    }
}
