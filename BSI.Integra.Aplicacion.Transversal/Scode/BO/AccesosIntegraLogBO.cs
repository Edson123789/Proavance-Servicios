///BO: AccesosIntegraLogBO
///Autor: Edgar S.
///Fecha: 27/01/2021
///<summary>
///Columnas de la tabla T_AccesosIntegraLog
///</summary>
using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AccesosIntegraLogBO : BaseBO
    {
        ///Propiedades		  Significado
        ///-------------	  -----------------------
        ///Usuario             Nombre de Usuario
        ///IpUsuario           Ip de Usuario
        ///Cookie              Cookie de Usuario
        ///Habilitado          Estado Habilitación de registro
        public string Usuario { get; set; }
        public string IpUsuario { get; set; }
        public string Cookie { get; set; }
        public bool Habilitado { get; set; }
    }
}
