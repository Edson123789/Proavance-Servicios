using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoCategoriaOrigenConHijosDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public List<SubCategoriaFormularioDTO> SubCategorias {get;set; }
    }
}
