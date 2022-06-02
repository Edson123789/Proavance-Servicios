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
    /// BO: Finanzas/Retencion
    /// Autor: Miguel Mora
    /// Fecha: 11/08/2021
    /// <summary>
    /// BO para la logica de las retenciones
    /// </summary>

    public class RetencionBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// Nombre		                    Nombre de la Retención
        /// Descripcion		                Descripción de la retención 
        /// Valor		                    Valor de la retencion
        /// IDPais                          Id del país de la retención

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }
        public int? IdPais { get; set; }

        private RetencionRepositorio _repCargo;

        public RetencionBO()
        {
            _repCargo = new RetencionRepositorio();
        }

    
    }

}
