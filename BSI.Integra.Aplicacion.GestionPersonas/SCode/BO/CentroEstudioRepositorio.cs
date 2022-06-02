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
	/// Repositorio: CentroEstudioRepositorio
	/// Autor: Luis Huallpa - Edgar Serruto.
	/// Fecha: 05/08/2021
	/// <summary>
	/// Gestión de Centro de Estudio registrados
	/// </summary>
	public class CentroEstudioRepositorio : BaseRepository<TCentroEstudio, CentroEstudioBO>
	{
		#region Metodos Base
		public CentroEstudioRepositorio() : base()
		{
		}
		public CentroEstudioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CentroEstudioBO> GetBy(Expression<Func<TCentroEstudio, bool>> filter)
		{
			IEnumerable<TCentroEstudio> listado = base.GetBy(filter);
			List<CentroEstudioBO> listadoBO = new List<CentroEstudioBO>();
			foreach (var itemEntidad in listado)
			{
				CentroEstudioBO objetoBO = Mapper.Map<TCentroEstudio, CentroEstudioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CentroEstudioBO FirstById(int id)
		{
			try
			{
				TCentroEstudio entidad = base.FirstById(id);
				CentroEstudioBO objetoBO = new CentroEstudioBO();
				Mapper.Map<TCentroEstudio, CentroEstudioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CentroEstudioBO FirstBy(Expression<Func<TCentroEstudio, bool>> filter)
		{
			try
			{
				TCentroEstudio entidad = base.FirstBy(filter);
				CentroEstudioBO objetoBO = Mapper.Map<TCentroEstudio, CentroEstudioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CentroEstudioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCentroEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CentroEstudioBO> listadoBO)
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

		public bool Update(CentroEstudioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCentroEstudio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CentroEstudioBO> listadoBO)
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
		private void AsignacionId(TCentroEstudio entidad, CentroEstudioBO objetoBO)
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

		private TCentroEstudio MapeoEntidad(CentroEstudioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCentroEstudio entidad = new TCentroEstudio();
				entidad = Mapper.Map<CentroEstudioBO, TCentroEstudio>(objetoBO,
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

		///Repositorio: CentroEstudioRepositorio
		///Autor: Edgar S.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> Lista de Objeto DTO : List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		///Repositorio: CentroEstudioRepositorio
		///Autor: Luis Huallpa - Edgar Serruto.
		///Fecha: 25/01/2021
		/// <summary>
		/// Obtiene lista de centros de estudio registrados
		/// </summary>
		/// <returns>Lista de Objeto DTO : List<CentroEstudioDTO></returns>
		public List<CentroEstudioDTO> ObtenerCentroEstudioRegistrado()
		{
			try
			{
				List<CentroEstudioDTO> lista = new List<CentroEstudioDTO>();
				var query = "SELECT Id, Nombre, IdPais, IdCiudad, Pais, Ciudad, IdTipoCentroEstudio, TipoCentroEstudio FROM gp.V_TCentroEstudio_ObtenerCentroEstudio WHERE Estado = 1";
				var resultado = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<CentroEstudioDTO>>(resultado);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		///Repositorio: CentroEstudioRepositorio
		///Autor: Luis Huallpa - Edgar S.
		///Fecha: 05/08/2021
		/// <summary>
		/// Obtiene lista de centros de estudio registrados
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		public List<FiltroIdNombreDTO> ObtenerTipoCentroEstudio()
		{
			try
			{
				List<FiltroIdNombreDTO> lista = new List<FiltroIdNombreDTO>();
				var query = "SELECT Id, Nombre FROM gp.V_TTipoCentroEstudio_ObtenerTipoCentroEstudio WHERE Estado = 1";
				var resultado = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<FiltroIdNombreDTO>>(resultado);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
