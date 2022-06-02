using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PGeneralFiltroConUrlDTO
    {
        public int Id { get; set; }
        public int IdBusqueda { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
    }
}
