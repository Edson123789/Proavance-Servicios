﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionChatIntegra
    {
        public int Id { get; set; }
        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int? IdAlumno { get; set; }
        public Guid? IdContactoPortalSegmento { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public string Ip { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public int? Duracion { get; set; }
        public int? NroMensajes { get; set; }
        public int? NroPalabrasVisitor { get; set; }
        public int? NroPalabrasAgente { get; set; }
        public int? UsuarioTiempoRespuestaMaximo { get; set; }
        public int? UsuarioTiempoRespuestaPromedio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Leido { get; set; }
        public string Plataforma { get; set; }
        public string Navegador { get; set; }
        public string UrlFrom { get; set; }
        public string UrlTo { get; set; }
        public int IdEstadoChat { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public Guid IdChatSession { get; set; }
        public Guid? IdFaseOportunidadPortalWeb { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public decimal? ClienteTiempoEspera { get; set; }
        public int? ContadorUsuarioPromedioRespuesta { get; set; }
        public decimal? TiempoRespuestaTotal { get; set; }
        public int? NroMensajesSinLeer { get; set; }
    }
}
