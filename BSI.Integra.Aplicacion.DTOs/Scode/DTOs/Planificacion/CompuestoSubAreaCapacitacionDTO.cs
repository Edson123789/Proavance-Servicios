using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoSubAreaCapacitacionDTO
    {
        public SubAreaCapacitacionPrincipalDTO ObjetoSubArea{ get; set; }
        public List<ParametroContenidoDTO> ListaParametro { get; set; }
        public string Usuario { get; set; }
    }
}
