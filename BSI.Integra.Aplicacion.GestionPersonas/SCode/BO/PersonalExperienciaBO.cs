using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PersonalExperiencia
    /// Autor: Luis Huallpa . 
    /// Fecha: 16/06/2021
    /// <summary>
    /// BO para la logica de T_PersonalExperiencia
    /// </summary>
    public class PersonalExperienciaBO : BaseBO
    {
        /// Propiedades                 Significado
		/// -----------	                ------------
		/// IdPersonal                  Id de Personal
        /// IdEmpresa                   Id de Empresa
        /// IdIndustria                 Id de Industria
        /// IdAreaTrabajo               Id de Área de Trabajo
        /// IdCargo                     Id de Cargo
        /// FechaIngreso                Fecha de Ingreso
        /// FechaRetiro                 Fecha de Retiro
        /// MotivoRetiro                Motivo de Retiro
        /// NombreJefeInmediato         Nombre de Jefe Inmediato
        /// TelefonoJefeInmediato       Teléfono de Jefe Inmediato
        /// IdMigracion                 Id de Migración
        /// IdPersonalArchivo           FK de T_PersonalArchivo
        public int IdPersonal { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string MotivoRetiro { get; set; }
        public string NombreJefeInmediato { get; set; }
        public string TelefonoJefeInmediato { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
