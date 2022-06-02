using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AprobarHistorialCambioDTO
    {
        public int Id { get; set; }
        public bool Aprobar { get; set; }
        public string NombreUsuario { get; set; }
    }
}
