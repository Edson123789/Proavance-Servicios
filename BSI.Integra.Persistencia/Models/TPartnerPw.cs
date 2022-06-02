using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPartnerPw
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ImgPrincipal { get; set; }
        public string ImgPrincipalAlf { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgSecundariaAlf { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionCorta { get; set; }
        public string Preguntas { get; set; }
        public int Posicion { get; set; }
        public short? IdPartner { get; set; }
        public string EncabezadoCorreoPartner { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
