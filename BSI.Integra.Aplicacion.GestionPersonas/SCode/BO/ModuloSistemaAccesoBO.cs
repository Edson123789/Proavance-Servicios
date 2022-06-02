using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    ///BO: ModuloSistemaAccesoBO
    ///Autor: Edgar S.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas de la tabla T_ModuloSistemaAcceso
    ///</summary>
    public class ModuloSistemaAccesoBO: BaseBO
    {
        ///Propiedades		        Significado
        ///-------------	        -----------------------
        ///Id                        PK de T_ModuloSistemaAcceso
        ///IdUsuarioRol              FK de T_UsuarioRol 
        ///IdUsuario                 FK de T_Usuario
        ///IdModuloSistema           FK de T_ModuloSistema 
        ///Estado                    Estado de registro
        public int Id { get; set; }
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdModuloSistema { get; set; }
        public bool Estado { get; set; }
    }
}
