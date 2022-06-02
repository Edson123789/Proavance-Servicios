using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion
{
    public class OportunidadReasignarDTO
    {
        public int IdAsesor { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public int IdJefe { get; set; }
        public string NombreCompletoJefe { get; set; }
        public int IdOportunidad { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
}
