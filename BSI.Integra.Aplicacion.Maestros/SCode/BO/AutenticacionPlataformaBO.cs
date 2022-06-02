using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Maestros.BO
{
    public class AutenticacionPlataformaBO : BaseBO
    {
        public string Producto { get; set; }
        public string Autenticacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
