using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	public class RecuperacionSesionRepositorio : BaseRepository<TRecuperacionSesion, RecuperacionSesionBO>
	{
		#region Metodos Base
		public RecuperacionSesionRepositorio() : base()
		{
		}
		public RecuperacionSesionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RecuperacionSesionBO> GetBy(Expression<Func<TRecuperacionSesion, bool>> filter)
		{
			IEnumerable<TRecuperacionSesion> listado = base.GetBy(filter);
			List<RecuperacionSesionBO> listadoBO = new List<RecuperacionSesionBO>();
			foreach (var itemEntidad in listado)
			{
				RecuperacionSesionBO objetoBO = Mapper.Map<TRecuperacionSesion, RecuperacionSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RecuperacionSesionBO FirstById(int id)
		{
			try
			{
				TRecuperacionSesion entidad = base.FirstById(id);
				RecuperacionSesionBO objetoBO = new RecuperacionSesionBO();
				Mapper.Map<TRecuperacionSesion, RecuperacionSesionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RecuperacionSesionBO FirstBy(Expression<Func<TRecuperacionSesion, bool>> filter)
		{
			try
			{
				TRecuperacionSesion entidad = base.FirstBy(filter);
				RecuperacionSesionBO objetoBO = Mapper.Map<TRecuperacionSesion, RecuperacionSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RecuperacionSesionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRecuperacionSesion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RecuperacionSesionBO> listadoBO)
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

		public bool Update(RecuperacionSesionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRecuperacionSesion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RecuperacionSesionBO> listadoBO)
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
		private void AsignacionId(TRecuperacionSesion entidad, RecuperacionSesionBO objetoBO)
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

		private TRecuperacionSesion MapeoEntidad(RecuperacionSesionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRecuperacionSesion entidad = new TRecuperacionSesion();
				entidad = Mapper.Map<RecuperacionSesionBO, TRecuperacionSesion>(objetoBO,
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
