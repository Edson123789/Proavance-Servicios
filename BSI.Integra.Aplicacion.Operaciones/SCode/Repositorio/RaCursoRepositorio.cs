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
	public class RaCursoRepositorio : BaseRepository<TRaCurso, RaCursoBO>
	{
		#region Metodos Base
		public RaCursoRepositorio() : base()
		{
		}
		public RaCursoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaCursoBO> GetBy(Expression<Func<TRaCurso, bool>> filter)
		{
			IEnumerable<TRaCurso> listado = base.GetBy(filter);
			List<RaCursoBO> listadoBO = new List<RaCursoBO>();
			foreach (var itemEntidad in listado)
			{
				RaCursoBO objetoBO = Mapper.Map<TRaCurso, RaCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaCursoBO FirstById(int id)
		{
			try
			{
				TRaCurso entidad = base.FirstById(id);
				RaCursoBO objetoBO = new RaCursoBO();
				Mapper.Map<TRaCurso, RaCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaCursoBO FirstBy(Expression<Func<TRaCurso, bool>> filter)
		{
			try
			{
				TRaCurso entidad = base.FirstBy(filter);
				RaCursoBO objetoBO = Mapper.Map<TRaCurso, RaCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaCursoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaCurso entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaCursoBO> listadoBO)
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

		public bool Update(RaCursoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaCurso entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaCursoBO> listadoBO)
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
		private void AsignacionId(TRaCurso entidad, RaCursoBO objetoBO)
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

		private TRaCurso MapeoEntidad(RaCursoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaCurso entidad = new TRaCurso();
				entidad = Mapper.Map<RaCursoBO, TRaCurso>(objetoBO,
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
		public List<RaCursoDTO> ObtenerCursos(int idCentroCosto)
		{
			try
			{
				return GetBy(x => x.Estado == true && x.IdRaCentroCosto == idCentroCosto, x => new RaCursoDTO {
					Id = x.Id,
					IdRaCentroCosto = x.IdRaCentroCosto,
					NombreCurso = x.NombreCurso,
					IdRaTipoCurso = x.IdRaTipoCurso,
					Silabo = x.Silabo,
					IdExpositor = x.IdExpositor,
					PorcentajeAsistencia = x.PorcentajeAsistencia,
					Orden = x.Orden,
					Grupo = x.Grupo,
					PlazoPagoDias = x.PlazoPagoDias,
					EsInicioAonline = x.EsInicioAonline,
					IdMoneda = x.IdMoneda,
					CostoHora = x.CostoHora,
					IdRaTipoContrato = x.IdRaTipoContrato,
					IdMonedaTipoCambio = x.IdMonedaTipoCambio,
					TipoCambio = x.TipoCambio,
					FechaTipoCambio = x.FechaTipoCambio,
					Finalizado = x.Finalizado
				}).ToList();
	        }
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene un listado de cursos que pertenecen a centros de costo
        /// </summary>
        /// <returns></returns>
        public List<FiltroCursoDependienteCentroCostoDTO> ObtenerFiltroDependienteCentroCosto() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroCursoDependienteCentroCostoDTO { Id = x.Id,  Nombre = x.NombreCurso, IdRaCentroCosto = x.IdRaCentroCosto}).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
	}
}
