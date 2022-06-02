using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalFormacion
    /// Autor: Luis Huallpa . 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalFormacion
    /// </summary>
    public class PersonalFormacionBO : BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
        /// IdCentroEstudio         Id de Centro de Estudio
        /// IdTipoEstudio           Id de Tipo de Estudio
        /// IdAreaFormacion         Id de Área de Formación
        /// FechaInicio             Fecha de Inicio
        /// FechaFin                Fecha de Fin
        /// AlaActualidad           Validación de Formación llevada a la actualidad
        /// IdEstadoEstudio         Id de Estado de Estudio
        /// Logro                   Logro Obtenido
        /// IdMigracion             Id de Migración
        /// IdPersonalArchivo       FK de T_PersonalArchivo
        public int IdPersonal { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? AlaActualidad { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public string Logro { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
