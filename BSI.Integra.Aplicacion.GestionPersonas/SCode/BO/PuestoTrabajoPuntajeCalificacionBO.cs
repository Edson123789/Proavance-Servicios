using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoPuntajeCalificacionBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoPuntajeCalificacion
	///</summary>
	public class PuestoTrabajoPuntajeCalificacionBO : BaseBO
	{
		///Propiedades                  Significado
		///-------------                -----------------------
		///IdPerfilPuestoTrabajo        FK de T_PerfilPuestoTrabajo 
		///IdExamenTest                 FK de T_ExamenTest
		///IdGrupoComponenteEvaluacion  FK de T_GrupoComponenteEvaluacion
		///IdExamen						FK de T_Examen
		///CalificaPorCentil			Estado de calificación por Centil
		///PuntajeMinimo				Puntaje mínimo
		///IdProcesoSeleccionRango		FK de T_PersonalAreaTrabajo
		///IdMigracion					Id de Migración
		///EsCalificable				Estado calificable
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? IdExamenTest { get; set; }
		public int? IdGrupoComponenteEvaluacion { get; set; }
		public int? IdExamen { get; set; }
		public bool CalificaPorCentil { get; set; }
		public decimal? PuntajeMinimo { get; set; }
		public int? IdProcesoSeleccionRango { get; set; }
		public int? IdMigracion { get; set; }
		public bool EsCalificable { get; set; }
	}
}
