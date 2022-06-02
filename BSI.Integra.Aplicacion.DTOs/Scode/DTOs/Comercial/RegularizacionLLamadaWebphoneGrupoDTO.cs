using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegularizacionLLamadaWebphoneGrupoDTO
    {
        public int IdPersonal { get; set; }
        public string Anexo3cx { get; set; }
        public List<RegularizacionLLamadaWebphoneDTO> Lista { get; set; }
    }
}
