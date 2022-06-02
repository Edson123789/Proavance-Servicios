using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegularizacionLLamadaWebphoneDTO
    {
        public string Anexo3cx { get; set; }
        public string Celular { get; set; }
        public DateTime FechaReal { get; set; }
        public int IdPersonal { get; set; }
        public int IdActividad { get; set; }
        public int IdAlumno { get; set; }
    }
}
