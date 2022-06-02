using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoTagDTO
    {
        public int IdPGeneral { get; set; }
        public TagPwDTO ObjetoTag { get; set; }
        public List<ParametroContenidoDTO> ListaParametro { get; set; }
        public string Usuario { get; set; }

    }
}
