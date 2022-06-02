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
	/// Repositorio: PersonalMotivoTiempoInactividadRepositorio
	/// Autor: Edgar S.
	/// Fecha: 18/03/2021
	/// <summary>
	/// Gestión de Personal Motivo y Tiempo de Inactividad
	/// </summary>
	public class PersonalMotivoTiempoInactividadRepositorio : BaseRepository<TPersonalMotivoTiempoInactividad, PersonalMotivoTiempoInactividadBO>
	{
		#region Metodos Base
		public PersonalMotivoTiempoInactividadRepositorio() : base()
		{
		}
		public PersonalMotivoTiempoInactividadRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PersonalMotivoTiempoInactividadBO> GetBy(Expression<Func<TPersonalMotivoTiempoInactividad, bool>> filter)
		{
			IEnumerable<TPersonalMotivoTiempoInactividad> listado = base.GetBy(filter);
			List<PersonalMotivoTiempoInactividadBO> listadoBO = new List<PersonalMotivoTiempoInactividadBO>();
			foreach (var itemEntidad in listado)
			{
				PersonalMotivoTiempoInactividadBO objetoBO = Mapper.Map<TPersonalMotivoTiempoInactividad, PersonalMotivoTiempoInactividadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PersonalMotivoTiempoInactividadBO FirstById(int id)
		{
			try
			{
				TPersonalMotivoTiempoInactividad entidad = base.FirstById(id);
				PersonalMotivoTiempoInactividadBO objetoBO = new PersonalMotivoTiempoInactividadBO();
				Mapper.Map<TPersonalMotivoTiempoInactividad, PersonalMotivoTiempoInactividadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PersonalMotivoTiempoInactividadBO FirstBy(Expression<Func<TPersonalMotivoTiempoInactividad, bool>> filter)
		{
			try
			{
				TPersonalMotivoTiempoInactividad entidad = base.FirstBy(filter);
				PersonalMotivoTiempoInactividadBO objetoBO = Mapper.Map<TPersonalMotivoTiempoInactividad, PersonalMotivoTiempoInactividadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PersonalMotivoTiempoInactividadBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPersonalMotivoTiempoInactividad entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PersonalMotivoTiempoInactividadBO> listadoBO)
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

		public bool Update(PersonalMotivoTiempoInactividadBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPersonalMotivoTiempoInactividad entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PersonalMotivoTiempoInactividadBO> listadoBO)
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
		private void AsignacionId(TPersonalMotivoTiempoInactividad entidad, PersonalMotivoTiempoInactividadBO objetoBO)
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

		private TPersonalMotivoTiempoInactividad MapeoEntidad(PersonalMotivoTiempoInactividadBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPersonalMotivoTiempoInactividad entidad = new TPersonalMotivoTiempoInactividad();
				entidad = Mapper.Map<PersonalMotivoTiempoInactividadBO, TPersonalMotivoTiempoInactividad>(objetoBO,
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
		/// Repositorio: PersonalMotivoTiempoInactividadRepositorio
		/// Autor: Edgar S.
		/// Fecha: 19/03/2021
		/// <summary>
		/// Obtiene registro de periodo inactivo por IdPersonal
		/// </summary>
		/// <param name="idPersonal"> Id de Personal </param>
		/// <returns> List<PersonalTiempoInactivoHistoricoDTO> </returns>
		public List<PersonalTiempoInactivoHistoricoDTO> ObtenerPeriodoInactivoHistorico(int idPersonal)
		{
			try
			{
				List<PersonalTiempoInactivoHistoricoDTO> listaTiempoInactivo = new List<PersonalTiempoInactivoHistoricoDTO>();
				string query = "SELECT Id, IdMotivoInactividad,MotivoInactividad,FechaInicio,FechaFin,Estado FROM [gp].[V_TPersonalMotivoTiempoInactividad_ObtenerHistorico] WHERE IdPersonal = @idPersonal";
				var resultado = _dapper.QueryDapper(query, new { idPersonal });
				if (!string.IsNullOrEmpty(resultado))
				{
					listaTiempoInactivo = JsonConvert.DeserializeObject<List<PersonalTiempoInactivoHistoricoDTO>>(resultado);
				}
				return listaTiempoInactivo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
