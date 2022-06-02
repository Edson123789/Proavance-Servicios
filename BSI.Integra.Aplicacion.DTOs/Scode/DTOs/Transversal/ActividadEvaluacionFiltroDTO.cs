using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Transversal
{
    public class ActividadEvaluacionFiltroDTO
    {
        public int IdProveedor { get; set; }
        public int? IdCentroCostoD { get; set; }

        public string IdArea { get; set; }
        public string IdSubArea { get; set; }
        public string IdPGeneral { get; set; }
        public string IdProgramaEspecifico { get; set; }

        public bool? EstadoEvaluacion { get; set; }
    }
}
