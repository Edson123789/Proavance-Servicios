using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MovilidadDTO
    {
        public int Id { get; set; }
        public int IdTipoMovilidad { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
    }
}
