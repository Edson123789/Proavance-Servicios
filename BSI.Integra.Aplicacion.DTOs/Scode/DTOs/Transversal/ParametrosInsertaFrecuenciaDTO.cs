using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametrosInsertaFrecuenciaDTO
    {
        public List<PEspecificoFrecuenciaDetalleDTO> listaDetalles { get; set; }
        public List<PEspecificoFrecuenciaDetalleDTO> listaDetallesWebinar { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdFrecuencia { get; set; }
        public DateTime? FechaInicioWebinar { get; set; }
        public int? IdFrecuenciaWebinar { get; set; }
        public string Usuario { get; set; }
        public List<int> ListaPEspecificos { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? ValorTiempoFrecuencia { get; set; }
        public int? IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int? ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCorreo { get; set; }
        public int? ValorFrecuenciaCorreo { get; set; }
        public int? IdTiempoFrecuenciaWhatsapp { get; set; }
        public int? ValorFrecuenciaWhatsapp { get; set; }
        public int? IdPlantillaFrecuenciaCorreo { get; set; }
        public int? IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int? IdPlantillaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int? ValorFrecuenciaDocente { get; set; }
        public int? IdPlantillaDocente { get; set; }
        public bool? CheckTiempoFrecuencia { get; set; }
        public bool? CheckEnvioCorreo { get; set; }
        public bool? CheckEnvioWhatsApp { get; set; }
        public bool? CheckEnvioCorreoConfirmacion { get; set; }
        public bool? CheckEnvioCorreoDocente { get; set; }






    }
}
