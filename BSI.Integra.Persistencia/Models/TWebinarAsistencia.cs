﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWebinarAsistencia
    {
        public int Id { get; set; }
        public int IdWebinarDetalle { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool? ConfirmoAsistencia { get; set; }
        public bool Asistio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
        public virtual TWebinarDetalle IdWebinarDetalleNavigation { get; set; }
    }
}
