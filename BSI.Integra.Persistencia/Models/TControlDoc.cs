using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TControlDoc
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCriterioDoc { get; set; }
        public bool EstadoDocumento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public string IdMigracion { get; set; }
    }
}
