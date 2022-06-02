using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapinPortalEmpleoRegistrarClasificacionDTO
    {
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public List<ScrapingEmpleoClasificacionEstudioDTO> ListadoEstudio { get; set; }
        public List<ScrapingEmpleoClasificacionExperienciaDTO> ListadoExperiencia { get; set; }
        public List<ScrapingEmpleoClasificacionCertificacionDTO> ListadoCertificacion { get; set; }
        public string NombreUsuario { get; set; }
    }
}