﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaFrecuenteSubArea
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int IdSubArea { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPreguntaFrecuente IdPreguntaFrecuenteNavigation { get; set; }
        public virtual TSubAreaCapacitacion IdSubAreaNavigation { get; set; }
    }
}
