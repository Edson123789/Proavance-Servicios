using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PublicidadWebFormularioCampoBO : BaseBO
    {
        public int IdPublicidadWebFormulario { get; set; }
        public int IdCampoContacto { get; set; }
        public string Nombre { get; set; }
        public bool Siempre { get; set; }
        public bool Inteligente { get; set; }
        public bool Probabilidad { get; set; }
        public int Orden { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
