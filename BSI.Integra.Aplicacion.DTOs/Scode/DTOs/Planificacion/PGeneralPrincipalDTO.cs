using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PGeneralPrincipalDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int ? IdArea { get; set; }
        public int ? IdSubArea { get; set; }
        public int ? IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int ? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string PgTitulo { get; set; }
        public string Codigo { get; set; }
        public string UrlImagenPortadaFr { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string UrlPartner { get; set; }
        public string UrlVersion { get; set; }
        public string PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string NombreCorto { get; set; }
        public int ? IdPagina { get; set; }
        public int ? ChatActivo { get; set; }
        public string Usuario { get; set; }
    }
}
