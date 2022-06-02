﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMoodleCategoria
    {
        public int Id { get; set; }
        public int IdMoodleCategoriaTipo { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public bool AplicaProyecto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMoodleCategoriaTipo IdMoodleCategoriaTipoNavigation { get; set; }
    }
}
