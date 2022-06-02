using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalInformacionMedica
	/// Autor: Luis Huallpa - Britsel Calluchi. 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_PersonalInformacionMedica
	/// </summary>
	public class PersonalInformacionMedicaBO : BaseBO
    {
		/// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
		/// IdTipoSangre			Id de Tipo de Sangre
		/// Alergia					Alergia
		/// Precaucion				Precauciones
		/// IdMigracion				Id de Migración
		public int IdPersonal { get; set; }
		public int? IdTipoSangre { get; set; }
		public string Alergia { get; set; }
		public string Precaucion { get; set; }
		public int? IdMigracion { get; set; }
	}
}
