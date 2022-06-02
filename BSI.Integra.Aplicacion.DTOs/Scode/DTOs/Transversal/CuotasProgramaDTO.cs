using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CuotasProgramaDTO
    {
        public int? IdBusqueda { get; set; }
        public string NombreCurso { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCuotaSeleccionada { get; set; }
        public string WebMoneda { get; set; }
        public decimal? WebTipoCambio { get; set; }
        public string NombrePespecifico { get; set; }
        public string DuracionPespecifico { get; set; }
        public string DuracionPGeneral { get; set; }
        public string TipoPrograma { get; set; }
        public int? NumeroCuotas { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? WebTotalPagar { get; set; }
        public bool EstadoCronogramaMod { get; set; }
        public List<ProgramaListaCuotaDTO> ListaCuotas { get; set; }
    }    
}
