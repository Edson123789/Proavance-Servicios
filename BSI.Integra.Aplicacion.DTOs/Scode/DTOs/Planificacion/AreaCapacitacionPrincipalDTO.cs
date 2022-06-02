using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AreaCapacitacionPrincipalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImgPortada { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgPortadaAlt { get; set; }
        public string ImgSecundariaAlt { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdArea { get; set; }
        public bool EsWeb { get; set; }
        public string DescripcionHtml { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public string Usuario { get; set; }
    }
}
