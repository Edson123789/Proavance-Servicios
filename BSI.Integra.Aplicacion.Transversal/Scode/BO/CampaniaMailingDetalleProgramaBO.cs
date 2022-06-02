using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CampaniaMailingDetalleProgramaBO : BaseBO
    {
        public int IdCampaniaMailingDetalle { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Orden { get; set; }
        public Guid? IdMigracion { get; set; }

        public CampaniaMailingDetalleProgramaBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
