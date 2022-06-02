using AngleSharp.Html.Parser;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CorreoGmailBO : BaseBO
    {
        public long GmailCorreoId { get; set; }
        public int IdGmailFolder { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string CuerpoHtml { get; set; }
        public bool EsLeido { get; set; }
        public string NombreRemitente { get; set; }
        public string EmailRemitente { get; set; }
        public string Destinatarios { get; set; }
        public string EmailConCopiaOculta { get; set; }
        public string EmailConCopia { get; set; }
        public bool AplicaCrearOportunidad { get; set; }
        public bool CumpleCriterioCrearOportunidad { get; set; }
        public bool SeCreoOportunidad { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsDesuscritoCorrectamente { get; set; }
        public bool? EsMarcadoDesuscrito { get; set; }
        public bool? EsDescartado { get; set; }
        public int? IdPersonal { get; set; }

        public virtual ICollection<CorreoGmailArchivoAdjuntoBO> CorreoGmailArchivoAdjunto { get; set; }

        private HtmlParser _htmlParser;
        //private integraDBContext _integraDBContext;
        private CorreoGmailRepositorio _repCorreoGmail;
        public CorreoGmailBO()
        {
            CorreoGmailArchivoAdjunto = new HashSet<CorreoGmailArchivoAdjuntoBO>();
            _htmlParser = new HtmlParser();
        }

        public CorreoGmailBO(integraDBContext integraDBContext)
        {
            CorreoGmailArchivoAdjunto = new HashSet<CorreoGmailArchivoAdjuntoBO>();
            _htmlParser = new HtmlParser();
            //_integraDBContext = integraDBContext;
            _repCorreoGmail = new CorreoGmailRepositorio(integraDBContext);
        }

        /// <summary>
        /// Valida si el registro cumple los criterios necesarios para crear oportunidad
        /// </summary>
        public void ValidarCriterios() {

            //Logica validacion contiene el correo
            //this.CuerpoHtml = "<span style='display: none' id='idPrioridadMailChimpListaCorreo'> 152 </span>";
            if (this.CuerpoHtml.Contains("prioridadMailChimpCorreoId"))
            {
                var document = _htmlParser.ParseDocument(this.CuerpoHtml);

                var _allSpan = document.All.Where(m => m.LocalName == "span" &&
                                     m.HasAttribute("id") &&
                                     m.GetAttribute("id").Contains("prioridadMailChimpCorreoId"));


                if (_allSpan != null)
                {
                    //var element = document.GetElementById("prioridadMailChimpCorreoId");
                    var element = _allSpan.FirstOrDefault();
                    if (element != null)
                    {
                        element.TextContent = element.TextContent.Replace("\n", "");
                        var value = element.TextContent.Trim();
                        if (Int32.TryParse(value, out int intValue))
                        {
                            IdPrioridadMailChimpListaCorreo = intValue;
                            CumpleCriterioCrearOportunidad = true;
                        }
                    }
                }
            }else if (this.CuerpoHtml.Contains("IdPrioridadMailChimpListaCorreo"))
            {
                var totalString = this.CuerpoHtml.Split("IdPrioridadMailChimpListaCorreo");
                var stringContenedorPMLC = totalString[1];
                if (stringContenedorPMLC != null)
                {
                    var stringContenedorIPMLC = stringContenedorPMLC.Split("</");
                    var idPrioridadMailChimpListaCorreo = stringContenedorIPMLC[0];
                    if (idPrioridadMailChimpListaCorreo  != null)
                    {
                        if (Int32.TryParse(idPrioridadMailChimpListaCorreo, out int intValue))
                        {
                            IdPrioridadMailChimpListaCorreo = intValue;
                            CumpleCriterioCrearOportunidad = true;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Obtiene la ultima fecha de descarga por tipo de folder
        /// </summary>
        /// <returns></returns>
        public DateTime? ObtenerUltimaFechaDescarga() {
            return _repCorreoGmail.ObtenerUltimaFechaDescarga(this.IdGmailFolder);
        }
    }
}
