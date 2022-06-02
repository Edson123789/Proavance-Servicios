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

    public class ModuloSistemaPuestoTrabajoBO : BaseBO
    {
        ///Propiedades          Significado
        ///-------------        -----------------------
        ///IdPuestoTrabajo      Fk de T_PuestoTrabajo
        ///IdModuloSistema      Fk de T_ModuloSistema
        public int IdPuestoTrabajo { get; set; }
        public int IdModuloSistema { get; set; }    
    }
}
