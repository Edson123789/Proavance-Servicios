using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ArticuloInsertDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdArticulo { get; set; }
        public int IdParametroSeo { get; set; }
    }
}
