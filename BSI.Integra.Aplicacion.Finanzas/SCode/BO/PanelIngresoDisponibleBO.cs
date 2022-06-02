﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class PanelIngresoDisponibleBO : BaseBO
    {
        public int? IdFormaPago { get; set; }
        public int DiasDeposito { get; set; }
        public int DiasDisponible { get; set; }
        public bool CuentaFeriados { get; set; }
        public bool ConsideraVsd { get; set; }
        public bool ConsideraDiasHabilesLunesSabado { get; set; }
        public bool ConsideraDiasHabilesLunesViernes { get; set; }
        public bool ConsideraDiasFijoSemana { get; set; }
        public int HoraCorte { get; set; }
        public int MinutoCorte { get; set; }
        public decimal PorcentajeCobro { get; set; }
        public bool CuentaFeriadosEstatales { get; set; }        
        public Guid? IdMigracion { get; set; }
        public int? IdDiaSemanaFijo { get; set; }
    }
}
