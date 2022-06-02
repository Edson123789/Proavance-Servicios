using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones
{
    public class CategoriaMoodleDTO
    {
        public int Id { get; set; }
        public int IdMoodleCategoriaTipo { get; set; }
        public string MoodleCategoriaTipo { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public bool AplicaProyecto { get; set; }
        public string Usuario { get; set; }

    }
}
