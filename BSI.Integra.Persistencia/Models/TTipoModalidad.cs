using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoModalidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Abreviatura { get; set; }
        public string ImagenPrincipal { get; set; }
        public string ImagenPrincipalAlf { get; set; }
        public string ImagenSecundaria { get; set; }
        public string ImagenSecundariaAlf { get; set; }
        public string DescripcionCorta { get; set; }
        public string Descripcion { get; set; }
        public string Preguntas { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
