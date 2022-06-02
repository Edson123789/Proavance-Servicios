using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/NivelEstudio
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// BO para T_NivelEstudio
    /// </summary>
    public class NivelEstudioBO : BaseBO
    {
        /// Propiedades		    Significado
        /// -----------		    ------------
        /// Nombre			    Nombre de Nivel de Estudio
        /// IdTipoFormacion     FK de T_TipoFormacion
        /// IdMigracion         Id de Migración
        public string Nombre { get; set; }
		public int IdTipoFormacion { get; set; }
		public int? IdMigracion { get; set; }
	}
}
