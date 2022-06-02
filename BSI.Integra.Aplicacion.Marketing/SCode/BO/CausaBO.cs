using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class CausaBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdProblema { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
