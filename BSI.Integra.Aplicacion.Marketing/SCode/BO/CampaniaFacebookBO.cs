using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/CampaniaFacebook
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// BO para la logica de Campanias Facebook
    /// </summary>
    public class CampaniaFacebookBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// FacebookIdCampania                  Id de la campania - segun DB de Facebook
        /// FacebookNombreCampania              Nombre de la campania - segun DB de Facebook
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public string FacebookIdCampania { get; set; }
        public string FacebookNombreCampania { get; set; }
        public string FacebookIdCuenta { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public CampaniaFacebookBO()
        {
        }

        public CampaniaFacebookBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
