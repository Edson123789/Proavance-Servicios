using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaOrigenSubCategoriaDatoDTO
    {
        public int IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int IdSubCategoriaDato { get; set; }
        public string CodigoOrigen { get; set; }
    }
}
