using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TroncalListaDTO
    {
        public int Id { get; set; }
        public int IdRegionCiudad { get; set; }
        public int IdCategoriaPrograma { get; set; }
        public string TroncalCompleto { get; set; }
        public string NombreRegionCiudad { get; set; }
        public string NombreCategoriaPrograma { get; set; }
    }
}
