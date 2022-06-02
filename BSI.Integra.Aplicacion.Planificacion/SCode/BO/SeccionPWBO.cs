using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class SeccionPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public Guid? IdMigracion { get; set; }

        public List<SeccionTipoDetallePwBO> SeccionTipoDetallePw { get; set; }
    }
}
