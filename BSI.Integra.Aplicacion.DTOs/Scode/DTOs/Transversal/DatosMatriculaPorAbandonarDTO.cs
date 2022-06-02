using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosMatriculaPorAbandonarDTO
    {
        public int IdMatriculacabecera { get; set; }
        public int IdOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public string Email1 { get; set; }
        public int IdAlumno { get; set; }
        public string EmailCoordinador { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
    }
}
