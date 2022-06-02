using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MensajeProcesarDetalleDTO
    {
        public string NombreCampania { get; set; }
        public string NombreLista { get; set; }
        public int NroIntentos { get; set; }
    }
    public class MensajeProcesarDTO
    {
        public string Nombre { get; set; }
        public List<MensajeProcesarDetalleDTO> ListaDetalle { get; set; }
    }

    public class MensajeWhatsAppFinalizacionProcesadoDTO
    {
        public string Nombre { get; set; }
    }

    public class MensajeProcesarMailingGeneralDTO
    {
        public string Nombre { get; set; }
        public List<MensajeProcesarDetalleDTO> ListaDetalle { get; set; }
        public int IntentosCorrectos { get; set; }
        public int IntentosFallidos { get; set; }
    }
}
