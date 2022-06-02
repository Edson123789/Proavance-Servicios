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
	public class RaTipoContratoRepositorio : BaseRepository<TRaTipoContrato, RaTipoContratoBO>
	{
		#region Metodos Base
		public RaTipoContratoRepositorio() : base()
		{
		}
		public RaTipoContratoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaTipoContratoBO> GetBy(Expression<Func<TRaTipoContrato, bool>> filter)
		{
			IEnumerable<TRaTipoContrato> listado = base.GetBy(filter);
			List<RaTipoContratoBO> listadoBO = new List<RaTipoContratoBO>();
			foreach (var itemEntidad in listado)
			{
				RaTipoContratoBO objetoBO = Mapper.Map<TRaTipoContrato, RaTipoContratoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaTipoContratoBO FirstById(int id)
		{
			try
			{
				TRaTipoContrato entidad = base.FirstById(id);
				RaTipoContratoBO objetoBO = new RaTipoContratoBO();
				Mapper.Map<TRaTipoContrato, RaTipoContratoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaTipoContratoBO FirstBy(Expression<Func<TRaTipoContrato, bool>> filter)
		{
			try
			{
				TRaTipoContrato entidad = base.FirstBy(filter);
				RaTipoContratoBO objetoBO = Mapper.Map<TRaTipoContrato, RaTipoContratoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaTipoContratoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaTipoContrato entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaTipoContratoBO> listadoBO)
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

		public bool Update(RaTipoContratoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaTipoContrato entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaTipoContratoBO> listadoBO)
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
		private void AsignacionId(TRaTipoContrato entidad, RaTipoContratoBO objetoBO)
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

		private TRaTipoContrato MapeoEntidad(RaTipoContratoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaTipoContrato entidad = new TRaTipoContrato();
				entidad = Mapper.Map<RaTipoContratoBO, TRaTipoContrato>(objetoBO,
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
		public List<FiltroGenericoDTO> ObtenerFiltroTipoContrato()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
