using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroFasesOportunidadAlumnoDTO
    {
        public int? IdAlumno { get; set; }
        public string FasesOportunidad { get; set; }
        public DateTime HoraMinima { get; set; }
        public int ConsiderarEnviados { get; set; }
    }
}
