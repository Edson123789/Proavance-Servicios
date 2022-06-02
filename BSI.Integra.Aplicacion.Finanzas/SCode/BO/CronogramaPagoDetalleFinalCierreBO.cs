using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CronogramaPagoDetalleFinalCierreBO : BaseBO
    {
        public int IdPeriodoCorte { get; set; }
        public string PeriodoNombre { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoPagado { get; set; }
        public int? IdMigracion { get; set; }

    }

}
