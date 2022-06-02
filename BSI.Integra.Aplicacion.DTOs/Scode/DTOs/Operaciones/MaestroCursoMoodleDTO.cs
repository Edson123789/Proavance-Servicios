using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones
{
    public class MaestroCursoMoodleDTO
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int IdCursoMoodle { get; set; }
        public string NombreCursoMoodle { get; set; }
        public string Usuario { get; set; }
    }
}
