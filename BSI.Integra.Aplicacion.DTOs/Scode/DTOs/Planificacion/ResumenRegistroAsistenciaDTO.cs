using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResumenRegistroAsistenciaDTO
    {
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int TotalAsistenciaRegistrada { get; set; }
    }
}
