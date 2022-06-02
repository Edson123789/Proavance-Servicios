﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppObjetoSerealizado
    {
        public int Id { get; set; }
        public string WaObjeto { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPais IdPaisNavigation { get; set; }
    }
}
