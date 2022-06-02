using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: MandrilEnvioCorreoBO
    /// Autor: _ _ _ _ _ _ .
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_MandrilEnvioCorreo
    /// </summary>
    public class MandrilEnvioCorreoBO : BaseBO
    {
        /// Propiedades		                    Significado
        /// -------------	                    -----------------------
        /// IdOportunidad                       Id de Oportunidad                       
        /// IdCentroCosto                       Id de Centro de Costo
        /// IdPersonal                          Id de Personal
        /// IdAlumno                            Id de Alumno
        /// IdMandrilTipoAsignacion             Id de Tipo de Asignación Mandril
        /// IdMandrilTipoEnvio                  Id de Tipo de Envío de Mandril
        /// Asunto                              Asunto
        /// FechaEnvio                          Fecha de Envío
        /// FkMandril                           Id de Mandril
        /// EsEnvioMasivo                       Validación de envío masivo
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdMandrilTipoAsignacion { get; set; }
        public int? EstadoEnvio { get; set; }
        public int IdMandrilTipoEnvio { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string FkMandril { get; set; }
        public bool EsEnvioMasivo { get; set; }

        public MandrilEnvioCorreoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
