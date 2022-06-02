﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLogRecuperacionAutomaticoModuloSistema
    {
        public int Id { get; set; }
        public int IdRecuperacionAutomaticoModuloSistema { get; set; }
        public string Tipo { get; set; }
        public bool Habilitado { get; set; }
        public bool EnvioCorreo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
