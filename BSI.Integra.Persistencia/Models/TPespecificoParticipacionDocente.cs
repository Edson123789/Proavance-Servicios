﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoParticipacionDocente
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdExpositor { get; set; }
        public bool? ConfirmaParticipacion { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
