using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalHistorialMedico
	/// Autor: Luis Huallpa . 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_PersonalHistorialMedico
	/// </summary>
	public class PersonalHistorialMedicoBO : BaseBO
    {
		/// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
		/// Enfermedad				Nombre de Enfermedad de Personal
		/// DetalleEnfermedad		Detalle de Enfermedad de Personal
		/// Periodo					Periodo
		/// IdMigracion				Id de Migración
		public int IdPersonal { get; set; }
		public string Enfermedad { get; set; }
		public string DetalleEnfermedad { get; set; }
		public string Periodo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
