using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	/// Repositorio: Marketing/FacebookCuentaPublicitaria
	/// Autor: Gian Miranda
	/// Fecha: 12/06/2021
	/// <summary>
	/// Repositorio para consultas de mkt.T_FacebookCuentaPublicitaria
	/// </summary>
	public class FacebookCuentaPublicitariaRepositorio : BaseRepository<TFacebookCuentaPublicitaria, FacebookCuentaPublicitariaBO>
	{
		#region Metodos Base
		public FacebookCuentaPublicitariaRepositorio() : base()
		{
		}
		public FacebookCuentaPublicitariaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookCuentaPublicitariaBO> GetBy(Expression<Func<TFacebookCuentaPublicitaria, bool>> filter)
		{
			IEnumerable<TFacebookCuentaPublicitaria> listado = base.GetBy(filter);
			List<FacebookCuentaPublicitariaBO> listadoBO = new List<FacebookCuentaPublicitariaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookCuentaPublicitariaBO objetoBO = Mapper.Map<TFacebookCuentaPublicitaria, FacebookCuentaPublicitariaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookCuentaPublicitariaBO FirstById(int id)
		{
			try
			{
				TFacebookCuentaPublicitaria entidad = base.FirstById(id);
				FacebookCuentaPublicitariaBO objetoBO = new FacebookCuentaPublicitariaBO();
				Mapper.Map<TFacebookCuentaPublicitaria, FacebookCuentaPublicitariaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookCuentaPublicitariaBO FirstBy(Expression<Func<TFacebookCuentaPublicitaria, bool>> filter)
		{
			try
			{
				TFacebookCuentaPublicitaria entidad = base.FirstBy(filter);
				FacebookCuentaPublicitariaBO objetoBO = Mapper.Map<TFacebookCuentaPublicitaria, FacebookCuentaPublicitariaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookCuentaPublicitariaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookCuentaPublicitaria entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Insert(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(IEnumerable<FacebookCuentaPublicitariaBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Insert(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(FacebookCuentaPublicitariaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookCuentaPublicitaria entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Update(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(IEnumerable<FacebookCuentaPublicitariaBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Update(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		private void AsignacionId(TFacebookCuentaPublicitaria entidad, FacebookCuentaPublicitariaBO objetoBO)
		{
			try
			{
				if (entidad != null && objetoBO != null)
				{
					objetoBO.Id = entidad.Id;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		private TFacebookCuentaPublicitaria MapeoEntidad(FacebookCuentaPublicitariaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookCuentaPublicitaria entidad = new TFacebookCuentaPublicitaria();
				entidad = Mapper.Map<FacebookCuentaPublicitariaBO, TFacebookCuentaPublicitaria>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		/// <summary>
		/// Obtiene todos los registros para combos.
		/// </summary>
		/// <returns></returns>
		public List<FacebookCuentaPublicitariaDTO> ObtenerCombo()
		{
			try
			{
				List<FacebookCuentaPublicitariaDTO> facebookCuentaPublicitariaDTOs = new List<FacebookCuentaPublicitariaDTO>();
				facebookCuentaPublicitariaDTOs = GetBy(x => true, y => new FacebookCuentaPublicitariaDTO
				{
					Id = y.Id,
					FacebookIdCuentaPublicitaria = y.FacebookIdCuentaPublicitaria,
					Nombre = y.Nombre
				}).ToList();
				return facebookCuentaPublicitariaDTOs;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtener Cuenta por Id.
		/// </summary>
		/// <returns></returns>
		public List<FacebookCuentaPublicitariaDTO> ObtenerCuentaPorIdFacebook(int IdFacebookAudiencia)
		{
			try
			{
				List<FacebookCuentaPublicitariaDTO> Cuenta = new List<FacebookCuentaPublicitariaDTO>();
				string _queryCuentas = "Select Id,Nombre,FacebookIdCuentaPublicitaria From mkt.V_FacebookObtenerCuenta Where Estado=1 and IdFacebookAudiencia =@IdFacebookAudiencia";
				var queryCuentas = _dapper.QueryDapper(_queryCuentas, new { IdFacebookAudiencia });

				if (queryCuentas != "[]" && queryCuentas != "null")
				{
					Cuenta = JsonConvert.DeserializeObject<List<FacebookCuentaPublicitariaDTO>>(queryCuentas);
				}

				return Cuenta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
