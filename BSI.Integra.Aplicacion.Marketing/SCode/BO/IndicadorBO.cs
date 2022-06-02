using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class IndicadorBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Meta { get; set; }
        public bool Verificacion { get; set; }
        public int? IdCategoriaIndicador { get; set; }
    }
}
