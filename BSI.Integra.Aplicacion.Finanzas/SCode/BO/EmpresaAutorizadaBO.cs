using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class EmpresaAutorizadaBO : BaseBO
    {
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public string Central { get; set; }
        public bool Activo { get; set; }
        public int IdPais { get; set; }
        public int? IdMigracion { get; set; }
    }
}
