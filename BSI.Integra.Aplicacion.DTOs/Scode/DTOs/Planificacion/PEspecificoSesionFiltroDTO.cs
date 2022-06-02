using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoSesionFiltroDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Duracion { get; set; }
        public string DuracionTotal { get; set; }   
        public string Curso { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int? PEspecificoHijoId { get; set; }
        public string Tipo { get; set; }
        public int CentroCosto { get; set; }
    }

    public class PEspecificoSesionWebexDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
    }

    public class PEspecificoSesionGrupoAnteriorDTO
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public int IdPEspecifico { get; set; }
        public decimal? Duracion { get; set; }
        public decimal? DuracionTotal { get; set; }
        public int? IdExpositor { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int Grupo { get; set; }
        public string ModalidadSesion { get; set; }
    }

    public class PEspecificoSesionGrupoCompletoDTO
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public int IdPEspecifico { get; set; }
        public double Duracion { get; set; }
        public string DuracionTotal { get; set; }
        public int? IdExpositor { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int Grupo { get; set; }
        public string ModalidadSesion { get; set; }        
    }
    public class PEspecificoSesionWebexUrlDTO
    {
        public int IdPEspecifico { get; set; }
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public int IdTipoPrograma { get; set; }
        public string UrlWebex { get; set; }
        public int Grupo{ get; set; }
    }
    public class PEspecificoSesionRecuperacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public int? IdRecuperacionSesion { get; set; }

    }
    public class PEspecificoProximaSesionWebexDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreDia { get; set; }
        public string UrlWebex { get; set; }
    }
}
