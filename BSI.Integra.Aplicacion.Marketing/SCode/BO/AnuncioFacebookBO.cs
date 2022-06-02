using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/AnuncioFacebook
    /// Autor: Gian Miranda
    /// Fecha: 01/06/2021
    /// <summary>
    /// BO para la logica de Anuncios Facebook
    /// </summary>
    public class AnuncioFacebookBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// FacebookIdAnuncio                   Id del anuncio - segun DB de Facebook
        /// FacebookNombreAnuncio               Nombre del anuncio - segun DB de Facebook
        /// FacebookIdConjuntoAnuncio           Id del conjunto anuncio - segun DB de Facebook
        /// IDConjuntoAnuncioFacebook           Id del conjunto anuncio Facebook (PK de la tabla mkt.T_ConjuntoAnuncioFacebook)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)
        
        public string FacebookIdAnuncio { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public string FacebookIdConjuntoAnuncio { get; set; }
        public int? IdConjuntoAnuncioFacebook { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public AnuncioFacebookBO()
        {
        }

        public AnuncioFacebookBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
