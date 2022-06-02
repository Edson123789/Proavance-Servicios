using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalIdioma
    /// Autor: Luis Huallpa . 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalIdioma
    /// </summary>
    public class PersonalIdiomaBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
        /// IdIdioma                Id de Idioma
        /// IdNivelIdioma           Id de Nivel de Idioma
        /// IdCentroEstudio         Id de Centro de Estudio
        /// IdMigracion             Id de Migración
        /// IdPersonalArchivo       FK de T_PersonalArchivo
        public int IdPersonal { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public int IdCentroEstudio { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
