using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaPagoAlumnoDTO
    {
        public List<CronogramaDetallePagoDTO> ListaCronogramaDetallePago { get; set; }
        public string Moneda { get; set; }
        public string TipoCambio { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public string NombreUsuario { get; set; }
    }
}
