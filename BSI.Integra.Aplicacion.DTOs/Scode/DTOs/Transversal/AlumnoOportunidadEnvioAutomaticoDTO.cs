using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoOportunidadEnvioAutomaticoDTO
    {
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime HoraMinima { get; set; }
        public string FaseOportunidad { get; set; }
        public int ConsiderarEnviados { get; set; }
        public int IdPlantilla { get; set; }
    }
}
