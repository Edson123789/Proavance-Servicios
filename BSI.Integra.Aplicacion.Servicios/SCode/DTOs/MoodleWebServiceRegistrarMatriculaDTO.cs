using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.SCode.DTOs
{
    public class MoodleWebServiceRegistrarMatriculaDTO
    {
        public int userid { get; set; }
        public int courseid { get; set; }
        public int roleid { get; set; }
        public long timestart { get; set; }
        public long timeend { get; set; }
    }
}
