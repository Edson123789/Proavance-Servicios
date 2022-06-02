using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ProgramaCapacitacionBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTemaProgramaCapacitacion { get; set; }
        public int? IdPEspecificoAsesor { get; set; }

    }
}
