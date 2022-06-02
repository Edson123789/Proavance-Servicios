using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AscensoCertificacionBO : BaseBO
    {
        public int Id { get; set; }
        public int IdAscenso { get; set; }
        public int IdCertificacion { get; set; }

    }
}
