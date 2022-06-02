using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoAnuncioPanelDTO
    {
        public int Id { get; set; }
        public string IdCampaniaFacebook { get; set; }
        public int IdProveedor { get; set; }
        public DateTime? FechaCreacionCampania { get; set; }
        public string Nombre { get; set; }
        public string Total { get; set; }


    }
}
