using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: PartnerPwBO
    /// Autor: _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_PartnerPw
    /// </summary>
    public class PartnerPwBO : BaseBO
    {
        /// Propiedades		                Significado
        /// -------------	                -----------------------
        /// Nombre                          Nombre de partner                               
        /// ImgPrincipal                    Imagen principal
        /// ImgPrincipalAlf                 Imagen principal
        /// ImgSecundaria                   Imagen secundaria
        /// ImgSecundariaAlf                Imagen secundaria
        /// Descripcion                     Descripción de partner
        /// DescripcionCorta                Descripción corta
        /// Preguntas                       Preguntas
        /// Posicion                        Posición
        /// IdPartner                       Id de partner
        /// EncabezadoCorreoPartner         Encabezado de correo partner
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
    }

    public class PartnerBeneficioPwBO : BaseBO
    {
        /// Propiedades		                Significado
        /// -------------	                -----------------------
        /// IdPartner                       Id de partner                               
        /// Descripcion                     Descripción
        public int IdPartner { get; set; }
        public string Descripcion { get; set; }
    }

    public class PartnerContactoPwBO : BaseBO
    {
        /// Propiedades		                Significado
        /// -------------	                -----------------------
        /// IdPartner                       Id de partner                              
        /// Nombres                         Nombres
        /// Apellidos                       Apellidos
        /// Email1                          Email 1
        /// Email2                          Email 2
        /// Telefono1                       Teléfono 1
        /// Telefono2                       Teléfono 2
        public int IdPartner { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
    }
}
