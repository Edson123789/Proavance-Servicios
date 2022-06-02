using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComboConjuntoAnuncioDTO
    {
        public List<PGeneralFiltroConUrlDTO> ProgramasGenerales { get; set; }
        public List<CategoriaOrigenFiltroDTO> CategoriasOrigen { get; set; }
    }
}
