using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamenTest
    {
        public TExamenTest()
        {
            TPuestoTrabajoPuntajeCalificacion = new HashSet<TPuestoTrabajoPuntajeCalificacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreAbreviado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsCalificadoPorPostulante { get; set; }
        public bool MostrarEvaluacionAgrupado { get; set; }
        public bool MostrarEvaluacionPorGrupo { get; set; }
        public bool MostrarEvaluacionPorComponente { get; set; }
        public bool RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public bool CalificarEvaluacion { get; set; }
        public bool EsCalificacionAgrupada { get; set; }
        public decimal? Factor { get; set; }
        public int? IdEvaluacionCategoria { get; set; }

        public virtual ICollection<TPuestoTrabajoPuntajeCalificacion> TPuestoTrabajoPuntajeCalificacion { get; set; }
    }
}
