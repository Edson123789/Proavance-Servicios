using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubAreaCampoEtiquetaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdAreaEtiqueta { get; set; }
        public List<CampoEtiquetaDTO> listaCampoEtiqueta { get; set; }
    }
}
