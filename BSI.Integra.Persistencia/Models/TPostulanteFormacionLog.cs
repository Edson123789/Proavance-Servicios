﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteFormacionLog
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdPostulanteFormacion { get; set; }
        public int? IdCentroEstudio { get; set; }
        public int? IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string OtraInstitucion { get; set; }
        public string OtraCarrera { get; set; }
        public bool? AlaActualidad { get; set; }
        public string TurnoEstudio { get; set; }
        public int? IdPais { get; set; }
        public string TipoActualizacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
