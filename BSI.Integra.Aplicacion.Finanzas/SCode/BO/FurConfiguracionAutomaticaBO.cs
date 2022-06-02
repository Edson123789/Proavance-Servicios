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
    public class FurConfiguracionAutomaticaBO : BaseBO
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

        public int? IdMigracion { get; set; }
    }
}
