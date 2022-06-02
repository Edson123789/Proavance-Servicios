using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AdwordsApiVolumenBusquedaHistoricoDTO
    {
        public string PalabraClave { get; set; }
        public int PromedioBusqueda { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public int IdPais { get; set; }

    }
}
