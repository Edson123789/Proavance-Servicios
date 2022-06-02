using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroComisionMontoPagoDTO
    {
        public List<ComisionMontoPagoDTO> ComisionMontoPagos { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EsComisionable { get; set; }
        public bool TieneDocumento { get; set; }
        public bool TieneMatriculaPagada { get; set; }
        public bool TieneAsistencia { get; set; }
        public string Usuario { get; set; }


    }
}
