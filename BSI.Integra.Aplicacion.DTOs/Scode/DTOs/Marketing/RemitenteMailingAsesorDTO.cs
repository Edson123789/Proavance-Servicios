using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RemitenteMailingAsesorDTO
    {
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
