using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConjuntoAnuncioFiltroCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Codigo { get; set; }
    }
}
