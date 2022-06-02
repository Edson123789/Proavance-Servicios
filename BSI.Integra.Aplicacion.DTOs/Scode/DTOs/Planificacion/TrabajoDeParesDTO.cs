using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TrabajoDeParesDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Curso { get; set; }
        public int? Version { get; set; }
        public int NroTarea { get; set; }
        public string NombreArchivo { get; set; }
        public string FechaEnvio { get; set; }
        public string FechaCalificacion { get; set; }
        public string Nota { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string MatriculaAlumnoResponsableRevision { get; set; }
        public string NombreAlumnoResponsableRevision { get; set; }
        public string FechaAsignacion { get; set; }
        public string CoordinadorResponsableRevision { get; set; }
        public bool? EsDocente { get; set; }

    }
    public class TrabajoDeParesFiltroDTO
    {
        public List<int> ProgramaEspecifico { get; set; }
        public int CodigoMatricula { get; set; }
        public List<int> Alumno { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public int? EstadoTarea { get; set; }

    }

    public class TrabajoDeParesActualizacion
    {
        public int Id{ get; set; }
        public int IdProveedor { get; set; }
        public int IdPEspecifico { get; set; }
    }

    public class ProgramaCentroCostoDocenteDTO
    {
        public int IdPGeneralPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public int TareasPendientes { get; set; }
    }


    public class AlumnoDocenteTrabajoParesDTO
    {
        public string Alumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public int IdPGeneralPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }

        public int IdPGeneralHijo { get; set; }

        public int IdEvaluacion { get; set; }
        public int IdAlumno { get; set; }
        public bool Calificado { get; set; }

    }

    public class TrabajoDeParesCorreoDTO
    {
        public string Usuario { get; set; }
        public int IdProveedor { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }

    }

    public class TrabajoDeParesCorreoDetalleDTO
    {
        public string NombreProveedor { get; set; }
        public string NombreAlumno { get; set; }
        public string NombrePrograma { get; set; }
        public string Email { get; set; }


    }


}
