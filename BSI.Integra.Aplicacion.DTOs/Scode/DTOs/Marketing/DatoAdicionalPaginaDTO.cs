using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatoAdicionalPaginaDTO
    {
        public int? Id { get; set; }
        public int IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreImagen { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorDescripcion { get; set; }
    }
}
