using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Servicios.BO;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class UnsuscribedMailchimpBO : BaseBO
    {
        public string CampaniaId { get; set; }
        public string Email { get; set; }
        public string EmailId { get; set; }
        public string ListaId { get; set; }
        public string Reason { get; set; }
        public string FechaUnsuscribed { get; set; }

    }
}
