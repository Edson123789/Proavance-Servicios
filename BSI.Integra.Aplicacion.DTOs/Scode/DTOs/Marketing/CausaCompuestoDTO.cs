using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CausaCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdProblema { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<SolucionDTO> Soluciones { get; set; }
        public string Usuario { get; set; }
    }
}
