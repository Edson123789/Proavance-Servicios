using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalFormacionFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public int? IdTipoEstudio { get; set; }
        public string TipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? AlaActualidad { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public string EstadoEstudio { get; set; }
        public string Logro { get; set; }
        public bool Estado { get; set; }


    }
}
