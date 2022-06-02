using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EnvioCorreoBicOperacionesDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string Remitente { get; set; }
        public string Destiantario { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public int IdMandrilEnvioCorreo { get; set; }
        public string Usuario { get; set; }
    }
}
