﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalIdioma
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public int IdCentroEstudio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
