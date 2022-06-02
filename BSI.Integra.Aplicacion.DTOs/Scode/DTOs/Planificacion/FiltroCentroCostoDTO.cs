using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class FiltroCentroCostoDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string CentroCosto { get; set; }
    }
}
