using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosFormularioLandingPageDTO
    {
        public FormularioLandingPageDTO formularioLandingPage { get; set; }
        public List<DatoAdicionalPaginaDTO> datosAdicionales  { get; set; }
        public string Usuario { get; set; }
    }
}
