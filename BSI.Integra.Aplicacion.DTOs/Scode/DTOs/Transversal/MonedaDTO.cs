using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class MonedaDTO
    {
        int Id { get; set; }
        string Nombre { get; set; }
    }
    public class CodigoMonedaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
    }
}
