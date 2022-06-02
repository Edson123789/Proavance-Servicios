using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BitacoraSesionDetalleDTO
    {
        public DateTime? HoraInicio { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Detalle { get; set; }
    }
}
