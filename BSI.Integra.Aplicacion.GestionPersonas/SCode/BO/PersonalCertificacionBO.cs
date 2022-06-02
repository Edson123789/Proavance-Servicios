using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalCertificacion
	/// Autor: Luis Huallpa - Edgar Serruto. 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_PersonalCertificacion
	/// </summary>
	public class PersonalCertificacionBO : BaseBO
    {
		/// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
		/// Programa				Programa de Certificación
		/// Institucion				Institución
		/// FechaCertificacion		Fecha de Certificación
		/// IdMigracion				Id de Migración
		/// IdPersonalArchivo       FK de T_PersonalArchivo
		/// IdCentroEstudio			FK de T_CentroEstudio
		public int IdPersonal { get; set; }
		public string Programa { get; set; }
		public string Institucion { get; set; }
		public DateTime FechaCertificacion { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdPersonalArchivo { get; set; }
		public int? IdCentroEstudio { get; set; }
	}
}
