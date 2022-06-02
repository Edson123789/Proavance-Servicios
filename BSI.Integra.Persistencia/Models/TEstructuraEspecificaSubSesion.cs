﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEstructuraEspecificaSubSesion
    {
        public int Id { get; set; }
        public int IdEstructuraEspecificaSesion { get; set; }
        public int Numero { get; set; }
        public string SubSesion { get; set; }
        public int IdEstructuraEspecifica { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
