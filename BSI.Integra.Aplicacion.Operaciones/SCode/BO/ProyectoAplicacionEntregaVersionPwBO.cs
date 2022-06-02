using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class ProyectoAplicacionEntregaVersionPwBO : BaseBO
    {
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public int Version { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
}
