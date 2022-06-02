using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TagPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? TagWebId { get; set; }
        public string Codigo { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<TagParametroSeoPwBO> TagParametroSeo { get; set; }
        public List<PGeneralTagPwBO> PGeneralTag { get; set; }
    }
}
