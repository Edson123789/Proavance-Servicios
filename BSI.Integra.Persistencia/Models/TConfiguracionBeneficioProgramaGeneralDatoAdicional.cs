﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionBeneficioProgramaGeneralDatoAdicional
    {
        public int Id { get; set; }
        public int IdConfiguracionBeneficioPgeneral { get; set; }
        public int IdBeneficioDatoAdicional { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TBeneficioDatoAdicional IdBeneficioDatoAdicionalNavigation { get; set; }
        public virtual TConfiguracionBeneficioProgramaGeneral IdConfiguracionBeneficioPgeneralNavigation { get; set; }
    }
}
