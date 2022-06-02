using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPublicidadWebDTO
    {
        public PublicidadWebPanelDTO PublicidadWeb { get; set; }
        public PublicidadWebFormularioDTO Formulario { get; set; }
        public List<PublicidadWebProgramaDTO> PublicidadProgramas { get; set; }
        public List<PublicidadWebFormularioCampoDTO> FormularioCampos { get; set; }
        public string Usuario { get; set; }
        public int IdPublicidad { get; set; }
    }
}
