using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookAudiencia
    {
        public int Id { get; set; }
        public int IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Subtipo { get; set; }
        public string RecursoArchivoCliente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
