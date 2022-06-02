using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoBancoListDTO
    {
        public List<PagoBancoDTO> listaPagosBanco { get;  set; }
        public string Usuario { get; set; }
    }
}
