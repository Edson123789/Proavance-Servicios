using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/AsesorTipoCategoriaOrigenDetalle
    /// Autor: Gian Miranda
    /// Fecha: 21/01/2021
    /// <summary>
    /// BO para la interaccion con mkt.T_AsesorTipoCategoriaOrigenDetalle, tabla hija de mkt.T_AsesorCentroCosto
    /// </summary>
    public class AsesorTipoCategoriaOrigenDetalleBO : BaseBO
    {
        /// Propiedades	            Significado
        /// -----------	            ------------
        /// IdAsesorCentroCosto		FK de la tabla mkt.T_AsesorCentroCosto
        /// IdTipoCategoriaOrigen   FK de la tabla mkt.T_TipoCategoriaOrigen
        /// Prioridad		        Prioridad del registro en la tabla mkt.T_AsesorCentroCosto
        /// IdMigracion		        Id de Migracion, nulleable y campo obligatorio
        public int IdAsesorCentroCosto { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Prioridad { get; set; }
        public Guid? IdMigracion { get; set; }
        public AsesorTipoCategoriaOrigenDetalleBO()
        {

        }
    }
}
