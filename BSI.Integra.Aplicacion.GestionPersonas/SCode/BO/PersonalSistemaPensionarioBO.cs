using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalSistemaPensionario
    /// Autor: Luis Huallpa - Britsel Calluchi. 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalSistemaPensionario
    /// </summary>
    public class PersonalSistemaPensionarioBO : BaseBO
    {
        /// Propiedades                     Significado
		/// -----------	                    ------------
        /// IdPersonal                      Id de Personal
        /// IdSistemaPensionario            Id de Sistema Pensionario
        /// IdEntidadSistemaPensionario     Id de Entidad de Sistema Pensionario
        /// CodigoAfiliado                  Código de Afiliación de Personal
        /// Activo                          Validación de Activación de Sistema Pensionario
        /// IdMigracion                     Id de Migración
        public int IdPersonal { get; set; }
        public int IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string CodigoAfiliado { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
