using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TarifarioDetalleAlternoBO : BaseBO
    {        
        public int IdTarifario { get; set; }
        public string Concepto { get; set; }
        public int? IdPais { get; set; }
        public decimal? Monto { get; set; }
        public bool? AplicaCuota { get; set; }
        public string Descripcion { get; set; }
        public string TipoCantidad { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdMoneda { get; set; }
        public bool? VisualizarPortalWeb { get; set; }
    }
}
