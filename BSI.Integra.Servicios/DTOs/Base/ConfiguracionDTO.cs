using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.DTOs
{
    public class ConfiguracionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string RowVersion { get; set; }
    }
}
