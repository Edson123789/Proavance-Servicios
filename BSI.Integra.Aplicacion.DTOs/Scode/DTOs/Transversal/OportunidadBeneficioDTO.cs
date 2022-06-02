using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadBeneficioDTO
    {
        public int IdOportunidadCompetidor { get; set; }
        public int IdBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
    }
    public class OportunidadMotivacionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMotivacion { get; set; }
        public int Respuesta { get; set; }
    }

    public class OportunidadPublicoObjetivoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPublicoObjetivo { get; set; }
        public int Respuesta { get; set; }
    }
    public class OportunidadCertificacionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCertificacion { get; set; }
        public int Respuesta { get; set; }
    }
    public class OportunidadBeneficioAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdBeneficio { get; set; }
        public int Respuesta { get; set; }
    }
}
