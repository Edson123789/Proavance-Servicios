using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO
    {
        public int IdOportunidad { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdProbabilidadRegistroPWActual { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdAlumno { get; set; }
        public int Idcodigopais { get; set; }
    }
}
