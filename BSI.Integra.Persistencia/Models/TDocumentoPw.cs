using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDocumentoPw
    {
        public TDocumentoPw()
        {
            TBandejaPendientePw = new HashSet<TBandejaPendientePw>();
            TConfigurarEvaluacionTrabajo = new HashSet<TConfigurarEvaluacionTrabajo>();
            TDocumentoSeccionPw = new HashSet<TDocumentoSeccionPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; }
        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePw { get; set; }
        public virtual ICollection<TConfigurarEvaluacionTrabajo> TConfigurarEvaluacionTrabajo { get; set; }
        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPw { get; set; }
    }
}
