using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaCalificar_DocenteDTO
    {
        public int IdPGeneral { get; set; }
        public string PGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public int Grupo { get; set; }
        public int IdProveedor { get; set; }

        public string CentroCostoPrograma { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public string AreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public string SubAreaCapacitacion { get; set; }
    }
}
