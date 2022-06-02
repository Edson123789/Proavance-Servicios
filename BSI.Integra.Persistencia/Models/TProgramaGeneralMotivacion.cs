﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProgramaGeneralMotivacion
    {
        public TProgramaGeneralMotivacion()
        {
            TProgramaGeneralMotivacionArgumento = new HashSet<TProgramaGeneralMotivacionArgumento>();
            TProgramaGeneralMotivacionModalidad = new HashSet<TProgramaGeneralMotivacionModalidad>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TProgramaGeneralMotivacionArgumento> TProgramaGeneralMotivacionArgumento { get; set; }
        public virtual ICollection<TProgramaGeneralMotivacionModalidad> TProgramaGeneralMotivacionModalidad { get; set; }
    }
}
