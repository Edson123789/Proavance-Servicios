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
    public class GastoFinancieroCronogramaBO : BaseBO
    {
        public int IdEntidadFinanciera { get; set; }
        public string Nombre { get; set; }
        public int IdMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
        public int? IdMigracion { get; set; }
        public List<GastoFinancieroCronogramaDetalleBO> GastoFinancieroCronogramaDetalle { get; set; }
    }

}
