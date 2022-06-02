using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ProductoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CuentaGeneral { get; set; }
        public string CuentaGeneralCodigo { get; set; }
        public string CuentaEspecifica { get; set; }
        public string CuentaEspecificaCodigo { get; set; }
        public int IdProductoPresentacion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
