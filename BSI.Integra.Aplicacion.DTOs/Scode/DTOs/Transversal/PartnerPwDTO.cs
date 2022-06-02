using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PartnerPwDTO
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
        public string Usuario { get; set; }

        public List<PartnerBeneficioPwDTO> listaPartnerBeneficio { get; set; }
        public List<PartnerContactoPwDTO> listaPartnerContacto { get; set; }
    }

    public class PartnerBeneficioPwDTO
    {
        public int Id { get; set; }
        public int IdPartner { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public bool Estado { get; set; }
    }

    public class PartnerContactoPwDTO
    {
        public int Id { get; set; }
        public int IdPartner { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Usuario { get; set; }
        public bool Estado { get; set; }
    }

    public class PartnerRegistrosPwDTO
    {
        public List<PartnerBeneficioPwDTO> listaPartnerBeneficio { get; set; }
        public List<PartnerContactoPwDTO> listaPartnerContacto { get; set; }
    }
}
