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
	public class RaSedeRepositorio : BaseRepository<TRaSede, RaSedeBO>
	{
		#region Metodos Base
		public RaSedeRepositorio() : base()
		{
		}
		public RaSedeRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaSedeBO> GetBy(Expression<Func<TRaSede, bool>> filter)
		{
			IEnumerable<TRaSede> listado = base.GetBy(filter);
			List<RaSedeBO> listadoBO = new List<RaSedeBO>();
			foreach (var itemEntidad in listado)
			{
				RaSedeBO objetoBO = Mapper.Map<TRaSede, RaSedeBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaSedeBO FirstById(int id)
		{
			try
			{
				TRaSede entidad = base.FirstById(id);
				RaSedeBO objetoBO = new RaSedeBO();
				Mapper.Map<TRaSede, RaSedeBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaSedeBO FirstBy(Expression<Func<TRaSede, bool>> filter)
		{
			try
			{
				TRaSede entidad = base.FirstBy(filter);
				RaSedeBO objetoBO = Mapper.Map<TRaSede, RaSedeBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaSedeBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaSede entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaSedeBO> listadoBO)
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

		public bool Update(RaSedeBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaSede entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaSedeBO> listadoBO)
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
		private void AsignacionId(TRaSede entidad, RaSedeBO objetoBO)
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

		private TRaSede MapeoEntidad(RaSedeBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaSede entidad = new TRaSede();
				entidad = Mapper.Map<RaSedeBO, TRaSede>(objetoBO,
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
