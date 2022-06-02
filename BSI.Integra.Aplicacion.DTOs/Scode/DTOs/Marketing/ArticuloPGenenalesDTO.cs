using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ArticuloAsociadosDTO
    {
        public int IdArticulo { get; set; }
        public List<int> IdsAsociados{ get; set; }
        public string Usuario { get; set; }
    }
}
