using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPlantilaPwDTO
    {
        public PlantillaPwDTO PlantillaPw { get; set; }
        public List <int> ListaPaises { get; set; }
        public List <PlantillaRevisionPwDTO> PlantillaRevisionPw { get; set; }
        public List <PlantillaPlantillaMaestroPwDTO> PlantillaPlantillaMaestroPw { get; set; }
        public List <SeccionPwDTO> SeccionPw { get; set; }
        public string Usuario { get; set; }
    }
}

