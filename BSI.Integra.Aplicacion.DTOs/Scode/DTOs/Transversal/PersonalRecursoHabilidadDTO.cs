using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalRecursoHabilidadDTO
    {
        public int Id { get; set; }
        public int IdPersonalRecurso { get; set; }
        public int IdHabilidadSimulador { get; set; }
        public int Puntaje { get; set; }
        public string Usuario { get; set; }
    }
}
