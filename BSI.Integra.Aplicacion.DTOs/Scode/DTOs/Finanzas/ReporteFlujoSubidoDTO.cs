﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteFlujoSubidoDTO
    {
        public string CodigoPrograma { get; set; }
        public string EstadoPrograma { get; set; }
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public Nullable<DateTime> FechaVencimientoOriginal { get; set; }
        public double? MontoCuotaOriginal { get; set; }
        public double MontoModificado { get; set; }
        public DateTime FechaVencimientoActual { get; set; }
        public double MontoCuotaActual { get; set; }
        public Nullable<DateTime> FechaPago { get; set; }
        public double MontoPagado { get; set; }
        public double SaldoPendiente { get; set; }
        public double Mora { get; set; }
        public string NroCuota { get; set; }
        public string Moneda { get; set; }
        public double TotalCuotaDolar { get; set; }
        public double RealPagoDolar { get; set; }
        public double SaldoPendienteDolar { get; set; }
        public string OrigenPrograma { get; set; }
        public string CCodigo { get; set; }
        public string CodigoMatricula { get; set; }
        public string Email { get; set; }
        public string TelFijo { get; set; }
        public string TelCel { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public string Nuevo { get; set; }
        public string Factura { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
