using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SesionTempDTO
    {
        public string Dia { get; set; }
        public string Horainicio { get; set; }
        public string Horafin { get; set; }
        public string Horainicio2 { get; set; }
        public string Horafin2 { get; set; }
        public bool Tipo { get; set; }
        public string Ciudad { get; set; }
        public int Idciudad { get; set; }
        public int Veces { get; set; }
    }
}
