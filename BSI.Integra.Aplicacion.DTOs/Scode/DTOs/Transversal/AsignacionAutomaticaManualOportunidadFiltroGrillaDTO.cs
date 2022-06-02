using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaManualOportunidadFiltroGrillaDTO
    {
        public Paginador paginador { get; set; }
        public GridFilters filter { get; set; }
        public AsignacionManualOportunidadFiltroDTO filtro { get; set; }
    }
}
