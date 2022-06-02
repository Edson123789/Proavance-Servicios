using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSdtEstandarItemDniRucDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string Semaforos { get; set; }
        public string Score { get; set; }
        public string DeudaTotal { get; set; }       
        public string SemanaActual { get; set; }
        public string SemanaPrevio { get; set; }
        
    }
}
