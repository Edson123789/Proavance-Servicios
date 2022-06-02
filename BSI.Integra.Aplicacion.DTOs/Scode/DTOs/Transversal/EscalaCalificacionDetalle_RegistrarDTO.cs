using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EscalaCalificacionDetalle_RegistrarDTO
    {
        public int Id { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
    }
}
