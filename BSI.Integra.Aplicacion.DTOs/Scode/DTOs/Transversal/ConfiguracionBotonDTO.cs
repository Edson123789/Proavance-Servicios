using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class ConfiguracionBotonDTO
    {
        public int Id { get; set; }
        public string NombreBoton { get; set; }
        public int Orden { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
