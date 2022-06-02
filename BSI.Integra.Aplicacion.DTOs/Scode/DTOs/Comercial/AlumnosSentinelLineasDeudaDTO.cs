using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnosSentinelLineasDeudaDTO
    {
        public string TipoDocCPT { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public System.Int32? DiasVencidos { get; set; }
    }
}
