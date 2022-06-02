using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: Gestion de personas/PreguntaIntentoDetalle
    /// Autor: Luis Huallpa - Gian Miranda
    /// Fecha: 24/02/2021
    /// <summary>
    /// Repositorio para consultas de gp.T_PreguntaIntentoDetalle
    /// </summary>
	public class PreguntaIntentoDetalleRepositorio : BaseRepository<TPreguntaIntentoDetalle, PreguntaIntentoDetalleBO>
	{
		#region Metodos Base
		public PreguntaIntentoDetalleRepositorio() : base()
		{
		}
		public PreguntaIntentoDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PreguntaIntentoDetalleBO> GetBy(Expression<Func<TPreguntaIntentoDetalle, bool>> filter)
		{
			IEnumerable<TPreguntaIntentoDetalle> listado = base.GetBy(filter);
			List<PreguntaIntentoDetalleBO> listadoBO = new List<PreguntaIntentoDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				PreguntaIntentoDetalleBO objetoBO = Mapper.Map<TPreguntaIntentoDetalle, PreguntaIntentoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PreguntaIntentoDetalleBO FirstById(int id)
		{
			try
			{
				TPreguntaIntentoDetalle entidad = base.FirstById(id);
				PreguntaIntentoDetalleBO objetoBO = new PreguntaIntentoDetalleBO();
				Mapper.Map<TPreguntaIntentoDetalle, PreguntaIntentoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PreguntaIntentoDetalleBO FirstBy(Expression<Func<TPreguntaIntentoDetalle, bool>> filter)
		{
			try
			{
				TPreguntaIntentoDetalle entidad = base.FirstBy(filter);
				PreguntaIntentoDetalleBO objetoBO = Mapper.Map<TPreguntaIntentoDetalle, PreguntaIntentoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PreguntaIntentoDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPreguntaIntentoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PreguntaIntentoDetalleBO> listadoBO)
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

		public bool Update(PreguntaIntentoDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPreguntaIntentoDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PreguntaIntentoDetalleBO> listadoBO)
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
		private void AsignacionId(TPreguntaIntentoDetalle entidad, PreguntaIntentoDetalleBO objetoBO)
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

		private TPreguntaIntentoDetalle MapeoEntidad(PreguntaIntentoDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPreguntaIntentoDetalle entidad = new TPreguntaIntentoDetalle();
				entidad = Mapper.Map<PreguntaIntentoDetalleBO, TPreguntaIntentoDetalle>(objetoBO,
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
		/// Obtiene los intentos registrados por pregunta
		/// </summary>
		/// <param name="idPreguntaIntento">Id del intento de pregunta(PK de la tabla gp.T_PreguntaIntento)</param>
		/// <returns>Lista de objeto de tipo PreguntaIntentoDetalleDTO</returns>
		public List<PreguntaIntentoDetalleDTO> ObtenerIntentosPreguntaRegistrados(int idPreguntaIntento)
		{
			return this.GetBy(x => x.IdPreguntaIntento == idPreguntaIntento).Select(x => new PreguntaIntentoDetalleDTO
			{
				Id = x.Id,
				PorcentajeCalificacion = x.PorcentajeCalificacion == null ? 0 : x.PorcentajeCalificacion.Value
			}).ToList();
		}
	}
}
