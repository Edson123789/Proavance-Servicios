using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookAudienciaAlumnoBO : BaseBO
    {
        public int IdFacebookAudiencia { get; set; }
        public int IdAlumno { get; set; }
        public Guid? IdMigracion { get; set; }

        public FacebookAudienciaAlumnoBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
