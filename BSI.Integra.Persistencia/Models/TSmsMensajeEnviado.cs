﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSmsMensajeEnviado
    {
        public int Id { get; set; }
        public string Celular { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public string Mensaje { get; set; }
        public int ParteMensaje { get; set; }
        public int? IdPais { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
