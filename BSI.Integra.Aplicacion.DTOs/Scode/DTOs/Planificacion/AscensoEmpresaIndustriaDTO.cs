﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AscensoEmpresaIndustriaDTO
    {
        public int Id { get; set; }
        public string CargoMercado { get; set; }
        public bool? Activo { get; set; }
        public string FechaPublicacion { get; set; }
        public int? SueldoMin { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdPortalEmpleo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int? IdCiudad { get; set; }
        public string UrlOferta { get; set; }


        public string NombreEmpresa { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public int? IdIndustria { get; set; }
        public string NombreIndustria { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public string NombreTamanioEmpresa { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
