using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSueldoIndividualIncluirDTO
    {
        public int Id { get; set; }
        public bool? Incluir { get; set; }

        public string Usuario { get; set; }
    }
}
