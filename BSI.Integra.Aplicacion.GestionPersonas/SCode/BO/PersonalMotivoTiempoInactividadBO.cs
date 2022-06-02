using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    ///BO: PersonalMotivoTiempoInactividadBO
    ///Autor: Edgar S.
    ///Fecha: 18/03/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_PersonalMotivoTiempoInactividad
    ///</summary>
    public class PersonalMotivoTiempoInactividadBO : BaseBO
    {
        ///Propiedades		                     Significado
        ///-------------	                     -----------------------
        ///IdPersonal                            Id de Personal
        ///IdMotivoInactividad                   Id de Motivo de Inactividad
        ///FechaInicio                           Fecha Inicio de Inactividad
        ///FechaFin                              Fecha Fin de Inactividad
        ///IdMigracion                           Id de Migracion
        public int IdPersonal { get; set; }
        public int IdMotivoInactividad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
    }
}
