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
    public class ComprobantePagoPorFurBO : BaseBO
    {
        public int IdComprobantePago { get; set; }
        public int IdFur { get; set; }
        public decimal Monto { get; set; }
        public int? IdMigracion { get; set; }
    }

}
