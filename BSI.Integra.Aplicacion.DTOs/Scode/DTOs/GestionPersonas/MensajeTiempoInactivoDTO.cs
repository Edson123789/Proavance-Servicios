using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MensajeTiempoInactivoDTO
    {
        public int Id { get; set; }
        public int MinutoInactivo { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
    }
}
