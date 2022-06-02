﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSueldoPorIndustriaDataTotal
    {
        public int Id { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
