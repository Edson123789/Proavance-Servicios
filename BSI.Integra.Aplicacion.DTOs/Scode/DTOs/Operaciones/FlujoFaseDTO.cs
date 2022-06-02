using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoFaseDTO
    {
        public int Id { get; set; }
        public int IdFlujo { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }

        public string NombreUsuario { get; set; }
    }
}
