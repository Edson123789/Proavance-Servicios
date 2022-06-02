using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppPlantillaPorOcurrenciaActividadDTO
    {
        public int Id { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public int IdPlantilla { get; set; }
        public int NumeroDiasSinContacto { get; set; }
    }
}
