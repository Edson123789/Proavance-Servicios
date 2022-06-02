using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PatronClasificacionEmpleoRegistrarDTO
    {
        public int Id { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Patron { get; set; }
        public string Usuario { get; set; }
    }
}
