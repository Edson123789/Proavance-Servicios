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

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: PersonalDireccionRepositorio
	/// Autor: Luis Huallpa .
	/// Fecha: 16/06/2021
	/// <summary>
	/// Repositorio para de tabla T_PersonalDireccion
	/// </summary>
	public class PersonalDireccionRepositorio : BaseRepository<TPersonalDireccion, PersonalDireccionBO>
	{
		#region Metodos Base
		public PersonalDireccionRepositorio() : base()
		{
		}
		public PersonalDireccionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalDireccionBO> GetBy(Expression<Func<TPersonalDireccion, bool>> filter)
		{
			IEnumerable<TPersonalDireccion> listado = base.GetBy(filter);
			List<PersonalDireccionBO> listadoBO = new List<PersonalDireccionBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalDireccionBO objetoBO = Mapper.Map<TPersonalDireccion, PersonalDireccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalDireccionBO FirstById(int id)
		{
			try
			{
				TPersonalDireccion entidad = base.FirstById(id);
				PersonalDireccionBO objetoBO = new PersonalDireccionBO();
				Mapper.Map<TPersonalDireccion, PersonalDireccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalDireccionBO FirstBy(Expression<Func<TPersonalDireccion, bool>> filter)
		{
			try
			{
				TPersonalDireccion entidad = base.FirstBy(filter);
				PersonalDireccionBO objetoBO = Mapper.Map<TPersonalDireccion, PersonalDireccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalDireccionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalDireccion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalDireccionBO> listadoBO)
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

		public bool Update(PersonalDireccionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalDireccion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalDireccionBO> listadoBO)
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
		private void AsignacionId(TPersonalDireccion entidad, PersonalDireccionBO objetoBO)
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

		private TPersonalDireccion MapeoEntidad(PersonalDireccionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalDireccion entidad = new TPersonalDireccion();
				entidad = Mapper.Map<PersonalDireccionBO, TPersonalDireccion>(objetoBO,
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
		/// Obtiene lista de Direcciones del personal por idpersonal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
		public List<PersonalDireccionVistaDTO> ObtenerPersonalDireccion(int idPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdPersonal == idPersonal).Select(x => new PersonalDireccionVistaDTO
				{
					IdPais = x.IdPais,
					IdCiudad = x.IdCiudad,
					Distrito = x.Distrito,
					TipoVia = x.TipoVia,
					NombreVia = x.NombreVia,
					TipoZonaUrbana = x.TipoZonaUrbana,
					NombreZonaUrbana = x.NombreZonaUrbana,
					Lote = x.Lote,
					Manzana = x.Manzana,
					Activo = x.Activo,
					UsuarioModificacion = x.UsuarioModificacion,
					FechaModificacion = x.FechaModificacion
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
