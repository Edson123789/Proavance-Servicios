using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	public class TokenPostulanteProcesoSeleccionRepositorio : BaseRepository<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionBO>
	{
		#region Metodos Base
		public TokenPostulanteProcesoSeleccionRepositorio() : base()
		{
		}
		public TokenPostulanteProcesoSeleccionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TokenPostulanteProcesoSeleccionBO> GetBy(Expression<Func<TTokenPostulanteProcesoSeleccion, bool>> filter)
		{
			IEnumerable<TTokenPostulanteProcesoSeleccion> listado = base.GetBy(filter);
			List<TokenPostulanteProcesoSeleccionBO> listadoBO = new List<TokenPostulanteProcesoSeleccionBO>();
			foreach (var itemEntidad in listado)
			{
				TokenPostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TokenPostulanteProcesoSeleccionBO FirstById(int id)
		{
			try
			{
				TTokenPostulanteProcesoSeleccion entidad = base.FirstById(id);
				TokenPostulanteProcesoSeleccionBO objetoBO = new TokenPostulanteProcesoSeleccionBO();
				Mapper.Map<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TokenPostulanteProcesoSeleccionBO FirstBy(Expression<Func<TTokenPostulanteProcesoSeleccion, bool>> filter)
		{
			try
			{
				TTokenPostulanteProcesoSeleccion entidad = base.FirstBy(filter);
				TokenPostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TokenPostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTokenPostulanteProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TokenPostulanteProcesoSeleccionBO> listadoBO)
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

		public bool Update(TokenPostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTokenPostulanteProcesoSeleccion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TokenPostulanteProcesoSeleccionBO> listadoBO)
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
		private void AsignacionId(TTokenPostulanteProcesoSeleccion entidad, TokenPostulanteProcesoSeleccionBO objetoBO)
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

		private TTokenPostulanteProcesoSeleccion MapeoEntidad(TokenPostulanteProcesoSeleccionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTokenPostulanteProcesoSeleccion entidad = new TTokenPostulanteProcesoSeleccion();
				entidad = Mapper.Map<TokenPostulanteProcesoSeleccionBO, TTokenPostulanteProcesoSeleccion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<TokenPostulanteProcesoSeleccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTokenPostulanteProcesoSeleccion, bool>>> filters, Expression<Func<TTokenPostulanteProcesoSeleccion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TTokenPostulanteProcesoSeleccion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<TokenPostulanteProcesoSeleccionBO> listadoBO = new List<TokenPostulanteProcesoSeleccionBO>();

			foreach (var itemEntidad in listado)
			{
				TokenPostulanteProcesoSeleccionBO objetoBO = Mapper.Map<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene el ultimo token registrado del postulante proceso seleccion
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public TokenPostulanteProcesoSeleccionDTO ObtenerUltimoTokenPorPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulanteProcesoSeleccion, Token, TokenHash, Activo FROM [gp].[V_TTokenPostulanteProcesoSeleccion_ObtenerTokenGenerados] WHERE IdPostulanteProcesoSeleccion = @IdPostulanteProcesoSeleccion AND Estado = 1 ORDER BY FechaCreacion DESC";
				var res = _dapper.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<TokenPostulanteProcesoSeleccionDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene lista de tokens de postulantes por proceso de seleccion
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public List<TokenPostulanteProcesoSeleccionDTO> ObtenerTokenPorPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulanteProcesoSeleccion, Token, TokenHash, Activo FROM [gp].[V_TTokenPostulanteProcesoSeleccion_ObtenerTokenGenerados] WHERE IdPostulanteProcesoSeleccion = @IdPostulanteProcesoSeleccion AND Estado = 1";
				var res = _dapper.QueryDapper(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<List<TokenPostulanteProcesoSeleccionDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
