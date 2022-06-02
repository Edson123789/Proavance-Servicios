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
	/// Repositorio: PersonalTipoFuncionRepositorio
	/// Autor: Luis Huallpa
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión tipo de funciones de Personal
	/// </summary>
	public class PersonalTipoFuncionRepositorio : BaseRepository<TPersonalTipoFuncion, PersonalTipoFuncionBO>
	{
		#region Metodos Base
		public PersonalTipoFuncionRepositorio() : base()
		{
		}
		public PersonalTipoFuncionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalTipoFuncionBO> GetBy(Expression<Func<TPersonalTipoFuncion, bool>> filter)
		{
			IEnumerable<TPersonalTipoFuncion> listado = base.GetBy(filter);
			List<PersonalTipoFuncionBO> listadoBO = new List<PersonalTipoFuncionBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalTipoFuncionBO objetoBO = Mapper.Map<TPersonalTipoFuncion, PersonalTipoFuncionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalTipoFuncionBO FirstById(int id)
		{
			try
			{
				TPersonalTipoFuncion entidad = base.FirstById(id);
				PersonalTipoFuncionBO objetoBO = new PersonalTipoFuncionBO();
				Mapper.Map<TPersonalTipoFuncion, PersonalTipoFuncionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalTipoFuncionBO FirstBy(Expression<Func<TPersonalTipoFuncion, bool>> filter)
		{
			try
			{
				TPersonalTipoFuncion entidad = base.FirstBy(filter);
				PersonalTipoFuncionBO objetoBO = Mapper.Map<TPersonalTipoFuncion, PersonalTipoFuncionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalTipoFuncionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalTipoFuncion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalTipoFuncionBO> listadoBO)
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

		public bool Update(PersonalTipoFuncionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalTipoFuncion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalTipoFuncionBO> listadoBO)
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
		private void AsignacionId(TPersonalTipoFuncion entidad, PersonalTipoFuncionBO objetoBO)
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

		private TPersonalTipoFuncion MapeoEntidad(PersonalTipoFuncionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalTipoFuncion entidad = new TPersonalTipoFuncion();
				entidad = Mapper.Map<PersonalTipoFuncionBO, TPersonalTipoFuncion>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PersonalTipoFuncionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPersonalTipoFuncion, bool>>> filters, Expression<Func<TPersonalTipoFuncion, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPersonalTipoFuncion> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PersonalTipoFuncionBO> listadoBO = new List<PersonalTipoFuncionBO>();

			foreach (var itemEntidad in listado)
			{
				PersonalTipoFuncionBO objetoBO = Mapper.Map<TPersonalTipoFuncion, PersonalTipoFuncionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion
		/// Repositorio: PersonalTipoFuncionRepositorio
		/// Autor: Luis Huallpa.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de funciones de personal
		/// </summary>
		/// <returns>List<PersonalTipoFuncionDTO></returns>
		public List<PersonalTipoFuncionDTO> ObtenerPersonalTipoFuncion()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new PersonalTipoFuncionDTO
				{
					Id = x.Id,
					Nombre = x.Nombre
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
