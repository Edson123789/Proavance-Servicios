﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModuloSistemaPuestoTrabajo
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdModuloSistema { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TModuloSistema IdModuloSistemaNavigation { get; set; }
        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; }
    }
}
