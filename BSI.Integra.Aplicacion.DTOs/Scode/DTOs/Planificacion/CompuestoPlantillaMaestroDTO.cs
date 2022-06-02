using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPlantillaMaestroDTO
    {
        public PlantillaMaestroPwDTO ObjetoPlantillaMaestro { get; set; }
        public List<SeccionMaestraPwDTO> ListaSeccionMaestra { get; set; }
        public string Usuario { get; set; }
    }
}
