﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormularioRespuestaPlantilla
    {
        public int Id { get; set; }
        public string NombrePlantilla { get; set; }
        public string ColorTextoPgeneral { get; set; }
        public string ColorTextoDescripcionPgeneral { get; set; }
        public string ColorTextoInvitacionBrochure { get; set; }
        public string TextoBotonBrochure { get; set; }
        public string ColorFondoBotonBrochure { get; set; }
        public string ColorTextoBotonBrochure { get; set; }
        public string ColorBordeInferiorBotonBrochure { get; set; }
        public string ColorTextoBotonInvitacion { get; set; }
        public string ColorFondoBotonInvitacion { get; set; }
        public string FondoBotonLadoInvitacion { get; set; }
        public string UrlimagenLadoInvitacion { get; set; }
        public string TextoBotonInvitacionPagina { get; set; }
        public string TextoBotonInvitacionArea { get; set; }
        public string ContenidoSeccionTelefonos { get; set; }
        public string TextoInvitacionBrochure { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
