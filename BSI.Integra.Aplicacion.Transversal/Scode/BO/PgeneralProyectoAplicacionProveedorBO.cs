using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralProyectoAplicacionProveedorBO : BaseBO
    {
        public int IdPgeneralProyectoAplicacion { get; set; }
        public int IdProveedor { get; set; }       
        public int? IdMigracion { get; set; }
    }
}
