using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContenidoPlantillaDTO
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public int Id { get; set; }
        public int? IdPlantillaClaveValor { get; set; }
        public int? IdAreaEtiqueta { get; set; }
    }
}
