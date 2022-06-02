using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CombosTipoPagoDTO
    {
        public List<CategoriaProgramaFiltroPorNombreDTO> CategoriaPrograma { get; set; }
        public List<ModoPagoDTO> ModoPago { get; set; }
    }
}
