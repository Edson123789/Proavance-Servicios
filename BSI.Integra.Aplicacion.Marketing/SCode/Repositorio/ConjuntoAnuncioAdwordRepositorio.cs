using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;



namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class ConjuntoAnuncioAdwordRepositorio : BaseRepository<TConjuntoAnuncioAdword, ConjuntoAnuncioAdwordBO>
	{
		#region Metodos Base
		public ConjuntoAnuncioAdwordRepositorio() : base()
		{
		}
		public ConjuntoAnuncioAdwordRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ConjuntoAnuncioAdwordBO> GetBy(Expression<Func<TConjuntoAnuncioAdword, bool>> filter)
		{
			IEnumerable<TConjuntoAnuncioAdword> listado = base.GetBy(filter);
			List<ConjuntoAnuncioAdwordBO> listadoBO = new List<ConjuntoAnuncioAdwordBO>();
			foreach (var itemEntidad in listado)
			{
				ConjuntoAnuncioAdwordBO objetoBO = Mapper.Map<TConjuntoAnuncioAdword, ConjuntoAnuncioAdwordBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ConjuntoAnuncioAdwordBO FirstById(int id)
		{
			try
			{
				TConjuntoAnuncioAdword entidad = base.FirstById(id);
				ConjuntoAnuncioAdwordBO objetoBO = new ConjuntoAnuncioAdwordBO();
				Mapper.Map<TConjuntoAnuncioAdword, ConjuntoAnuncioAdwordBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ConjuntoAnuncioAdwordBO FirstBy(Expression<Func<TConjuntoAnuncioAdword, bool>> filter)
		{
			try
			{
				TConjuntoAnuncioAdword entidad = base.FirstBy(filter);
				ConjuntoAnuncioAdwordBO objetoBO = Mapper.Map<TConjuntoAnuncioAdword, ConjuntoAnuncioAdwordBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ConjuntoAnuncioAdwordBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TConjuntoAnuncioAdword entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ConjuntoAnuncioAdwordBO> listadoBO)
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

		public bool Update(ConjuntoAnuncioAdwordBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TConjuntoAnuncioAdword entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ConjuntoAnuncioAdwordBO> listadoBO)
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
		private void AsignacionId(TConjuntoAnuncioAdword entidad, ConjuntoAnuncioAdwordBO objetoBO)
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

		private TConjuntoAnuncioAdword MapeoEntidad(ConjuntoAnuncioAdwordBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TConjuntoAnuncioAdword entidad = new TConjuntoAnuncioAdword();
				entidad = Mapper.Map<ConjuntoAnuncioAdwordBO, TConjuntoAnuncioAdword>(objetoBO,
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
		/// Obtiene Anuncio mediante su nombre
		/// </summary>
		/// <param name="Nombre"></param>
		/// <returns></returns>
		public ConjuntoAnuncioAdwordDTO ObtenerAnuncioAdword(string Nombre)
		{
			try
			{
				string query = "Select Id, id_f, campaign_id, created_time, effective_status, name, optimization_goal, start_time, status, updated_time, tiene_insights, es_validado, es_integra, es_publicado, activo_actualizado, FK_CampaniaIntegra, es_relacionado, es_otros, CuentaPublicitaria, NombreCampania, CentroCosto, tipo_campania, FechaCreacion from mkt.V_TConjuntoAnuncioAdword_ObtenerCampanias WHERE Estado = 1 and name Like CONCAT('%',@nombre,'%') ";
				var Anuncio = _dapper.FirstOrDefault(query, new { nombre = Nombre });
				return JsonConvert.DeserializeObject<ConjuntoAnuncioAdwordDTO>(Anuncio);
			}
			catch (Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}

		/// <summary>
		/// Obtiene anuncio mediante id
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ConjuntoAnuncioAdwordDTO ObtenerAnuncioAdwordPorId(int Id)
		{
			try
			{
				string query = "Select Id, id_f, campaign_id, created_time, effective_status, name, optimization_goal, start_time, status, updated_time, tiene_insights, es_validado, es_integra, es_publicado, activo_actualizado, FK_CampaniaIntegra, es_relacionado, es_otros, CuentaPublicitaria, NombreCampania, CentroCosto, tipo_campania from mkt.V_TConjuntoAnuncioAdword_ObtenerCampanias WHERE Estado = 1 and Id = @id";
				var Anuncio = _dapper.FirstOrDefault(query, new { id = Id});
				return JsonConvert.DeserializeObject<ConjuntoAnuncioAdwordDTO>(Anuncio);
			}
			catch (Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}

		/// <summary>
		/// Filtro por nombre para saber si un anuncio existe
		/// </summary>
		/// <param name="Nombre"></param>
		/// <returns></returns>
		public bool Contiene(string Nombre)
		{
			try
			{
				string query = "Select name from mkt.T_ConjuntoAnuncioAdword WHERE Estado = 1 and name Like CONCAT('%',@nombre,'%') ";
				var Anuncio = _dapper.FirstOrDefault(query, new { nombre = Nombre });
				var AnuncioD = JsonConvert.DeserializeObject<ConjuntoAnuncioAdwordDTO>(Anuncio);
				if(AnuncioD!=null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}

	}
}
