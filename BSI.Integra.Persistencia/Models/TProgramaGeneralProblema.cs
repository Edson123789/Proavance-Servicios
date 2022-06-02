using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralProblema
    {
        public TProgramaGeneralProblema()
        {
            TProgramaGeneralProblemaDetalleSolucion = new HashSet<TProgramaGeneralProblemaDetalleSolucion>();
            TProgramaGeneralProblemaModalidad = new HashSet<TProgramaGeneralProblemaModalidad>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralProblemaDetalleSolucion> TProgramaGeneralProblemaDetalleSolucion { get; set; }
        public virtual ICollection<TProgramaGeneralProblemaModalidad> TProgramaGeneralProblemaModalidad { get; set; }
    }
}
