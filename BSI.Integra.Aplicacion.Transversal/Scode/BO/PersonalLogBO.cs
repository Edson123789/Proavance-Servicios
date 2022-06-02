using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: PersonalLogBO
    ///Autor: Esthephany Tanco - Britsel Calluchi - Edgar Serruto.
    ///Fecha: 27/01/2021
    ///<summary>
    ///Columnas de la tabla T_PersonalLog
    ///</summary>
    public class PersonalLogBO : BaseBO
    {
        ///Propiedades		        Significado
		///-------------	        -----------------------
		///IdPersonal               Fk de T_Personal
		///Rol		                Rol de Personal
        ///TipoPersonal             Tipo de Personal
        ///IdJefe                   Fk de T_Personal Jefe
        ///EstadoRol                Estado de Rol de Personal
        ///EstadoTipoPersonal       Estado de Tipo de Personal
        ///EstadoIdJefe             Estado de Id Jefe
        ///FechaInicio              Fecha de Inicio
        ///FechaFin                 Fecha Final
        ///IdMigracion              Id de Migración
        ///IdCerrador               Id de Cerrador
        ///EsCerrador               Confirmación de cerrador
        ///EstadoCerrador           Confirmación de modificación
        ///IdPuestoTrabajoNivel     FK de T_PuestoTrabajoNivel
        public int IdPersonal { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public int? IdJefe { get; set; }
        public bool EstadoRol { get; set; }
        public bool EstadoTipoPersonal { get; set; }
        public bool EstadoIdJefe { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public bool? EstadoCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
    }

}
