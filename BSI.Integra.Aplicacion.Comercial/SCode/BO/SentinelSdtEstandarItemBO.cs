﻿using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSdtEstandarItemBO : BaseBO
    {
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string Semaforos { get; set; }
        public string Score { get; set; }
        public string NroBancos { get; set; }
        public string DeudaTotal { get; set; }
        public string VencidoBanco { get; set; }
        public string Calificativo { get; set; }
        public string Veces24m { get; set; }
        public string SemanaActual { get; set; }
        public string SemanaPrevio { get; set; }
        public string SemanaPeorMejor { get; set; }
        public string Documento2 { get; set; }
        public string EstadoDomicilio { get; set; }
        public string CondicionDomicilio { get; set; }
        public string DeudaTributaria { get; set; }
        public string DeudaLaboral { get; set; }
        public string DeudaImpagable { get; set; }
        public string DeudaProtestos { get; set; }
        public string DeudaSbs { get; set; }
        public string CuentasTarjetas { get; set; }
        public string ReporteNegativo { get; set; }
        public string TipoActividad { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public string DeudaDirecta { get; set; }
        public string DeudaIndirecta { get; set; }
        public string DeudaCastigada { get; set; }
        public string LineaCreditoNoUtilizada { get; set; }
        public string TotalRiesgo { get; set; }
        public string PeorCalificacion { get; set; }
        public string PorcentajeCalificacionNormal { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
