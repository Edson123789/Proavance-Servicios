using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaTipoDescuentoDTO
    {
        public List<int> Descuentos { get; set; }
        public string Usuario { get; set; }
        public int IdPgeneral { get; set; }
    }
}
