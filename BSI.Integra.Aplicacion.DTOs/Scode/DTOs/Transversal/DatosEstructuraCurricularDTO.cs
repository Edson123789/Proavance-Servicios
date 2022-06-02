using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosEstructuraCurricularDTO
    {
        public List<DatosEstructuraEspecificaDTO> DatosEstructura { get; set; }
        public string Usuario { get; set; }
    }

    public class DatosEstructuraEspecificaSubSesionDTO
    {
        public int Id { get; set; }
        public int IdSesion { get; set; }
        public int Numero { get; set; }
        public string SubSesion { get; set; }
        public int IdEstructuraEspecifica { get; set; }
    }
    public class DatosEstructuraEspecificaSesionDTO
    {
        public int Id { get; set; }
        public int IdCapitulo { get; set; }
        public int Numero { get; set; }
        public string Sesion { get; set; }
        public int? OrdenSesion { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public List<DatosEstructuraEspecificaSubSesionDTO> SubSesion { get; set; }
    }
    public class DatosEstructuraEspecificaCapituloDTO
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Capitulo { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public List<DatosEstructuraEspecificaSesionDTO> Sesion { get; set; }
    }
    public class DatosEstructuraEspecificaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPGeneralPadre { get; set; }
        public int IdPGeneralHijo { get; set; }
        //public DateTime FechaRegistro { get; set; }
        //public DateTime FechaInicio { get; set; }
        //public DateTime FechaFin { get; set; }
        public List<DatosEstructuraEspecificaCapituloDTO> Capitulo { get; set; }
        public List<DatosEstructuraEspecificaTareaDTO> Tarea { get; set; }
        public List<DatosEstructuraEspecificaEncuestaDTO> Encuesta { get; set; }
    }
    public class DatosEstructuraEspecificaTareaDTO
    {
        public int Id { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public string NombreTarea { get; set; }
        public int IdTarea { get; set; }
        public int OrdenCapitulo { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
    }
    public class DatosEstructuraEspecificaEncuestaDTO
    {
        public int Id { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public int IdEncuesta { get; set; }
        public string NombreEncuesta { get; set; }
        public int OrdenCapitulo { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
    }
}
