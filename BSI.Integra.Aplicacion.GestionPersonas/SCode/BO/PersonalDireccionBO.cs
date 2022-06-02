using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalDireccion
	/// Autor: Luis Huallpa . 
	/// Fecha: 16/06/2021
	/// <summary>
	/// BO para la logica de T_PersonalDireccion
	/// </summary>
	public class PersonalDireccionBO : BaseBO
	{
		/// Propiedades             Significado
		/// -----------	            ------------
		/// IdPersonal              Id de Personal
		/// IdPais					Id de Pais
		/// IdCiudad				Id de Ciudad
		/// Distrito				Distrito de Personal
		/// TipoVia					Tipo de Vía de Dirección de Personal
		/// NombreVia				Nombre de vía de Dirección de Personal
		/// Manzana					Manzana de Dirección Personal
		/// Lote					Lote de Dirección de Personal
		/// TipoZonaUrbana			Tipo de Zona Urbana de Personal
		/// NombreZonaUrbana		Nombre de Zona Urbana de Personal
		/// Activo					Validación de Personal Activo
		/// IdMigracion             Id de Migración
		public int IdPersonal { get; set; }
		public int? IdPais { get; set; }
		public int? IdCiudad { get; set; }
		public string Distrito { get; set; }
		public string TipoVia { get; set; }
		public string NombreVia { get; set; }
		public string Manzana { get; set; }
		public int? Lote { get; set; }
		public string TipoZonaUrbana { get; set; }
		public string NombreZonaUrbana { get; set; }
		public bool Activo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
