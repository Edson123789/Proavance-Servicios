using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class RecuperacionAutomaticoModuloSistemaBO : BaseBO
    {
        public int IdModuloSistema { get; set; }
        public string Tipo { get; set; }
        public bool Habilitado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
