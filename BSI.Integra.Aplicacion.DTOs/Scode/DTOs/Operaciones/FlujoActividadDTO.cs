using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoActividadDTO
    {
        public int Id { get; set; }
        public int IdFlujoFase { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public string NombreUsuario { get; set; }
    }
}
