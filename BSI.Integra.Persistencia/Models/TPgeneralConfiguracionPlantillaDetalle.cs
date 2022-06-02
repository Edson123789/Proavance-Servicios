using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralConfiguracionPlantillaDetalle
    {
        public int Id { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdOperadorComparacion { get; set; }
        public decimal? NotaAprobatoria { get; set; }
        public bool DeudaPendiente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; }
        public virtual TOperadorComparacion IdOperadorComparacionNavigation { get; set; }
        public virtual TPgeneralConfiguracionPlantilla IdPgeneralConfiguracionPlantillaNavigation { get; set; }
    }
}
