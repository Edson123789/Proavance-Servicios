using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public string NombreSubAreaCapacitacion { get; set; }
    }

    public class PGeneralSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public string NombreSubAreaCapacitacion { get; set; }
    }
}
