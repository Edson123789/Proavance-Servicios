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
	public class DatoContratoComisionBonoRepositorio : BaseRepository<TDatoContratoComisionBono, DatoContratoComisionBonoBO>
	{
		#region Metodos Base
		public DatoContratoComisionBonoRepositorio() : base()
		{
		}
		public DatoContratoComisionBonoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DatoContratoComisionBonoBO> GetBy(Expression<Func<TDatoContratoComisionBono, bool>> filter)
		{
			IEnumerable<TDatoContratoComisionBono> listado = base.GetBy(filter);
			List<DatoContratoComisionBonoBO> listadoBO = new List<DatoContratoComisionBonoBO>();
			foreach (var itemEntidad in listado)
			{
				DatoContratoComisionBonoBO objetoBO = Mapper.Map<TDatoContratoComisionBono, DatoContratoComisionBonoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DatoContratoComisionBonoBO FirstById(int id)
		{
			try
			{
				TDatoContratoComisionBono entidad = base.FirstById(id);
				DatoContratoComisionBonoBO objetoBO = new DatoContratoComisionBonoBO();
				Mapper.Map<TDatoContratoComisionBono, DatoContratoComisionBonoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DatoContratoComisionBonoBO FirstBy(Expression<Func<TDatoContratoComisionBono, bool>> filter)
		{
			try
			{
				TDatoContratoComisionBono entidad = base.FirstBy(filter);
				DatoContratoComisionBonoBO objetoBO = Mapper.Map<TDatoContratoComisionBono, DatoContratoComisionBonoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DatoContratoComisionBonoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDatoContratoComisionBono entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DatoContratoComisionBonoBO> listadoBO)
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

		public bool Update(DatoContratoComisionBonoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDatoContratoComisionBono entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DatoContratoComisionBonoBO> listadoBO)
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
		private void AsignacionId(TDatoContratoComisionBono entidad, DatoContratoComisionBonoBO objetoBO)
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

		private TDatoContratoComisionBono MapeoEntidad(DatoContratoComisionBonoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDatoContratoComisionBono entidad = new TDatoContratoComisionBono();
				entidad = Mapper.Map<DatoContratoComisionBonoBO, TDatoContratoComisionBono>(objetoBO,
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
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns></returns>
		public List<FiltroIdNombreDTO> ObtenerListaParaFiltro()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO
				{
					Id = x.Id,
					Nombre = x.Concepto
				}).ToList(); ;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}




		/// <summary>
		/// Este método obtiene la lista de remuneraciones variables de determinado contrato
		/// </summary>
		/// <returns></returns>
		public List<DatoContratoComisionBonoBO> ObtenerRegistros(int IdDatoContratoPersonal)
		{
			try
			{
				return this.GetBy(x => x.IdDatoContratoPersonal == IdDatoContratoPersonal).Select(x => new DatoContratoComisionBonoBO
				{
					Id = x.Id,
					IdDatoContratoPersonal = x.IdDatoContratoPersonal,
					TipoRemuneracionVariable = x.TipoRemuneracionVariable,
					Monto = x.Monto,
					Concepto = x.Concepto,
					Estado = x.Estado,
				}).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
