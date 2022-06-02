using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAvatar
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string Top { get; set; }
        public string Accessories { get; set; }
        public string HairColor { get; set; }
        public string FacialHair { get; set; }
        public string FacialHairColor { get; set; }
        public string Clothes { get; set; }
        public string Eyes { get; set; }
        public string Eyesbrow { get; set; }
        public string Mouth { get; set; }
        public string Skin { get; set; }
        public string ClothesColor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
