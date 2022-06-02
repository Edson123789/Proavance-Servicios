using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarWhatsAppPlantillaPorOcurrenciaActividadDTO
    {
        public List<WhatsAppPlantillaPorOcurrenciaActividadDTO> WhatsAppPlantillaPorOcurrenciaActividad { get; set; }
        public List<CorreoPlantillaPorOcurrenciaActividadDTO> CorreoPlantillaPorOcurrenciaActividad { get; set; }

        public string Usuario { get; set; }
        public int IdOcurrenciaActividad { get; set; }

    }
}
