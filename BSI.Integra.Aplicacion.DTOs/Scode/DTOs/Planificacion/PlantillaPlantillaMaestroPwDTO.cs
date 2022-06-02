using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaPlantillaMaestroPwDTO
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public string Contenido { get; set; }
    }
}
