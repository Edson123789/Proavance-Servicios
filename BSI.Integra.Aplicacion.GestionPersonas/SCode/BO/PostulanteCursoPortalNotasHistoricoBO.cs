using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    /// BO: GestionPersonas/PostulanteCursoPortalNotasHistorico
    /// Autor: Edgar Serruto. 
    /// Fecha: 02/08/2021
    /// <summary>
    /// BO para la logica de T_PostulanteCursoPortalNotasHistorico
    /// </summary>
    public class PostulanteCursoPortalNotasHistoricoBO : BaseBO
    {
        /// Propiedades                         Significado
		/// -----------	                        ------------
		/// IdPostulanteProcesoSeleccion        FK de T_PostulanteProcesoSeleccion   
		/// IdPgeneral					        FK de T_PGeneral
        /// OrdenFilaCapitulo                   Orden de fila de capítulo
        /// OrdenFilaSesion                     Orden de fila de sesión
        /// GrupoPregunta                       Grupo de Pregunta
        /// Calificacion                        Calificación
        /// IdUsuario                           Id de Usuario
        /// IdAlumno                            Id de Alumno
        /// IdPespecifico                       Id de Pespecifico
        /// AccesoPrueba                        Validación de Acceso de Prueba
        /// IdMigracion                         Id de Migración
        public int IdPostulanteProcesoSeleccion { get; set; }
        public int IdPgeneral { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string GrupoPregunta { get; set; }
        public decimal? Calificacion { get; set; }
        public string IdUsuario { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPespecifico { get; set; }
        public bool? AccesoPrueba { get; set; }
        public int? IdMigracion { get; set; }
    }
}
