using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurConfiguracionAutomaticaDTO
    {
        public int Id { get; set; }
        public int IdFurTipoSolicitud { get; set; }
        public int IdSede { get; set; }
        public int IdEmpresa { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public decimal Cantidad { get; set; }
        public int IdMonedaPagoReal { get; set; }
        public int AjusteNumeroSemana { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdFrecuencia { get; set; }
        public int IdCentroCosto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaGeneracionFur { get; set; }
        public DateTime FechaInicioConfiguracion { get; set; }
        public DateTime FechaFinConfiguracion { get; set; }

        public string Usuario { get; set;  }
    }
}
