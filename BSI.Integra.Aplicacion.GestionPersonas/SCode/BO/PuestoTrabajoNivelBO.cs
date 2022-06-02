using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: GestionPersonas/PuestoTrabajoNivel
    /// Autor: Edgar Serruto . 
    /// Fecha: 15/06/2021
    /// <summary>
    /// BO para la logica de T_PuestoTrabajoNivel
    /// </summary>
    public class PuestoTrabajoNivelBO : BaseBO
    {
        /// Propiedades                 Significado
        /// -----------	                ------------
        /// Nombre                      Nombre de Nivel de Puesto Trabajo
        /// Descripción                 Descripción de Nivel de Puesto Trabajo
        /// NivelVisualizacionAgenda    Nivel según Visualización de Agenda
        /// IdMigracion                 Id de Migración
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NivelVisualizacionAgenda { get; set; }
        public int? IdMigracion { get; set; }
    }
}
