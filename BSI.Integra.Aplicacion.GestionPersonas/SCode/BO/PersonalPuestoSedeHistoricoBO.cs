using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    ///BO: ModuloSistemaPuestoTrabajo
    ///Autor: Edgar S.
    ///Fecha: 19/01/2021
    ///<summary>
    ///Información de columnas de la tabla T_ModuloSistemaPuestoTrabajo
    ///</summary>
    public class PersonalPuestoSedeHistoricoBO : BaseBO
    {
        ///Propiedades          Significado
        ///-------------        -----------------------
        /// IdPersonal           Fk de T_PuestoTrabajo
        /// IdPuestoTrabajo      Fk de T_ModuloSistema
        /// IdSedeTrabajo        Fk de T_SedeTrabajo
        /// Actual               Determina el estado del puesto y sede a la actualidad
        public int IdPersonal { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public bool Actual { get; set; }        
    }
}
