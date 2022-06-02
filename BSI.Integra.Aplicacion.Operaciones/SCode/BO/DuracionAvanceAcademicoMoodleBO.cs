﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class DuracionAvanceAcademicoMoodleBO : BaseBO
    {
        public int Id { get; set; }
        public int IdTipoCapacitacionMoodle { get; set; }
        public int IdMoodle { get; set; }
        public int Duracion { get; set; }
        public int Meses { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
