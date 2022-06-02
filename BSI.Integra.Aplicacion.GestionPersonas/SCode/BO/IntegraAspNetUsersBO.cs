using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    ///BO: IntegraAspNetUsersBO
    ///Autor: Edgar S.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas tabla T_IntegraAspNetUsers
    ///</summary>
    public class IntegraAspNetUsersBO : BaseBO

    {
        ///Propiedades		        Significado
        ///-------------	        -----------------------
        ///Id                       PK de T_IntegraAspNetUsers
        ///UsClave                  Clave de Integra
        ///PerId                    FK de T_Personal
        ///RolId                    Fk de T_UsuarioRol
        ///AreaTrabajo              Area de Trabajo
        ///Email                    Email de Usuario
        ///EmailConfirmed           Estado de confirmación de Email
        ///PasswordHash             Clave de Integra Encriptada
        ///SecurityStamp            Sello de Seguridad
        ///PhoneNumber              Número de celular
        ///PhoneNumberConfirmed     Confirmación de celular
        ///TwoFactorEnabled         Factores Disponibles
        ///LockoutEndDateUtc        Fecha de Finalización de bloqueo UTC
        ///LockoutEnabled           Fecha de disponibilidad
        ///AccessFailedCount        Contador de Fallso de Acceso
        ///UserName                 Nombre de Usuario
        public string Id { get; set; }
        public string UsClave { get; set; }
        public int PerId { get; set; }
        public int RolId { get; set; }
        public string AreaTrabajo { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public void Select(Func<object, IntegraAspNetUsersBO> p)
        {
            throw new NotImplementedException();
        }
    }
}
