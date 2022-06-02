using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ElementoDTO
    {
        public int Id { get; set; }
        public int IdElementoSubCategoria { get; set; }
        public int? IdElementoCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreElementoSubCategoria { get; set; }
        public string NombreElementoCategoria { get; set; }
        public string Usuario { get; set; }
    }
}
