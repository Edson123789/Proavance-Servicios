﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoNuevoAulaVirtual
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}