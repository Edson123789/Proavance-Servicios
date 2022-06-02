﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class THistoricoOportunidadRn2
    {
        public THistoricoOportunidadRn2()
        {
            THistoricoDetalleOportunidadRn2 = new HashSet<THistoricoDetalleOportunidadRn2>();
        }

        public int Id { get; set; }
        public int IdOportunidadRn2 { get; set; }
        public int IdAlumno { get; set; }
        public string EstadoValidacionRn2 { get; set; }
        public int IdFaseOportunidadActual { get; set; }
        public DateTime? FechaCreacionActual { get; set; }
        public DateTime? FechaProgramacionActual { get; set; }
        public DateTime? FechaProgramacionNueva { get; set; }
        public int? IdOportunidadClasificacion { get; set; }
        public int? IdFaseOportunidadClasificacion { get; set; }
        public DateTime FechaLog { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TAlumno IdAlumnoNavigation { get; set; }
        public virtual ICollection<THistoricoDetalleOportunidadRn2> THistoricoDetalleOportunidadRn2 { get; set; }
    }
}
