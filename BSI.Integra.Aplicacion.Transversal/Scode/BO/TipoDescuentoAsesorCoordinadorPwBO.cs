using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoDescuentoAsesorCoordinadorPwBO : BaseBO
    {
        public int IdAgendaTipoUsuario { get; set; }
        public int? IdTipoDescuento { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
