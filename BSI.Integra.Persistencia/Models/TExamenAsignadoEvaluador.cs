using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TExamenAsignadoEvaluador
    {
        public TExamenAsignadoEvaluador()
        {
            TExamenRealizadoRespuestaEvaluador = new HashSet<TExamenRealizadoRespuestaEvaluador>();
        }

        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
        public int IdExamen { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public bool EstadoExamen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TExamen IdExamenNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TPostulante IdPostulanteNavigation { get; set; }
        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; }
        public virtual ICollection<TExamenRealizadoRespuestaEvaluador> TExamenRealizadoRespuestaEvaluador { get; set; }
    }
}
