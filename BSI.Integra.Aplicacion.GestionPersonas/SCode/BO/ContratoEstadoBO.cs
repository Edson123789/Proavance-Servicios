using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: GestionPersonas/ContratoEstado
    /// Autor: Luis Huallpa - Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// BO para T_ContratoEstado
    /// </summary>
    public class ContratoEstadoBO : BaseBO
    {
        /// Propiedades	       Significado
		/// -----------	       ------------
		/// Nombre             Nombre de Estado de Contrato
		/// IdMigracion        Id de Migración
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
