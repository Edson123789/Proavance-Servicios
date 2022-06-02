﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaCabeceraControlCondicionesComision
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool TieneMatriculaPagada { get; set; }
        public bool TieneAsistencia { get; set; }
        public bool TieneDocumento { get; set; }
        public bool EsComisionable { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMatriculaCabecera IdMatriculaCabeceraNavigation { get; set; }
    }
}
