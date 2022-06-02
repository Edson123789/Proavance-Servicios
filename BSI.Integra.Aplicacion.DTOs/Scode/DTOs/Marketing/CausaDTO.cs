using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CausaDTO
    {
        public int Id { get; set; }
        public int? IdProblema { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
