using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PaisPorPEspecificoFiltroDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
    }
}
