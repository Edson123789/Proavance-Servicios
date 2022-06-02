using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookAudienciaBO : BaseBO
    {
        public int IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Subtipo { get; set; }
        public string RecursoArchivoCliente { get; set; }
        public Guid? IdMigracion { get; set; }


        public FacebookAudienciaBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
