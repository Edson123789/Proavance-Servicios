using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalCese
    /// Autor: Joao Benavente
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalCese
    /// </summary>
    public class PersonalCeseBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
		/// IdMotivoCese            Id de Motivo de Cese
        /// FechaCese               Fecha de Cese
        /// IdMigracion             Id de Migración
        public int IdPersonal { get; set; }
        public int? IdMotivoCese { get; set; }
        public DateTime FechaCese { get; set; }
        public int? IdMigracion { get; set; }

    }
}
