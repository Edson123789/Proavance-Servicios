using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.DTO
{
    public class EnviarCorreoIntegraDTO
    {
        public string Asunto { get; set; }
        public string Emisor { get; set; }
        public string EmisorNombrePersonalizado { get; set; }
        public string Mensaje { get; set; }
        public List<string> Receptores { get; set; }
        public List<string> ReceptoresCopia { get; set; }
        public List<string> ReceptoresCopiaOculta { get; set; }
    }
    public class EnviarCorreoIntegraCodificadoDTO
    {
        public string DatosCodificados { get; set; }
    }
    public class RespuestaServiciosDTO
    {
        public bool Respuesta { get; set; }
        public string Mensaje { get; set; }
    }
}
