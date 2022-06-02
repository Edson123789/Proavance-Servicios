using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SmsEnvioAnexoDTO
    {
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int? IdCodigoPais { get; set; }
        public string Celular { get; set; }
        public string Servidor { get; set; }
        public string Tipo { get; set; }
        public string Puerto { get; set; }
    }

    public class OportunidadDiasSinContactoDTO
    {
        public int IdOportunidad { get; set; }
        public int DiasSinContacto { get; set; }
    }
}
