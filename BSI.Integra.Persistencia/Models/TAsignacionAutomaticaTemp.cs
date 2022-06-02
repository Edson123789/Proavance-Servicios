﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAsignacionAutomaticaTemp
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Origen { get; set; }
        public bool? Procesado { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public int? IdPagina { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public int? IdFacebookFormularioLeadgen { get; set; }
        public bool? AptoProcesamiento { get; set; }
    }
}
