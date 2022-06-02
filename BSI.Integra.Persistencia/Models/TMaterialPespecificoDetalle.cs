using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialPespecificoDetalle
    {
        public int Id { get; set; }
        public int IdMaterialPespecifico { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFur { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string UsuarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? IdEstadoRegistroMaterial { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string UsuarioSubida { get; set; }

        public virtual TFur IdFurNavigation { get; set; }
        public virtual TMaterialEstado IdMaterialEstadoNavigation { get; set; }
        public virtual TMaterialPespecifico IdMaterialPespecificoNavigation { get; set; }
        public virtual TMaterialVersion IdMaterialVersionNavigation { get; set; }
    }
}
