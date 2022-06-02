using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaFiltroDTO
    {
        public List<FiltroDTO> ListaFiltro { get; set; }
        public int IdRol { get; set; }
        public int CheckedIsFurGeneral { get; set; }
        public string Observacion { get; set; }
        public bool isAprobar { get; set; }
    }
}
