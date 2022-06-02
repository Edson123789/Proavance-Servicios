using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/ScrapingPortalEmpleoResultado
    /// Autor: Ansoli Deyvis
    /// Fecha: 18-01-2021
    /// <summary>
    /// BO de la tabla T_ScrapingPortalEmpleoResultado
    /// </summary>
    public class ScrapingPortalEmpleoResultadoBO : BaseBO
    {
        /// Propiedades	Significado
        /// -----------	------------
        /// IdScrapingPagina		Fk con la tabla T_ScrapingPagina
        /// TituloAnuncio		Titulo del anuncio
        /// Url		Url del anuncio
        /// PortalId		Identificado del Anuncio en su portal
        /// Puesto		Puesto del anuncio
        /// Empresa		Empresa del anuncio
        /// Ubicacion		Ubicacion del anuncio
        /// Jornada		Jornada Laboral del anuncio
        /// Salario		Salario del anuncio
        /// Descripcion		Descripcion del anuncio en texto
        /// DescripcionHtml		Descripcion del anuncio en HTML
        /// Error		Error al Scrapear el anuncio
        /// Modalidad   Modalidad del anuncio
        /// EsClasificado   Indica si ha sido clasificado el anuncio

        public int IdScrapingPagina { get; set; }
        public string TituloAnuncio { get; set; }
        public string Url { get; set; }
        public string PortalId { get; set; }
        public DateTime FechaAnuncio { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string Ubicacion { get; set; }
        public string Jornada { get; set; }
        public string TipoContrato { get; set; }
        public string Salario { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionHtml { get; set; }
        public string Error { get; set; }
        public string Modalidad { get; set; }
        public bool? EsClasificado { get; set; }
    }
}
