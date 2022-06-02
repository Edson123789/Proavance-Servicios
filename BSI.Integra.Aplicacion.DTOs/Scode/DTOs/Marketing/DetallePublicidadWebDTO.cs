using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetallePublicidadWebDTO
    {
        public PublicidadWebFormularioDTO Formularios { get; set; }
        public List<PublicidadWebProgramaDTO> PublicidadProgramas { get; set; }
        public List<PublicidadWebFormularioCampoDTO> FormularioCampos { get; set; }
    }
   
}

