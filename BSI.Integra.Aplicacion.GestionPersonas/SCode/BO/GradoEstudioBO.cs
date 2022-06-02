using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/GradoEstudio
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// BO para T_GradoEstudio
    /// </summary>
    public class GradoEstudioBO : BaseBO
    {
        /// Propiedades		    Significado
        /// -----------		    ------------
        /// Nombre			    Nombre de Grado de Formación
        /// IdMigracion         Id de Migración
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
