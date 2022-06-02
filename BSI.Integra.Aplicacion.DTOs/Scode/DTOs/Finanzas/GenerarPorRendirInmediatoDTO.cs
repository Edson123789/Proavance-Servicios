using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GenerarPorRendirInmediatoDTO
    {
        public CajaPorRendirCabeceraDTO CajaPRCabecera { get; set; }
        public List<CajaPorRendirDTO> ListaPorRendir { get; set; }
    }
}
