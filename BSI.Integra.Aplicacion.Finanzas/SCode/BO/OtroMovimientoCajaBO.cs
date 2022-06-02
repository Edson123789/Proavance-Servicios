using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class OtroMovimientoCajaBO : BaseBO
    {
        public int Id {get;set;}
        public int IdSubTipoMovimientoCaja {get;set;}
        public int? IdAlumno {get;set;}
        public decimal Precio {get;set;}
        public int IdMoneda {get;set;}
        public DateTime FechaPago {get;set;}
        public int? IdCentroCosto {get;set;}
        public int? IdPlanContable {get;set;}
        public int IdCuentaCorriente {get;set;}
        public int? IdFormaPago {get;set;}
        public string Observaciones {get;set;}
    }
}
