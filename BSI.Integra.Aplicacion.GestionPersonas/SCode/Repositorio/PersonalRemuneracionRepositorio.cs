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
	/// Repositorio: PersonalRemuneracionRepositorio
	/// Autor: Luis Huallpa.
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_PersonalRemuneracion
	/// </summary>
	public class PersonalRemuneracionRepositorio : BaseRepository<TPersonalRemuneracion, PersonalRemuneracionBO>
	{
		#region Metodos Base
		public PersonalRemuneracionRepositorio() : base()
		{
		}
		public PersonalRemuneracionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalRemuneracionBO> GetBy(Expression<Func<TPersonalRemuneracion, bool>> filter)
		{
			IEnumerable<TPersonalRemuneracion> listado = base.GetBy(filter);
			List<PersonalRemuneracionBO> listadoBO = new List<PersonalRemuneracionBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalRemuneracionBO objetoBO = Mapper.Map<TPersonalRemuneracion, PersonalRemuneracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalRemuneracionBO FirstById(int id)
		{
			try
			{
				TPersonalRemuneracion entidad = base.FirstById(id);
				PersonalRemuneracionBO objetoBO = new PersonalRemuneracionBO();
				Mapper.Map<TPersonalRemuneracion, PersonalRemuneracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalRemuneracionBO FirstBy(Expression<Func<TPersonalRemuneracion, bool>> filter)
		{
			try
			{
				TPersonalRemuneracion entidad = base.FirstBy(filter);
				PersonalRemuneracionBO objetoBO = Mapper.Map<TPersonalRemuneracion, PersonalRemuneracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalRemuneracionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalRemuneracionBO> listadoBO)
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

		public bool Update(PersonalRemuneracionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalRemuneracion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalRemuneracionBO> listadoBO)
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
		private void AsignacionId(TPersonalRemuneracion entidad, PersonalRemuneracionBO objetoBO)
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

		private TPersonalRemuneracion MapeoEntidad(PersonalRemuneracionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalRemuneracion entidad = new TPersonalRemuneracion();
				entidad = Mapper.Map<PersonalRemuneracionBO, TPersonalRemuneracion>(objetoBO,
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
		/// Repositorio: PersonalRemuneracionRepositorio
		/// Autor: Luis Huallpa - Britsel Calluchi - Edgar Serruto.
		/// Fecha: 16/06/2021
		/// <summary>
		/// Obtener personal remuneracion por idpersonal
		/// </summary>
		/// <param name="idPersonal"> Id de Personal </param>
		/// <returns> List<PersonalRemuneracionDTO> </returns>
		public List<PersonalRemuneracionDTO> ObtenerPersonalRemuneracion(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).OrderByDescending(x => x.FechaModificacion).Select(x => new PersonalRemuneracionDTO
				{
					IdEntidadFinanciera = x.IdEntidadFinanciera,
					IdTipoPagoRemuneracion = x.IdTipoPagoRemuneracion,
					NumeroCuenta = x.NumeroCuenta,
					Activo = x.Activo,
					FechaModificacion = x.FechaModificacion,
					UsuarioModificacion = x.UsuarioModificacion
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
