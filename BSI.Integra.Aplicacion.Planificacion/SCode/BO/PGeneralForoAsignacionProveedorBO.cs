using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.BO
{
    /// BO: Planificacion/PGeneralForoAsignacionProveedor
    /// Autor: Edgar Serruto
    /// Fecha: 24/06/2021
    /// <summary>
    /// BO para la logica de T_PGeneralForoAsignacionProveedor
    /// </summary>
    public class PGeneralForoAsignacionProveedorBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// IdPgeneral              Id de Programa General
		/// IdModalidadCurso    	Id de Modalidad de Curso
		/// IdProveedor				Id de Proveedor
        /// IdMigracion             Id de Migración
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
