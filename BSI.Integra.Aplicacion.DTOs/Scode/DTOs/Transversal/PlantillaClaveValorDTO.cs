using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.DTO
{
    public class PlantillaClaveValorDTO
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public int IdPlantilla { get; set; }
    }
}
