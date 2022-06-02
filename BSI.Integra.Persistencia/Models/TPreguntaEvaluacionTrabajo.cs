using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaEvaluacionTrabajo
    {
        public int Id { get; set; }
        public int IdConfigurarEvaluacionTrabajo { get; set; }
        public int IdPregunta { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConfigurarEvaluacionTrabajo IdConfigurarEvaluacionTrabajoNavigation { get; set; }
        public virtual TPregunta IdPreguntaNavigation { get; set; }
    }
}
