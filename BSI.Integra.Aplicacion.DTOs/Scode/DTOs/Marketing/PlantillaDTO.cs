using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPlantillaBase { get; set; }
        public int? idTipoEnvio { get; set; }
    }
}
