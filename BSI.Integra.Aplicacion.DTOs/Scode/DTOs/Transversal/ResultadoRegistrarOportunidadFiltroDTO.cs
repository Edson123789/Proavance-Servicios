using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResultadoRegistrarOportunidadFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public int? IdTipoDato { get; set; }
        public string NombreTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string CodigoFase { get; set; }
        public string CodigoFaseMaxima { get; set; }
        public int? IdOrigen { get; set; }
        public string NombreOrigen { get; set; }
        public int? CodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? CodigoCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraPeru { get; set; }
        public string HoraContacto { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCargo { get; set; }
        public int? IdFormacion { get; set; }
        public int? IdTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdOportunidad { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public int? IdReferido { get; set; }
        public bool? Asociado { get; set; }
        public string NombreGrupo { get; set; }
        public string CodigoMailing { get; set; }
    }
}
