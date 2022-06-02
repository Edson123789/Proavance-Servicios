using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarFormularioSolicitudCampoDTO
    {
        public FormularioSolicitudDTO Formulario { get; set; }
        public List<datosInsertarCamposDTO> Campo { get; set; }
    }
}
