using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EtiquetaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdTipoEtiqueta { get; set; }

        public virtual ICollection<EtiquetaBotonReemplazoBO> ListaEtiquetaBotonReemplazo { get; set; }

        public EtiquetaBO() {
            ListaEtiquetaBotonReemplazo = new HashSet<EtiquetaBotonReemplazoBO>();
        }

        public string ObtenerEnlaceMessenger(int idCentroCosto) {
            try
            {
                return string.Concat("http://m.me/174599872598131?ref=", idCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string ObtenerWhatsapp(string etiqueta, int idCentroCosto, int idPais)
        {
            try
            {
                return string.Concat("https://bsginstitute.com/WhatsApp?cc=", idCentroCosto, "&ps=",idPais);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
