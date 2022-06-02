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
	public class RaTipoBoletoAereoRepositorio : BaseRepository<TRaTipoBoletoAereo, RaTipoBoletoAereoBO>
	{
		#region Metodos Base
		public RaTipoBoletoAereoRepositorio() : base()
		{
		}
		public RaTipoBoletoAereoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaTipoBoletoAereoBO> GetBy(Expression<Func<TRaTipoBoletoAereo, bool>> filter)
		{
			IEnumerable<TRaTipoBoletoAereo> listado = base.GetBy(filter);
			List<RaTipoBoletoAereoBO> listadoBO = new List<RaTipoBoletoAereoBO>();
			foreach (var itemEntidad in listado)
			{
				RaTipoBoletoAereoBO objetoBO = Mapper.Map<TRaTipoBoletoAereo, RaTipoBoletoAereoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaTipoBoletoAereoBO FirstById(int id)
		{
			try
			{
				TRaTipoBoletoAereo entidad = base.FirstById(id);
				RaTipoBoletoAereoBO objetoBO = new RaTipoBoletoAereoBO();
				Mapper.Map<TRaTipoBoletoAereo, RaTipoBoletoAereoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaTipoBoletoAereoBO FirstBy(Expression<Func<TRaTipoBoletoAereo, bool>> filter)
		{
			try
			{
				TRaTipoBoletoAereo entidad = base.FirstBy(filter);
				RaTipoBoletoAereoBO objetoBO = Mapper.Map<TRaTipoBoletoAereo, RaTipoBoletoAereoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaTipoBoletoAereoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaTipoBoletoAereo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaTipoBoletoAereoBO> listadoBO)
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

		public bool Update(RaTipoBoletoAereoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaTipoBoletoAereo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaTipoBoletoAereoBO> listadoBO)
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
		private void AsignacionId(TRaTipoBoletoAereo entidad, RaTipoBoletoAereoBO objetoBO)
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

		private TRaTipoBoletoAereo MapeoEntidad(RaTipoBoletoAereoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaTipoBoletoAereo entidad = new TRaTipoBoletoAereo();
				entidad = Mapper.Map<RaTipoBoletoAereoBO, TRaTipoBoletoAereo>(objetoBO,
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

		public List<FiltroGenericoDTO> ObtenerTipoBoletoAereoFiltro()
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
