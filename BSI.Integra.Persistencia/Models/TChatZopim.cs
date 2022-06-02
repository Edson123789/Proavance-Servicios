using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TChatZopim
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Script { get; set; }
        public string Filtro { get; set; }
        public int? IdAsesor { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
