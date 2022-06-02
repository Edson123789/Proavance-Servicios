using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class FiltroCompuestroGrillaDTO
    {
        public Paginador paginador { get; set; }
        public GridFilters filter { get; set; }
    }
}
