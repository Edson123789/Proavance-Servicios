using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEtiquetaBotonReemplazo
    {
        public int Id { get; set; }
        public int IdEtiqueta { get; set; }
        public string Texto { get; set; }
        public bool AbrirEnNuevoTab { get; set; }
        public string Estilos { get; set; }
        public string Url { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TEtiqueta IdEtiquetaNavigation { get; set; }
    }
}
