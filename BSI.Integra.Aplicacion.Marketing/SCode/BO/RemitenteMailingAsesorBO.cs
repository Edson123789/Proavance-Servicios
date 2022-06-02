using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class RemitenteMailingAsesorBO : BaseBO
    {
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
