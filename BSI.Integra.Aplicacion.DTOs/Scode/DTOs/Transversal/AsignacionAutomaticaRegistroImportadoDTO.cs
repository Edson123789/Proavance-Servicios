using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaRegistroImportadoDTO
    {
        public int TotalRegistros { get; set; }
        public string Alumno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string CentroCosto { get; set; }
        public string TipoDato { get; set; }
        public string Origen { get; set; }
        public string CodigoFase { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string Cargo { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string origenCampania { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public string NombreProbabilidadActual { get; set; }
    }
}
