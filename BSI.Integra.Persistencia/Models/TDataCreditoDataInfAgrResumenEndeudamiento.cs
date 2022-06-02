﻿using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrResumenEndeudamiento
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? TrimestreFecha { get; set; }
        public string SectorSector { get; set; }
        public string SectorCodigoSector { get; set; }
        public string SectorGarantiaAdmisible { get; set; }
        public string SectorGarantiaOtro { get; set; }
        public string CarteraTipo { get; set; }
        public int? CarteraNumeroCuentas { get; set; }
        public decimal? CarteraValor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
