using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionConsolidadaFiltroDTO
    {
        public List<int> areas { get; set; }
        public List<int> subareas { get; set; }
        public List<int> pgeneral { get; set; }
        public List<int> pespecifico { get; set; }
        public List<string> modalidades { get; set; }
        public List<string> ciudades { get; set; }
        public bool fecha { get; set; }
        public List<int> coordinadores { get; set; }
        public List<int> asesores { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        //para el reporte de asignacion detalle alumnos operaciones
        public DateTime? FechaInicioMatricula { get; set; }
        public DateTime? FechaFinMatricula { get; set; }
        public DateTime? FechaInicioAsignacion { get; set; }
        public DateTime? FechaFinAsignacion { get; set; }
        public int CheckFecha { get; set; }
        public int Personal { get; set; }
    }
    public class DiscadorPersonalFiltroDTO
    {
        public List<int> asesores { get; set; }
    }
}
