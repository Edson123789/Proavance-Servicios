using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SeguimientoFiltroFinalDTO
    {
        public string CentroCostos { get; set; }
        public string Asesores { get; set; }
        public string FasesOportunidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int OpcionFase { get; set; }
        public string FasesOportunidadOrigen { get; set; }
        public string FasesOportunidadDestino { get; set; }
        public string EstadosMatricula { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string CodigoMatricula { get; set; }
        public int? ControlFiltroFecha { get; set; }


    }
}
