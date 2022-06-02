using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalRemuneracion
	/// Autor: Luis Huallpa . 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_PersonalRemuneracion
	/// </summary>
	public class PersonalRemuneracionBO : BaseBO
	{
		/// Propiedades						Significado
		/// -----------						------------
		/// IdPersonal						Id de Personal
		/// IdTipoPagoRemuneracion			Id de Tipo de Pago de Remuneración
		/// IdEntidadFinanciera				Id de Entidad Financiera
		/// NumeroCuenta					Número de Cuenta de Personal
		/// Activo							Validación de Remuneración Activa
		/// IdMigracion						Id de Migración
		public int IdPersonal { get; set; }
		public int IdTipoPagoRemuneracion { get; set; }
		public int? IdEntidadFinanciera { get; set; }
		public string NumeroCuenta { get; set; }
		public bool Activo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
