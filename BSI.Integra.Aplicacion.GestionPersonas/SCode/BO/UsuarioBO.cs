using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    ///BO: UsuarioBO
    ///Autor: Edgar S.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas de la tabla T_Usuario
    ///</summary>
    public  class UsuarioBO :BaseBO
    {
        ///Propiedades		   Significado
		///-------------	   -----------------------
		///Id                  PK de T_Usuario
		///IdPersonal		   FK de T_Personal
        ///NombreUsuario       Nombre de Usuario
        ///Clave               Clave de Usuario
        ///IdUsuarioRol        FK de T_UsuarioRol
        ///CodigoAreaTrabajo   Codigo de Area de Trabajo
        ///Estado              Estado de Registro
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int IdUsuarioRol { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public bool Estado { get; set; }


    }
}
