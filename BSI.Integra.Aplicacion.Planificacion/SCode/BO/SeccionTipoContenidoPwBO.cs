using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class SeccionTipoContenidoPwBO : BaseEntity
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
