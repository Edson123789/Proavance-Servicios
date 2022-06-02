using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ValorTipoDTO
    {
        public string Areas { get; set; }
        public string SubAreas { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string ProbabilidadRegistro { get; set; }
        public string Paises { get; set; }
        public string Ciudades { get; set; }
        public string CategoriaDato { get; set; }
        public string Cargos { get; set; }
        public string Industrias { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string FaseOportunidadInicial { get; set; }
        public string FaseOportunidadMaximaInicial { get; set; }
        public string FaseHistorica { get; set; }
        public string FaseHistoricaMaxima { get; set; }
        public DateTime? FechaInicioOportunidad { get; set; }
        public DateTime FechaFinOportunidad { get; set; }
        public DateTime SinActividadAlumno { get; set; }
        public int IdFiltroSegmento { get; set; }
    }
}
