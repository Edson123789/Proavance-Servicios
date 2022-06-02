using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteComisionesDTO
    {
        public int IdPersonal { get; set; }
        public string Nombre{ get; set; }
        public decimal MontoComisionSoles { get; set; }
        public decimal MontoComisionDolares { get; set; }
    }
}
