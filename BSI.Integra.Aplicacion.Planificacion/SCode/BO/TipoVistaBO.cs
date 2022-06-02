using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/TipoVista
    /// Autor: Gian Miranda
    /// Fecha: 01/03/2021
    /// <summary>
    /// BO para la logica de tipo de vista
    /// </summary>
    public class TipoVistaBO : BaseBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// Nombre                                      Cadena con el nombre del tipo de vista
        public string Nombre { get; set; }
    }
}
