using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class SeccionTipoDetallePwBO : BaseBO
    {
        public int IdSeccionPw { get; set; }
        public string NombreTitulo { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public int? IdMigracion { get; set; }
    }
}
