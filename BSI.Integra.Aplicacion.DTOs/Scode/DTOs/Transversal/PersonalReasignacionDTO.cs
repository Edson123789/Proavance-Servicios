using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalReasignacionDTO
    {
        public int IdAsesor { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public int IdJefe { get; set; }
        public string NombreCompletoJefe { get; set; }
        public string EmailJefe { get; set; }
    }
}
