using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: MotivoActividadBO
    /// Autor: Edgar S.
    /// Fecha: 18/03/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_MotivoInactividad
    /// </summary>
    public class MotivoInactividadBO : BaseBO
    {
        /// Propiedades		   Significado
        /// -------------	   ---------------
        /// Nombre             Nombre
        /// IdMigracion        Id de Migracion
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
