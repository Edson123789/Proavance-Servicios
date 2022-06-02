using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalCeseDTO
    {
        public int? Id { get; set; }

        public int? IdMotivoCese { get; set; }
        public DateTime FechaCese { get; set; }
    }
}
