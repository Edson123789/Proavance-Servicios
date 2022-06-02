using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebinarDetalleBO : BaseBO
    {
        public WebinarDetalleBO()
        {
            ListaWebinarAsistencia = new HashSet<WebinarAsistenciaBO>();
        }

        public int IdWebinar { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Grupo { get; set; }
        public string Link { get; set; }
        public bool EsCancelado { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<WebinarAsistenciaBO> ListaWebinarAsistencia { get; set; }
    }
}
