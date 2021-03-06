using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoReporteBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoReporte
	///</summary>
	public class PuestoTrabajoReporteBO : BaseBO
    {
		///Propiedades					Significado
		///-------------				-----------------------
		///IdPerfilPuestoTrabajo		FK de T_PerfilPuestoTrabajo
		///NroOrden						Número de Orden
		///Nombre						Nombre
		///IdFrecuenciaPuestoTrabajo	FK de T_FrecuenciaPuestoTrabajo
		///IdMigracion					Id de Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public int NroOrden { get; set; }
		public string Nombre { get; set; }
		public int IdFrecuenciaPuestoTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
