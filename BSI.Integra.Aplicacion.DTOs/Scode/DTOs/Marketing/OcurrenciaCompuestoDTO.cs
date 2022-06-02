using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OcurrenciaCompuestoDTO
    {
        public OcurrenciaDTO ocurrencia { get; set; }
        public List<ConfiguracionLlamadaOcurrenciaDTO> configuraciones { get; set; }
        //public OcurrenciaDTO ocurrencia { get; set; }
        public string Usuario { get; set; }
    }
}
