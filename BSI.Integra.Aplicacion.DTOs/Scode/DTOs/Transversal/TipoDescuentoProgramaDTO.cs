using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoDescuentoProgramaDTO
    {
        public int IdTipoDescuento { get; set; }
        public string Usuario { get; set; }
        public List<int>  IdPgeneral { get; set; }
    }
}
