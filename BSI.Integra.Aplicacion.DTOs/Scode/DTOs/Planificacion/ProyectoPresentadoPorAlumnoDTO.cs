using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProyectoPresentadoPorAlumnoDTO
    {
        public string IdEnvio { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string NombreArchivo { get; set; }
        public string FechaEnvio { get; set; }
        public string HoraEnvio { get; set; }
        public string FechaCalificacion { get; set; }
        public string HoraCalificacion { get; set; }
        public string Nota { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string Docente { get; set; }
        public string ResponsableCoordinacion { get; set; }
        public string NroEnvio { get; set; }
        public string Comentarios { get; set; }

    }
    public class centroCostoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
    }

    public class ProyectoPresentadoPorAlumnoFiltroDTO
    {
        public List<int> ProgramaEspecifico { get; set; }
        public List<int> CentroCosto { get; set; }
        public List<int> Docente { get; set; }
        public List<int> Coordinadora { get; set; }
        public int CodigoMatricula { get; set; }
        public int? EstadoRevision { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }

    }

}
