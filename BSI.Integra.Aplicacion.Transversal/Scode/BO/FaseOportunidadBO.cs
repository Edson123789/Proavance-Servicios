using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: FaseOportunidadBO
    ///Autor: _ _ _ _ _ _ _ _
    ///Fecha: 26/03/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_FaseOportunidad
    ///</summary>
    public class FaseOportunidadBO : BaseBO
    {
        ///Propiedades		            Significado
        ///-------------	            -----------------------
        /// Codigo                      Id de Personal Asignado
        /// Nombre                      Id de Tipo de dato
        /// NroMinutos                  Id de Fase de Oportunidad
        /// IdActividad                 Id Origen
        /// MaxNumDias                  Id de alumno
        /// MinNumDias                  Mínimo número de días
        /// TasaConversionEsperada      Tasa de Conversión Esperada
        /// Meta                        Confirmación Meta
        /// Final                       Validación Final
        /// ReporteMeta                 Reporte de Meta
        /// EnSeguimiento               Validación de Fase de Seguimiento
        /// EsCierre                    Validación de Fase de Cierre
        /// IdMigracion                 Id de Migración
        /// Descripcion                 Descripción
        /// VisibleEnReporte            Validación de visibilidad en reportes
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; }
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        public bool? EsCierre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleEnReporte { get; set; }
        public FaseOportunidadBO() {
        }
    }
    
}
