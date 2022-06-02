using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///	BO: CentroEstudio
	///	Autor: Luis Huallpa - Edgar Serruto
	///	Fecha: 05/08/2021
	///	<summary>
	///	Columnas y funciones de la tabla T_CentroEstudio
	///	</summary>
	public class CentroEstudioBO : BaseBO
	{
		///	Propiedades				Significado
		///	-------------			-----------------------
		/// Nombre					Nombre de Centro de Estudios
		/// IdPais					FK de T_Pais
		/// IdCiudad				FK de T_Ciudad
		/// IdTipoCentroEstudio		FK de T_CentroEstudio
		/// IdMigracion				Id de Migración
		public string Nombre { get; set; }
		public int? IdPais { get; set; }
		public int IdCiudad { get; set; }
		public int IdTipoCentroEstudio { get; set; }
		public int? IdMigracion { get; set; }
	}
}
