using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DuracionAvanceAcademicoMoodleDTO
    {
        public int Id { get; set; }
        public int IdTipoCapacitacionMoodle { get; set; }
        public int IdMoodle { get; set; }
        public int Duracion { get; set; }
        public int Meses { get; set; }
        public string NombreUsuario { get; set; }
    }
}
