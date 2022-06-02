using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapinPortalEmpleoDetalleDTO
    {
        public int Id { get; set; }
        public int IdScrapingPagina { get; set; }
        public string NombrePortal { get; set; }
        public string TituloAnuncio { get; set; }
        public string Url { get; set; }
        public DateTime FechaAnuncio { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string Ubicacion { get; set; }
        public string Jornada { get; set; }
        public string TipoContrato { get; set; }
        public string Salario { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionHTML { get; set; }
        public string Error { get; set; }
        public string Modalidad { get; set; }
    }
}
