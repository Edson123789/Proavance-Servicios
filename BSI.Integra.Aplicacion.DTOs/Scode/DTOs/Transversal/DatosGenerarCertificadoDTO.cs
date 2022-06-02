using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosGenerarCertificadoDTO
    {
        public int Id { get; set; }
        public int IdDetalle { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int IdSubEstadoMatricula { get; set; }
    }
}
