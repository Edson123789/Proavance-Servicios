using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class SolucionBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdCausa { get; set; }
        public int IdAgendaTipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
