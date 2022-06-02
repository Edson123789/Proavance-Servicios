using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalSeguroSalud
    /// Autor: Luis Huallpa . 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalSeguroSalud
    /// </summary>
    public class PersonalSeguroSaludBO : BaseBO
    {
        // Propiedades              Significado
        /// -----------	            ------------
        /// IdPersonal              Id de Personal
        /// IdEntidadSeguroSalud    Id de Entidad de Seguro de Salud
        /// IdMigracion             Id de Migración
        /// Activo                  Validación de Seguro de Salud Activo
        public int IdPersonal { get; set; }
        public int IdEntidadSeguroSalud { get; set; }
        public int? IdMigracion { get; set; }
		public bool Activo { get; set; }
    }
}
