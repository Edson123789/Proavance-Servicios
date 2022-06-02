using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalAreaTrabajo
    /// Autor: Joao Benavente
    /// Fecha: 06/07/2021
    /// <summary>
    /// BO para la logica de T_PersonalAreaTrabajo
    /// </summary>
    public class PersonalAreaTrabajoBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// Codigo                  Código de Área de Trabajo
		/// Nombre                  Nombre de Área de Trabajo
        /// Descripción             Descripción de Área de Trabajo
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
