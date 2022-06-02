using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AreaCampoEtiquetaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<SubAreaCampoEtiquetaDTO>subAreas { get; set; }
    }
}
