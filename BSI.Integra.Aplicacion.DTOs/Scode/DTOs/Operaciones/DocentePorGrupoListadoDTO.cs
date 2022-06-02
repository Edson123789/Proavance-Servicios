using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class DocentePorGrupoListadoDTO
    {
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int? IdExpositor { get; set; }
        public int Grupo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsNotaAprobada { get; set; }
        public bool? EsAsistenciaAprobada { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
