using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParametroSeoPorTagDTO
    {
        public int Id { get; set; }
        public int IdParametroSeo { get; set; }
        public string NombreParametroSeo { get; set; }
        public string Descripcion { get; set; }
    }
}
