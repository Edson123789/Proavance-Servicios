using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaTipoMoodleDTO
    {
        public int Id { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public string IdTipoCapacitacionMoodle { get; set; }
        public bool? AplicaProyecto { get; set; }
        public string NombreUsuario { get; set; }
        
    }
}
