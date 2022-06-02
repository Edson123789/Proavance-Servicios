using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/DatoFamiliarPersonal
	/// Autor: Luis Huallpa . 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_DatoFamiliarPersonal
	/// </summary>
	public class DatoFamiliarPersonalBO : BaseBO
	{
		/// Propiedades					Significado
		/// -----------					------------
		/// IdPersonal					Id de Personal
		/// IdSexo						Id de Sexo
		/// IdParentescoPersonal		Id de Parentesco de Personal
		/// IdTipoDocumentoPersonal		Id de Tipo de Documento Personal
		/// Nombres						Nombre de Familiar
		/// Apellidos					Apellidos de Familiar
		/// FechaNacimiento				Fecha de Nacimiento de Familiar
		/// NumeroDocumento				Número de Documento de Familiar
		/// NumeroReferencia1			Primer número de referencia
		/// NumeroReferencia2			Segungo número de referencia
		/// DerechoHabiente				Validación de Derecho Habiente
		/// EsContactoInmediato			Validación de contacto inmediato
		/// IdMigracion					Id de Migración
		public int IdPersonal { get; set; }
		public int IdSexo { get; set; }
		public int IdParentescoPersonal { get; set; }
		public int IdTipoDocumentoPersonal { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string NumeroDocumento { get; set; }
		public string NumeroReferencia1 { get; set; }
		public string NumeroReferencia2 { get; set; }
		public bool DerechoHabiente { get; set; }
		public bool EsContactoInmediato { get; set; }
		public int? IdMigracion { get; set; }
	}
}
