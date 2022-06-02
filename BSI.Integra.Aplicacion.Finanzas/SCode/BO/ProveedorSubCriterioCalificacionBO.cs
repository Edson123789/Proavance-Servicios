using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ProveedorSubCriterioCalificacionBO : BaseBO
    {
        public int IdProveedorCriterioCalificacion { get; set; }
        public string Nombre { get; set; }
        public int Puntaje { get; set; }
        public int? IdMigracion { get; set; }
    }
}
