using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaCursoTrabajoAlumnoBO : BaseBO
    {
        public int Id { get; set; }
        public int IdRaCurso { get; set; }
        public int IdRaCursoTrabajoAlumnoTipo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
