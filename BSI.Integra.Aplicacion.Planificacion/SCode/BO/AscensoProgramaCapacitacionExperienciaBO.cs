using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AscensoProgramaCapacitacionExperienciaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int? IdProgramaCapacitacion { get; set; }
        public string Contenido { get; set; }
    }
}
