using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGrupoComponenteEvaluacion
    {
        public TGrupoComponenteEvaluacion()
        {
            TPuestoTrabajoPuntajeCalificacion = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool RequiereCentil { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public decimal? Factor { get; set; }

        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacion { get; set; }
    }
}
