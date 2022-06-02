using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class MonedaPaisFiltroDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string NombrePlural { get; set; }
    }
}
