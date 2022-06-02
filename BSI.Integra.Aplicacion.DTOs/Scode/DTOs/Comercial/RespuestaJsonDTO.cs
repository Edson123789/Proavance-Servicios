using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RespuestaJsonDTO
    {
        public string Respuesta;
        public string Mensaje;
        public RespuestaJsonDTO(string respuesta, string mensaje)
        {
            Respuesta = respuesta;
            Mensaje = mensaje;
        }
    }
}
