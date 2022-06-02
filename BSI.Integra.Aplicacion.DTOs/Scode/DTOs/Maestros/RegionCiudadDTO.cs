using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegionCiudadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public int? CodigoBs { get; set; }
        public string DenominacionBs { get; set; }
        public string  NombreCorto { get; set; }
        public string Usuario { get; set; }
    }
}
