using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampoEtiquetaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Campo { get; set; }
        public int IdAreaEtiqueta { get; set; }
        public int IdSubAreaEtiqueta { get; set; }
    }
}
