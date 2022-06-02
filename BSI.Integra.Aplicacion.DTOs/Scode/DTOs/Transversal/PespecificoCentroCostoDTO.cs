using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class PespecificoCentroCostoDTO
    {
        public int? IdCentroCosto { get; set; }
    }
    public class PespecificoAlumnosDTO
    {
        public int IdAlumno { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? ZonaHoraria { get; set; }

    }

    public class SesionAlumnoWebinarDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public string UrlWebex { get; set; }
        public bool? EsWebinarConfirmado { get; set; }
        public int? EsHoy { get; set; }
        public int? EsAfuturo { get; set; }
        public int? NoHay { get; set; }

    }

    public class PespecificoDocenteDTO
    {
        public int IdSesion { get; set; }
        public int IdDocente { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? ZonaHoraria { get; set; }

    }

    public class PespecificoAlumnosConfirmadosDTO
    {
        public int IdAlumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? ZonaHoraria { get; set; }
        public int IdWebinar { get; set; }
        public int IdMatriculaCabecera { get; set; }

    }

    public class ConfiguracionWebinarDTO
    {
        public int? IdOperadorComparacionAvance { get; set; }
        public int? ValorAvance { get; set; }
        public int? ValorAVanceOpc { get; set; }
        public int? IdOperadorComparacionPromedio { get; set; }
        public int? ValorPromedio { get; set; }
        public int? ValorPromedioOpc { get; set; }
        public int IdPEspecifico { get; set; }
    }

    public class AvanceAlumnoDTO
    {
        public int? ValorAvance { get; set; }
        public decimal? ValorPromedio { get; set; }
        public int IdMatriculaCabecera { get; set; }

    }
}
