using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoPlantilaActualizarPwDTO
    {
        public PlantillaPwDTO PlantillaPw { get; set; }
        public List <int> ListaPaises { get; set; }
        public List <PlantillaRevisionPwDTO> PlantillaRevisionPw { get; set; }
        public List <PlantillaPlantillaMaestroPwDTO> PlantillaPlantillaMaestroPw { get; set; }
        public List <SeccionPwDetalleDTO> ListaNuevasSeccionPw { get; set; }
        public List <SeccionPwDetalleDTO> ListaActualizarSeccionPw { get; set; }
        public List <SeccionPwDTO> ListaEliminarSeccionPw { get; set; }
        public List<SeccionTipoDetallePwDTO> ListaEliminarSeccionTipoDetallePw { get; set; }
        public string Usuario { get; set; }
    }
}

