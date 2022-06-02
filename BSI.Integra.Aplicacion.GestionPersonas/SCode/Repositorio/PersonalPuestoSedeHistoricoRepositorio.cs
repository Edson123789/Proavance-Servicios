///Repositorio: PersonalPuestoSedeHistorico
///Autor: Edgar S.
///Fecha: 19/01/2021
///<summary>
///Repositorio de T_PersonalPuestoSedeHistorico
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: PersonalPuestoSedeHistoricoRepositorio
	/// Autor: Edgar Serruto .
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_PersonalPuestoSedeHistorico
	/// </summary>
	public class PersonalPuestoSedeHistoricoRepositorio : BaseRepository<TPersonalPuestoSedeHistorico, PersonalPuestoSedeHistoricoBO>
	{
		#region Metodos Base
		public PersonalPuestoSedeHistoricoRepositorio() : base()
		{
		}
		public PersonalPuestoSedeHistoricoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalPuestoSedeHistoricoBO> GetBy(Expression<Func<TPersonalPuestoSedeHistorico, bool>> filter)
		{
			IEnumerable<TPersonalPuestoSedeHistorico> listado = base.GetBy(filter);
			List<PersonalPuestoSedeHistoricoBO> listadoBO = new List<PersonalPuestoSedeHistoricoBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalPuestoSedeHistoricoBO objetoBO = Mapper.Map<TPersonalPuestoSedeHistorico, PersonalPuestoSedeHistoricoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalPuestoSedeHistoricoBO FirstById(int id)
		{
			try
			{
				TPersonalPuestoSedeHistorico entidad = base.FirstById(id);
				PersonalPuestoSedeHistoricoBO objetoBO = new PersonalPuestoSedeHistoricoBO();
				Mapper.Map<TPersonalPuestoSedeHistorico, PersonalPuestoSedeHistoricoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalPuestoSedeHistoricoBO FirstBy(Expression<Func<TPersonalPuestoSedeHistorico, bool>> filter)
		{
			try
			{
				TPersonalPuestoSedeHistorico entidad = base.FirstBy(filter);
				PersonalPuestoSedeHistoricoBO objetoBO = Mapper.Map<TPersonalPuestoSedeHistorico, PersonalPuestoSedeHistoricoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalPuestoSedeHistoricoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalPuestoSedeHistorico entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalPuestoSedeHistoricoBO> listadoBO)
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

		public bool Update(PersonalPuestoSedeHistoricoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalPuestoSedeHistorico entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalPuestoSedeHistoricoBO> listadoBO)
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
		private void AsignacionId(TPersonalPuestoSedeHistorico entidad, PersonalPuestoSedeHistoricoBO objetoBO)
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

		private TPersonalPuestoSedeHistorico MapeoEntidad(PersonalPuestoSedeHistoricoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalPuestoSedeHistorico entidad = new TPersonalPuestoSedeHistorico();
				entidad = Mapper.Map<PersonalPuestoSedeHistoricoBO, TPersonalPuestoSedeHistorico>(objetoBO,
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
	}
}
