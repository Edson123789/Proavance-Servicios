using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PublicidadWebBO : BaseBO
    {
        public int IdTipoPublicidadWeb { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Popup { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }
        public int IdChatZoopim { get; set; }
        public int? IdPespecifico { get; set; }
        public string UrlImagen { get; set; }
        public string UrlBrochure { get; set; }
        public string UrlVideo { get; set; }
        public bool? EsRegistroAdicional { get; set; }
        public Guid? IdMigracion { get; set; }



        public PublicidadWebFormularioBO PublicidadWebFormulario { get; set; }
        public List<PublicidadWebProgramaBO> PublicidadWebPrograma { get; set; }
       

    }
}
