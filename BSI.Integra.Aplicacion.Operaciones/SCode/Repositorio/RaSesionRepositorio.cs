using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
	public class RaSesionRepositorio : BaseRepository<TRaSesion, RaSesionBO>
	{
		#region Metodos Base
		public RaSesionRepositorio() : base()
		{
		}
		public RaSesionRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<RaSesionBO> GetBy(Expression<Func<TRaSesion, bool>> filter)
		{
			IEnumerable<TRaSesion> listado = base.GetBy(filter);
			List<RaSesionBO> listadoBO = new List<RaSesionBO>();
			foreach (var itemEntidad in listado)
			{
				RaSesionBO objetoBO = Mapper.Map<TRaSesion, RaSesionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public RaSesionBO FirstById(int id)
		{
			try
			{
				TRaSesion entidad = base.FirstById(id);
				RaSesionBO objetoBO = new RaSesionBO();
				Mapper.Map<TRaSesion, RaSesionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public RaSesionBO FirstBy(Expression<Func<TRaSesion, bool>> filter)
		{
			try
			{
				TRaSesion entidad = base.FirstBy(filter);
				RaSesionBO objetoBO = Mapper.Map<TRaSesion, RaSesionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(RaSesionBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TRaSesion entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<RaSesionBO> listadoBO)
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

		public bool Update(RaSesionBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TRaSesion entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<RaSesionBO> listadoBO)
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
		private void AsignacionId(TRaSesion entidad, RaSesionBO objetoBO)
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

		private TRaSesion MapeoEntidad(RaSesionBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TRaSesion entidad = new TRaSesion();
				entidad = Mapper.Map<RaSesionBO, TRaSesion>(objetoBO,
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
        /// Obtiene el listado minimo de sesiones por curso
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public List<RaListadoMinimoSesionDTO> ObtenerListadoMinimoPorCurso(int idRaCurso)
        {
            try
            {
                List<RaListadoMinimoSesionDTO> sesiones = new List<RaListadoMinimoSesionDTO>();
                var query = "SELECT IdRaSesion, Fecha, Horario, HoraInicio, HoraFin, NombreExpositor, Tipo, NombreSede, NombreAula, BoletoAereo FROM ope.V_ObtenerListadoMinimoSesion WHERE IdRaCurso = @idRaCurso";
                var sesionesDB = _dapper.QueryDapper(query, new { idRaCurso });
                if (!string.IsNullOrEmpty(sesionesDB) && !sesionesDB.Contains("[]"))
                {
                    sesiones = JsonConvert.DeserializeObject<List<RaListadoMinimoSesionDTO>>(sesionesDB);
                }
                return sesiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene un listado por docente y periodo
		/// </summary>
		/// <param name="idExpositor"></param>
		/// <param name="fechaInicio"></param>
		/// <param name="fechaFin"></param>
		/// <returns></returns>
		public List<RaSesionBO> ListadoPorIdDocentePorPeriodo(int idExpositor, DateTime fechaInicio, DateTime fechaFin)
		{
			return this.GetBy(x => x.IdExpositor == idExpositor && x.Fecha >= fechaInicio && x.Fecha <= fechaFin).ToList();
		}


	}
}
