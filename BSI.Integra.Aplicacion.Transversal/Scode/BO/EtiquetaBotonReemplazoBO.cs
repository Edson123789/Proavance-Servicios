using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class EtiquetaBotonReemplazoBO : BaseBO
    {
        public int IdEtiqueta { get; set; }
        public string Texto { get; set; }
        public bool AbrirEnNuevoTab { get; set; }
        public string Estilos { get; set; }
        public string Url { get; set; }
        public int? IdMigracion { get; set; }

        public EtiquetaBotonReemplazoBO() {

        }
    }
}
