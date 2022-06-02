using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: AsignacionOportunidadLogBO
    ///Autor: Edgar S.
    ///Fecha: 08/02/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_AsignacionOportunidadLog
    ///</summary>
    public class AsignacionOportunidadLogBO : BaseBO
    {
        ///Propiedades		            Significado
        ///-------------	            -----------------------
        ///IdAsignacionOportunidad      Id de Asignación de Oportunidad
        ///IdOportunidad                Id de Oportunidad
        ///IdPersonalAnterior           Id de Personal Anterior
        ///IdPersonal                   Id de Personal
        ///IdCentroCostoAnt             Id de Centro de Costo Anterior
        ///IdCentroCosto                Id de Centro de Costo
        ///IdAlumno                     Id de Alumno
        ///FechaLog                     Fecha Log
        ///IdTipoDato                   Id de Tipo de Dato
        ///IdFaseOportunidad            Id de Fase de Oportunidad
        ///IdMigracion                  Id de migración
        ///IdClasificacionPersona       Id de Clasificación de Persona
        public int? IdAsignacionOportunidad { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonalAnterior { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaLog { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
    }
}
