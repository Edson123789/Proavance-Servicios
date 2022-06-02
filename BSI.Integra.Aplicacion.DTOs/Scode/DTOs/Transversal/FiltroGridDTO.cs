using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroGridDTO
    {
        public Paginador paginador { get; set; }
        public GridFilters filter { get; set; }
    }
}
