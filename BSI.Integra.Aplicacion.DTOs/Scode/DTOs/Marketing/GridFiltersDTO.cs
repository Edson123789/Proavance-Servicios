using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GridFiltersDTO
    {
        public List<GridFilterDTO> Filters { get; set; }
        public string Logic { get; set; }
    }
}
