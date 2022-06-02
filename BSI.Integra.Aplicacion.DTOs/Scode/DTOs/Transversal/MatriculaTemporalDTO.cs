using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaTemporalDTO
    {
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
    }
}
