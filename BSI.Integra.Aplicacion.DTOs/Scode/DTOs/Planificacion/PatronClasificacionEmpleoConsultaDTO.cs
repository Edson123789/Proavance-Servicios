using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PatronClasificacionEmpleoConsultaDTO
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public int IdItem { get; set; }
        public string Item { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Patron { get; set; }
    }
}
