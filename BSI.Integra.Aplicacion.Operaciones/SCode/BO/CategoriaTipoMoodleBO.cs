using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class CategoriaTipoMoodleBO : BaseBO
    {
        public int Id { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public string IdTipoCapacitacionMoodle { get; set; }
        public bool AplicaProyecto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
