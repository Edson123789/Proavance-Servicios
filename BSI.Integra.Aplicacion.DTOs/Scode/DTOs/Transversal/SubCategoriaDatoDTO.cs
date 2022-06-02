using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubCategoriaDatoDTO
    {
        public int Id { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int IdTipoFormulario { get; set; }
        public string Usuario { get; set; }
    }
}
