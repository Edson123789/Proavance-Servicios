using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapinPortalEmpleoCabeceraDTO
    {
        public int Id { get; set; }
        public int IdScrapingPagina { get; set; }
        public string NombrePortal { get; set; }
        public string TituloAnuncio { get; set; }
        public DateTime FechaAnuncio { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string Salario { get; set; }
    }
}
