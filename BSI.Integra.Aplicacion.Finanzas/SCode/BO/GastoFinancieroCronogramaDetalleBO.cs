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
    public class GastoFinancieroCronogramaDetalleBO : BaseBO
    {
        public int IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
        public int? IdMigracion { get; set; }

    }

}
