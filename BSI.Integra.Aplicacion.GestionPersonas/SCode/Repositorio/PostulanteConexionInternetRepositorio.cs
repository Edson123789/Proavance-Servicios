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
	public class PostulanteConexionInternetRepositorio : BaseRepository<TPostulanteConexionInternet, PostulanteConexionInternetBO>
	{
		#region Metodos Base
		public PostulanteConexionInternetRepositorio() : base()
		{
		}
		public PostulanteConexionInternetRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteConexionInternetBO> GetBy(Expression<Func<TPostulanteConexionInternet, bool>> filter)
		{
			IEnumerable<TPostulanteConexionInternet> listado = base.GetBy(filter);
			List<PostulanteConexionInternetBO> listadoBO = new List<PostulanteConexionInternetBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteConexionInternetBO objetoBO = Mapper.Map<TPostulanteConexionInternet, PostulanteConexionInternetBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteConexionInternetBO FirstById(int id)
		{
			try
			{
				TPostulanteConexionInternet entidad = base.FirstById(id);
				PostulanteConexionInternetBO objetoBO = new PostulanteConexionInternetBO();
				Mapper.Map<TPostulanteConexionInternet, PostulanteConexionInternetBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteConexionInternetBO FirstBy(Expression<Func<TPostulanteConexionInternet, bool>> filter)
		{
			try
			{
				TPostulanteConexionInternet entidad = base.FirstBy(filter);
				PostulanteConexionInternetBO objetoBO = Mapper.Map<TPostulanteConexionInternet, PostulanteConexionInternetBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteConexionInternetBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulanteConexionInternet entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteConexionInternetBO> listadoBO)
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

		public bool Update(PostulanteConexionInternetBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulanteConexionInternet entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteConexionInternetBO> listadoBO)
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
		private void AsignacionId(TPostulanteConexionInternet entidad, PostulanteConexionInternetBO objetoBO)
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

		private TPostulanteConexionInternet MapeoEntidad(PostulanteConexionInternetBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulanteConexionInternet entidad = new TPostulanteConexionInternet();
				entidad = Mapper.Map<PostulanteConexionInternetBO, TPostulanteConexionInternet>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteConexionInternetBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulanteConexionInternet, bool>>> filters, Expression<Func<TPostulanteConexionInternet, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulanteConexionInternet> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteConexionInternetBO> listadoBO = new List<PostulanteConexionInternetBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteConexionInternetBO objetoBO = Mapper.Map<TPostulanteConexionInternet, PostulanteConexionInternetBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion


		/// <summary>
		/// Obtiene equipo de computo del postulante
		/// </summary>
		/// <param name="IdPostulante"></param>
		/// <returns></returns>
		public PostulanteConexionInternetDTO ObtenerPostulanteConexionInternet(int idPostulante)
		{
			try
			{
				PostulanteConexionInternetDTO objeto = new PostulanteConexionInternetDTO();
				string query = "SELECT Id, IdPostulante, TipoConexion, MedioConexion, VelocidadInternet, ProveedorInternet, CostoInternet, ConexionCompartida FROM gp.V_TPostulanteConexionInternet_ObtenerInformacion WHERE Estado = 1 AND IdPostulante = @IdPOstulante";
				var res = _dapper.FirstOrDefault(query, new { IdPostulante = idPostulante });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<PostulanteConexionInternetDTO>(res);
				}
				return objeto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
