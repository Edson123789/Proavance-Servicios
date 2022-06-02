using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs { 
    public class FaseEstadoOcurrenciaDTO
    {
        public int Id { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdEstadoOcurrencia { get; set; }
    }
}
