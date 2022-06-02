﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloGeneralAformacion
    {
        public int Id { get; set; }
        public int IdModeloGeneral { get; set; }
        public int IdAreaFormacion { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAreaFormacion IdAreaFormacionNavigation { get; set; }
        public virtual TModeloGeneral IdModeloGeneralNavigation { get; set; }
    }
}
