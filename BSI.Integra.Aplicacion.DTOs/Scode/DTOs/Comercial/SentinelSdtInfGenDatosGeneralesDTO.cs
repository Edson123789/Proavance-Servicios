using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSdtInfGenDatosGeneralesDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Digito { get; set; }
        public string DigitoAnterior { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string TipoContribuyente { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionContribuyente { get; set; }
        public string Ciiu { get; set; }
        public DateTime? FechaActividad { get; set; }
        public string Dependencia { get; set; }
        public string Folio { get; set; }
        public string Asiento { get; set; }
        public string Patron { get; set; }
    }
}
