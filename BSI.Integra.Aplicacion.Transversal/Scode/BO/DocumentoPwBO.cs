using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentoPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public List<DocumentoSeccionPwBO> DocumentoSeccion { get; set; }
        public List<BandejaPendientePwBO> BandejaPendiente { get; set; }
    }
}
