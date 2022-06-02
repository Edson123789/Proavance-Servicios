using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalComputo
    /// Autor: Luis Huallpa . 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalComputo
    /// </summary>
    public class PersonalComputoBO : BaseBO
    {
        /// Propiedades                     Significado
		/// -----------	                    ------------
		/// IdPersonal                      Id de Personal
        /// Programa                        Programa Cómputo
        /// IdNivelCompetenciaTecnica       Id de Nivel de Competencia Técnica
        /// IdCentroEstudio                 Id de Centro de Estudio
        /// IdMigracion                     Id de Migración
        /// IdPersonalArchivo               FK de T_PersonalArchivo
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public int IdNivelCompetenciaTecnica { get; set; }
        public int IdCentroEstudio { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
