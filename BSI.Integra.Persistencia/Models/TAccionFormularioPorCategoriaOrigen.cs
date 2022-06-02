﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAccionFormularioPorCategoriaOrigen
    {
        public int Id { get; set; }
        public int IdAccionFormulario { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid IdMigracion { get; set; }

        public virtual TAccionFormulario IdAccionFormularioNavigation { get; set; }
        public virtual TCategoriaOrigen IdCategoriaOrigenNavigation { get; set; }
    }
}
