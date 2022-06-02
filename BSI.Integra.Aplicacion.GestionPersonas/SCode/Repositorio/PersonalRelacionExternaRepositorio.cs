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
	/// Repositorio: PersonalRelacionExternaRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 10/09/2020
	/// <summary>
	/// Gestión de Usuarios y Accesos al sistema T_PersonalRelacionExterna
	/// </summary>
	public class PersonalRelacionExternaRepositorio : BaseRepository<TPersonalRelacionExterna, PersonalRelacionExternaBO>
	{
		#region Metodos Base
		public PersonalRelacionExternaRepositorio() : base()
		{
		}
		public PersonalRelacionExternaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalRelacionExternaBO> GetBy(Expression<Func<TPersonalRelacionExterna, bool>> filter)
		{
			IEnumerable<TPersonalRelacionExterna> listado = base.GetBy(filter);
			List<PersonalRelacionExternaBO> listadoBO = new List<PersonalRelacionExternaBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalRelacionExternaBO objetoBO = Mapper.Map<TPersonalRelacionExterna, PersonalRelacionExternaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalRelacionExternaBO FirstById(int id)
		{
			try
			{
				TPersonalRelacionExterna entidad = base.FirstById(id);
				PersonalRelacionExternaBO objetoBO = new PersonalRelacionExternaBO();
				Mapper.Map<TPersonalRelacionExterna, PersonalRelacionExternaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalRelacionExternaBO FirstBy(Expression<Func<TPersonalRelacionExterna, bool>> filter)
		{
			try
			{
				TPersonalRelacionExterna entidad = base.FirstBy(filter);
				PersonalRelacionExternaBO objetoBO = Mapper.Map<TPersonalRelacionExterna, PersonalRelacionExternaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalRelacionExternaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalRelacionExterna entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalRelacionExternaBO> listadoBO)
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

		public bool Update(PersonalRelacionExternaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalRelacionExterna entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalRelacionExternaBO> listadoBO)
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
		private void AsignacionId(TPersonalRelacionExterna entidad, PersonalRelacionExternaBO objetoBO)
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

		private TPersonalRelacionExterna MapeoEntidad(PersonalRelacionExternaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalRelacionExterna entidad = new TPersonalRelacionExterna();
				entidad = Mapper.Map<PersonalRelacionExternaBO, TPersonalRelacionExterna>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PersonalRelacionExternaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPersonalRelacionExterna, bool>>> filters, Expression<Func<TPersonalRelacionExterna, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPersonalRelacionExterna> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PersonalRelacionExternaBO> listadoBO = new List<PersonalRelacionExternaBO>();

			foreach (var itemEntidad in listado)
			{
				PersonalRelacionExternaBO objetoBO = Mapper.Map<TPersonalRelacionExterna, PersonalRelacionExternaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// Repositorio: PersonalRelacionExternaRepositorio
		/// Autor: Luis Huallpa.
		/// Fecha: 10/09/2021
		/// <summary>
		/// Obtiene lista de personal relacion externa registrado
		/// </summary>
		/// <returns>List<PersonalRelacionExternaDTO></returns>
		public List<PersonalRelacionExternaDTO> ObtenerPersonalRelacionExterna()
		{
			try
			{
				List<PersonalRelacionExternaDTO> objeto = new List<PersonalRelacionExternaDTO>();
				var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo FROM [gp].[V_TPersonalRelacionExterna_ObtenerRegistros] WHERE Estado = 1";
				var resultado = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
				{
					objeto = JsonConvert.DeserializeObject<List<PersonalRelacionExternaDTO>>(resultado);
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
