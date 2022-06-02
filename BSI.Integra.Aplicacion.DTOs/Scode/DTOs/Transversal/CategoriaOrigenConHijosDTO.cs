using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaOrigenConHijosDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdTipoFormulario { get; set; }
        public string NombreTipoFormulario { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string Nombre { get; set; }
    }
}
