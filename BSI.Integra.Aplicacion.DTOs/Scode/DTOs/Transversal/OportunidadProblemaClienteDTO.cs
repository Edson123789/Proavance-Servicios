using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadProblemaClienteDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; }
        public int IdCausa { get; set; }
        public string NombreCausa { get; set; }
        public int IdSolucion { get; set; }
        public string NombreSolucion { get; set; }
        public string DescripcionSolucion { get; set; }
        public string Seleccionado { get; set; }
        public string Solucionado { get; set; }
        public string OtroProblema { get; set; }
    }
}
