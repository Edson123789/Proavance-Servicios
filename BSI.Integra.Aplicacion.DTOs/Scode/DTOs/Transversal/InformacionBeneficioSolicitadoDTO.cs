using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class InformacionBeneficioSolicitadoDTO
    {
        public int Id { get; set; }
        public string Beneficio { get; set; }
        public string Programa { get; set; }
        public string CentroCosto { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string Coordinador { get; set; }
        public string EstadoSolicitud { get; set; }
        public DateTime? FechaEntregaBeneficio { get; set; }

    }
}
