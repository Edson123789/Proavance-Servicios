using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing
{
    public class EnlacesLandingPageWebDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string EsPopUp { get; set; }
        public string DireccionUrl { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
