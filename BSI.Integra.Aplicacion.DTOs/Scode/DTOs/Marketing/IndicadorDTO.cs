using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class IndicadorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Meta { get; set; }
        public bool Verificacion { get; set; }
        public int? IdCategoriaIndicador { get; set; }
        public string Usuario { get; set; }
    }
}
