using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class CursoMoodleBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdCursoMoodle { get; set; }
        public string NombreCursoMoodle { get; set; }
        public int? IdCategoriaPadre { get; set; }
        public string NombreCategoriaPadre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
