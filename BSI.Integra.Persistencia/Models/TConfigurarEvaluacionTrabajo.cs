using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfigurarEvaluacionTrabajo
    {
        public TConfigurarEvaluacionTrabajo()
        {
            TPreguntaEvaluacionTrabajo = new HashSet<TPreguntaEvaluacionTrabajo>();
        }

        public int Id { get; set; }
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdDocumentoPw { get; set; }
        public string ArchivoNombre { get; set; }
        public string ArchivoCarpeta { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        public string DescripcionPregunta { get; set; }
        public int? OrdenCapitulo { get; set; }
        public bool? HabilitarInstrucciones { get; set; }
        public bool? HabilitarArchivo { get; set; }
        public bool? HabilitarPreguntas { get; set; }
        public int? OrdenEvaluacion { get; set; }

        public virtual TDocumentoPw IdDocumentoPwNavigation { get; set; }
        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual TTipoEvaluacionTrabajo IdTipoEvaluacionTrabajoNavigation { get; set; }
        public virtual ICollection<TPreguntaEvaluacionTrabajo> TPreguntaEvaluacionTrabajo { get; set; }
    }
}
