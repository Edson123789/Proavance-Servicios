using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/TConfiguracionBeneficioProgramaGeneralBO
    /// Autor: Edgar Serruto .
    /// Fecha: 21/07/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_ConfiguracionBeneficioProgramaGeneral
    /// </summary>
    public class TConfiguracionBeneficioProgramaGeneralBO : BaseBO
    {
        /// Propiedades		                    Significado
        /// -------------	                    -----------------------
        /// IdPGeneral                          Id de Programa General
        /// IdBeneficio                         Id de Beneficio
        /// Tipo                                Id de Tipo
        /// Asosiar                             Validación de asociación
        /// Entrega                             Entrega
        /// AvanceAcademico                     Indicador de Avance académico
        /// DeudaPendiente                      Indicador de Deuda
        /// OrdenBeneficio                      Orden de beneficio
        /// DatosAdicionales                    Validación de datos adicionales
        /// Requisitos                          Información de requisitos
        /// ProcesoSolicitud                    Información de proceso de solicitud
        /// DetallesAdicionales                 Información de detalles adicionales
        public int IdPGeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int Tipo { get; set; }
        public bool Asosiar { get; set; }
        public int Entrega { get; set; }
        public int? AvanceAcademico { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
        public string Requisitos { get; set; }
        public string ProcesoSolicitud { get; set; }
        public string DetallesAdicionales { get; set; }
    }
}
