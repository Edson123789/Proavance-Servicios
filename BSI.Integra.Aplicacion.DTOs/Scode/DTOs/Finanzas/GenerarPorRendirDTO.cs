using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GenerarPorRendirDTO
    {
        public CajaPorRendirCabeceraDTO CajaPRCabecera { get; set; }
        public List<int> ListaIdPorRendir { get; set; }
    }
}
