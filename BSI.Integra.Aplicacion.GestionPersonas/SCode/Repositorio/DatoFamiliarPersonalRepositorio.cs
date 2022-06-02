using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio
{
	/// Repositorio: DatoFamiliarPersonalRepositorio
	/// Autor: Luis Huallpa.
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_DatoFamiliarPersonal
	/// </summary>
	public class DatoFamiliarPersonalRepositorio : BaseRepository<TDatoFamiliarPersonal, DatoFamiliarPersonalBO>
	{
		#region Metodos Base
		public DatoFamiliarPersonalRepositorio() : base()
		{
		}
		public DatoFamiliarPersonalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DatoFamiliarPersonalBO> GetBy(Expression<Func<TDatoFamiliarPersonal, bool>> filter)
		{
			IEnumerable<TDatoFamiliarPersonal> listado = base.GetBy(filter);
			List<DatoFamiliarPersonalBO> listadoBO = new List<DatoFamiliarPersonalBO>();
			foreach (var itemEntidad in listado)
			{
				DatoFamiliarPersonalBO objetoBO = Mapper.Map<TDatoFamiliarPersonal, DatoFamiliarPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DatoFamiliarPersonalBO FirstById(int id)
		{
			try
			{
				TDatoFamiliarPersonal entidad = base.FirstById(id);
				DatoFamiliarPersonalBO objetoBO = new DatoFamiliarPersonalBO();
				Mapper.Map<TDatoFamiliarPersonal, DatoFamiliarPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DatoFamiliarPersonalBO FirstBy(Expression<Func<TDatoFamiliarPersonal, bool>> filter)
		{
			try
			{
				TDatoFamiliarPersonal entidad = base.FirstBy(filter);
				DatoFamiliarPersonalBO objetoBO = Mapper.Map<TDatoFamiliarPersonal, DatoFamiliarPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DatoFamiliarPersonalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDatoFamiliarPersonal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DatoFamiliarPersonalBO> listadoBO)
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

		public bool Update(DatoFamiliarPersonalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDatoFamiliarPersonal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DatoFamiliarPersonalBO> listadoBO)
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
		private void AsignacionId(TDatoFamiliarPersonal entidad, DatoFamiliarPersonalBO objetoBO)
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

		private TDatoFamiliarPersonal MapeoEntidad(DatoFamiliarPersonalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDatoFamiliarPersonal entidad = new TDatoFamiliarPersonal();
				entidad = Mapper.Map<DatoFamiliarPersonalBO, TDatoFamiliarPersonal>(objetoBO,
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
		/// Repositorio: DatoFamiliarPersonalRepositorio
		/// Autor: Luis Huallpa - Britsel Calluchi.
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtiene lista de familiares registrador por idpersonal
		/// </summary>
		/// <param name="idPersonal"> Id de Personal </param>
		/// <returns> List<PersonalFamiliarDTO> </returns>
		public List<PersonalFamiliarDTO> ObtenerListaFamiliarPersonal(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalFamiliarDTO
				{
					Id = x.Id,
					IdPersonal = x.IdPersonal,
					Apellidos = x.Apellidos,
					DerechoHabiente = x.DerechoHabiente,
					EsContactoInmediato = x.EsContactoInmediato,
					FechaNacimiento = x.FechaNacimiento,
					IdParentescoPersonal = x.IdParentescoPersonal,
					IdSexo = x.IdSexo,
					IdTipoDocumentoPersonal = x.IdTipoDocumentoPersonal,
					Nombres = x.Nombres,
					NumeroDocumento = x.NumeroDocumento,
					NumeroReferencia = x.NumeroReferencia1
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// <summary>
		/// Obtiene lista de Familiares por personal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
		public List<DatoFamiliarPersonalFormularioDTO> ObtenerPorPersonal(int IdPersonal)
		{
			try
			{
				string query = "SELECT * from [gp].[TDatoFamiliarPersonal_ObtenerPersonalFamiliar] WHERE IdPersonal = @IdPersonal AND Estado = 1";
				string queryRespuesta = _dapper.QueryDapper(query, new { IdPersonal });
				return JsonConvert.DeserializeObject<List<DatoFamiliarPersonalFormularioDTO>>(queryRespuesta);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
