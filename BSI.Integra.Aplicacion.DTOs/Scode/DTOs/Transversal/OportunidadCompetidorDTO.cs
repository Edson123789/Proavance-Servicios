using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadCompetidorDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string OtroBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public CalidadProcesamientoDTO CalidadBO { get; set; }
    }
}
