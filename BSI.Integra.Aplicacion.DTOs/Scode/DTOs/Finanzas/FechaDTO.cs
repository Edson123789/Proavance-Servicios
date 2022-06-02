using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FechaDTO
    {
        public DateTime FechaReprogramacion { get; set; }
        public int? idFur { get; set; }
        public int? idComprobantePago { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class FechaCronogramaDTO {

        public DateTime FechaReprogramacion { get; set; }
        public int idCronogramaPagoDetalleFinal { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
