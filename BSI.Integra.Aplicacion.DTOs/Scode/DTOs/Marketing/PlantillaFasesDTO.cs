using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaFasesDTO
    {
        public int? IdPlantilla { get; set; }
        public int? IdFaseOrigen { get; set; }
        public string NombreFase { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
