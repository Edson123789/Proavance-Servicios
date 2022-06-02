using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CertificacionPGeneralBO : BaseBO
    {
        public int Id { get; set; }
        public int IdCertificacion { get; set; }
        public int? IdPGeneral { get; set; }

    }
}
