using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PublicidadWebFormularioBO : BaseBO
    {
        public int IdPublicidadWeb { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TextoBoton { get; set; }
        public List<PublicidadWebFormularioCampoBO> PublicidadWebFormularioCampo { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
