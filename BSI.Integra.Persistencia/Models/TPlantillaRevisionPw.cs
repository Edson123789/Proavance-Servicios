﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantillaRevisionPw
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int IdPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; }
        public virtual TRevisionNivelPw IdRevisionNivelPwNavigation { get; set; }
    }
}
