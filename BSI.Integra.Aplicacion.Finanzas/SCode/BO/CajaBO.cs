using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CajaBO : BaseBO
    {
        public string CodigoCaja { get; set; }
        public int IdMoneda { get; set; }
        public int IdEmpresaAutorizada { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonalResponsable { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
