using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModalidadCurso
    {
        public TModalidadCurso()
        {
            TCriterioEvaluacionModalidadCurso = new HashSet<TCriterioEvaluacionModalidadCurso>();
            TEsquemaEvaluacionPgeneralModalidad = new HashSet<TEsquemaEvaluacionPgeneralModalidad>();
            TPgeneralCodigoPartnerModalidadCurso = new HashSet<TPgeneralCodigoPartnerModalidadCurso>();
            TPgeneralConfiguracionPlantillaDetalle = new HashSet<TPgeneralConfiguracionPlantillaDetalle>();
            TPgeneralModalidad = new HashSet<TPgeneralModalidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string Codigo { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TCriterioEvaluacionModalidadCurso> TCriterioEvaluacionModalidadCurso { get; set; }
        public virtual ICollection<TEsquemaEvaluacionPgeneralModalidad> TEsquemaEvaluacionPgeneralModalidad { get; set; }
        public virtual ICollection<TPgeneralCodigoPartnerModalidadCurso> TPgeneralCodigoPartnerModalidadCurso { get; set; }
        public virtual ICollection<TPgeneralConfiguracionPlantillaDetalle> TPgeneralConfiguracionPlantillaDetalle { get; set; }
        public virtual ICollection<TPgeneralModalidad> TPgeneralModalidad { get; set; }
    }
}
