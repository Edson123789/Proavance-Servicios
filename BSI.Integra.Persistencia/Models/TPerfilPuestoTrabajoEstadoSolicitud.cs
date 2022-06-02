﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPerfilPuestoTrabajoEstadoSolicitud
    {
        public TPerfilPuestoTrabajoEstadoSolicitud()
        {
            TPerfilPuestoTrabajo = new HashSet<TPerfilPuestoTrabajo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TPerfilPuestoTrabajo> TPerfilPuestoTrabajo { get; set; }
    }
}
