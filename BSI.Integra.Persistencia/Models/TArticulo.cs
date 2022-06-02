using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TArticulo
    {
        public int Id { get; set; }
        public int IdWeb { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string ImgPortada { get; set; }
        public string ImgPortadaAlt { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgSecundariaAlt { get; set; }
        public string Autor { get; set; }
        public int? IdTipoArticulo { get; set; }
        public string Contenido { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdCategoria { get; set; }
        public string UrlWeb { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string UrlDocumento { get; set; }
        public string DescripcionGeneral { get; set; }
    }
}
