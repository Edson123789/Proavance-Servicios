﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteInformacionImportacion
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? CantidadTotal { get; set; }
        public int? CantidadPrimerCriterio { get; set; }
        public int? CantidadSegundoCriterio { get; set; }
        public int? CantidadFinal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TProcesoSeleccion IdProcesoSeleccionNavigation { get; set; }
    }
}
