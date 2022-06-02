using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class GridFilters
    {
        public List<GridFilter> Filters { get; set; }
        public string Logic { get; set; }//"and"

        public GridFilters()
        {
            Filters = new List<GridFilter>();
            Logic = string.Empty;
        }
    }

    public class GridFilter
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }

    public class GridSort
    {
        public string Field { get; set; }
        public string Dir { get; set; }
    }
}
