﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioLaboralVentasDTO
    {
        public int IdAgendaTipoUsuario { get; set; }
        public string TipoPersona { get; set; }
        public decimal Sueldo { get; set; }
        public decimal Comisiones { get; set; }
        public decimal SistemaPensionario { get; set; }
        public decimal RentaQuintaCategoria { get; set; }
        public decimal EsSalud { get; set; }
        public decimal CTS { get; set; }
        public decimal Gratificacion { get; set; }
        public decimal ParticipacionesUtilidades { get; set; }
        public decimal Publicidad { get; set; }
    }
}
