﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCreativoPublicidad
    {
        public TCreativoPublicidad()
        {
            TAnuncio = new HashSet<TAnuncio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TAnuncio> TAnuncio { get; set; }
    }
}