﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TActividadCabecera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime? FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public byte? DiaFrecuenciaMensual { get; set; }
        public bool? EsRepetitivo { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public byte? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdFacebookCampanha { get; set; }
        public int? IdFacebookCuentaPublicitaria { get; set; }
        public bool? EsEnvioMasivo { get; set; }
    }
}
