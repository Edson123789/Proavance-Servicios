using System;
using System.Collections.Generic;
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
	public class RaSkypeReunionRepositorio : BaseRepository<TRaSkypeReunion, RaSkypeReunionBO>
	{
		#region Metodos Base
		public RaSkypeReunionRepositorio() : base()
		{
		}
		public RaSkypeReunionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaSkypeReunionBO> GetBy(Expression<Func<TRaSkypeReunion, bool>> filter)
		{
			IEnumerable<TRaSkypeReunion> listado = base.GetBy(filter);
			List<RaSkypeReunionBO> listadoBO = new List<RaSkypeReunionBO>();
			foreach (var itemEntidad in listado)
			{
				RaSkypeReunionBO objetoBO = Mapper.Map<TRaSkypeReunion, RaSkypeReunionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaSkypeReunionBO FirstById(int id)
		{
			try
			{
				TRaSkypeReunion entidad = base.FirstById(id);
				RaSkypeReunionBO objetoBO = new RaSkypeReunionBO();
				Mapper.Map<TRaSkypeReunion, RaSkypeReunionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaSkypeReunionBO FirstBy(Expression<Func<TRaSkypeReunion, bool>> filter)
		{
			try
			{
				TRaSkypeReunion entidad = base.FirstBy(filter);
				RaSkypeReunionBO objetoBO = Mapper.Map<TRaSkypeReunion, RaSkypeReunionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaSkypeReunionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaSkypeReunion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaSkypeReunionBO> listadoBO)
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

		public bool Update(RaSkypeReunionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaSkypeReunion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaSkypeReunionBO> listadoBO)
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
		private void AsignacionId(TRaSkypeReunion entidad, RaSkypeReunionBO objetoBO)
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

		private TRaSkypeReunion MapeoEntidad(RaSkypeReunionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaSkypeReunion entidad = new TRaSkypeReunion();
				entidad = Mapper.Map<RaSkypeReunionBO, TRaSkypeReunion>(objetoBO,
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
