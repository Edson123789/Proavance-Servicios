using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaPorAsesorDetalleDTO
    {
        public int IdAsesor { get; set; }
        public string Programa { get; set; }
        public int IdCentroCosto { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC1_MuyAlta { get; set; }
        public int BNC_VentaCruzada { get; set; }
        public int Total_BNC_BNC1_VentaCruzada { get; set; }
        public int BNC_SinProbabilidad { get; set; }
        public int BNC_Historico { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos_SinMeta { get; set; }
        public int TotalDatos_ConMeta { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
        public int MetaCentroCosto { get; set; }
        public int BNC1_Historico { get; set; }

    }
}
