using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TipoAmbienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }

    public class ProgramaGeneralPuntoCorteAEliminarDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }

    public class ProgramaGeneralPuntoCorteDTO
    {
        public int Id { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMedia { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteAlta { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMuyAlta { get; set; }
        public string Usuario { get; set; }
    }

    public class ProgramaGeneralPuntoCorteDetalleDTO
    {
        public int? Id { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
    }
    public class ProgramaGeneralPuntoCorteConfiguracionDTO
    {
        public int? Id { get; set; }
        public int IdTipoCorte { get; set; }
        public string Tipo { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public string Color { get; set; }
        public string Texto { get; set; }
        public string Usuario { get; set; }
    }
    public class ListaProgramaGeneralPuntoCorteConfiguracionDTO
    {
        public List<ProgramaGeneralPuntoCorteConfiguracionDTO> Datos { get; set; }
        public string Usuario { get; set; }
    }

    public class ProgramaGeneralPuntoCorteMasivoDTO
    {
        public List<int> ListaIdPGeneral { get; set; }
        public decimal PuntoCorteMedia { get; set; }
        public decimal PuntoCorteAlta { get; set; }
        public decimal PuntoCorteMuyAlta { get; set; }
        public string Usuario { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMedia { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteAlta { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMuyAlta { get; set; }
    }

    public class ProgramaGeneralPuntoCorteFiltroDTO
    {
        public List<int> ListaIdAreaCapacitacion { get; set; }
        public List<int> ListaIdSubAreaCapacitacion { get; set; }
        public List<int> ListaIdProgramaGeneral { get; set; }
        public bool? ActivoProgramaGeneral { get; set; }
    }

    public class ProgramaGeneralPuntoCorteAreaSubAreaDTO
    {
        public int? IdProgramaGeneralPuntoCorte { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public decimal PuntoCorteMedia { get; set; }
        public decimal PuntoCorteAlta { get; set; }
        public decimal PuntoCorteMuyAlta { get; set; }
        public string Usuario { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
    }
}
