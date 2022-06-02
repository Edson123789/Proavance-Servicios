using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	/// Repositorio: CentralLlamadaRepositorio
	/// Autor: Jashin Salazart
	/// Fecha: 21/07/2021
	/// <summary>
	/// Repositorio para validar las descargas de llamada en CentralLlamada
	/// </summary>
	public class CentralLlamadaRepositorio: BaseRepository<TActividadBase, ActividadBaseBO>
    {
		/// Autor: Jashin Salazar
		/// Fecha: 21/07/2021
		/// Version: 1.0
		/// <summary>
		/// Verifica que las descarga de llamadas se ejecuten correctamente
		/// </summary>
		/// <returns>CentralLlamadaDTO</returns>
		public CentralLlamadaDTO ValidarCentralLlamada()
		{
			try
			{
				var query = _dapper.QuerySPFirstOrDefault("com.SP_ValidarCentralLlamada", null);
				var rpta = JsonConvert.DeserializeObject<CentralLlamadaDTO>(query);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
