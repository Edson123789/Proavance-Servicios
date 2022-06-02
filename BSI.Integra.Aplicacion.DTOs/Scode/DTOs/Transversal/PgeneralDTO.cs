using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int IdPartner { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int IdBusqueda { get; set; }
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
        public int IdPagina { get; set; }
        public int ChatActivo { get; set; }
        public string PwDescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public bool Estado { get; set; }
        public List<int> Modalidades { get; set; }
        public string CodigoPartner { get; set; }

        public string LogoPrograma { get; set; }
        public string UrlLogoPrograma { get; set; }

    }

    public class PgeneralWebinarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }

    public class InformacionProgramaDocumentosDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string Tipo { get; set; }
        public string Ciudad { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoFase { get; set; }
        public int IdActividadDetalle { get; set; }
    }

    public class archivosAlumno
    {
        public byte[] registroMemoria { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class PGeneralPuntoCorteDTO
    {
        public List<FiltroDTO> ListaAreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
        public List<PGeneralSubAreaFiltroDTO> ListaProgramaGeneral { get; set; }
        public List<FiltroDTO> ListaPuntoCorte { get; set; }
    }
}
