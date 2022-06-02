using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class ReporteAnalyticsFiltroDetalleRepositorio : BaseRepository<TReporteAnalyticsFiltroDetalle, ReporteAnalyticsFiltroDetalleBO>
	{
		#region Metodos Base
		public ReporteAnalyticsFiltroDetalleRepositorio() : base()
		{
		}
		public ReporteAnalyticsFiltroDetalleRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ReporteAnalyticsFiltroDetalleBO> GetBy(Expression<Func<TReporteAnalyticsFiltroDetalle, bool>> filter)
		{
			IEnumerable<TReporteAnalyticsFiltroDetalle> listado = base.GetBy(filter);
			List<ReporteAnalyticsFiltroDetalleBO> listadoBO = new List<ReporteAnalyticsFiltroDetalleBO>();
			foreach (var itemEntidad in listado)
			{
				ReporteAnalyticsFiltroDetalleBO objetoBO = Mapper.Map<TReporteAnalyticsFiltroDetalle, ReporteAnalyticsFiltroDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ReporteAnalyticsFiltroDetalleBO FirstById(int id)
		{
			try
			{
				TReporteAnalyticsFiltroDetalle entidad = base.FirstById(id);
				ReporteAnalyticsFiltroDetalleBO objetoBO = new ReporteAnalyticsFiltroDetalleBO();
				Mapper.Map<TReporteAnalyticsFiltroDetalle, ReporteAnalyticsFiltroDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ReporteAnalyticsFiltroDetalleBO FirstBy(Expression<Func<TReporteAnalyticsFiltroDetalle, bool>> filter)
		{
			try
			{
				TReporteAnalyticsFiltroDetalle entidad = base.FirstBy(filter);
				ReporteAnalyticsFiltroDetalleBO objetoBO = Mapper.Map<TReporteAnalyticsFiltroDetalle, ReporteAnalyticsFiltroDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ReporteAnalyticsFiltroDetalleBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TReporteAnalyticsFiltroDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ReporteAnalyticsFiltroDetalleBO> listadoBO)
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

		public bool Update(ReporteAnalyticsFiltroDetalleBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TReporteAnalyticsFiltroDetalle entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ReporteAnalyticsFiltroDetalleBO> listadoBO)
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
		private void AsignacionId(TReporteAnalyticsFiltroDetalle entidad, ReporteAnalyticsFiltroDetalleBO objetoBO)
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

		private TReporteAnalyticsFiltroDetalle MapeoEntidad(ReporteAnalyticsFiltroDetalleBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TReporteAnalyticsFiltroDetalle entidad = new TReporteAnalyticsFiltroDetalle();
				entidad = Mapper.Map<ReporteAnalyticsFiltroDetalleBO, TReporteAnalyticsFiltroDetalle>(objetoBO,
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
        /// Se eliminan los detalles que se eliminaron en la interfaz
        /// </summary>
        /// <param name="idReporteAnalyticsFiltro"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void DeleteLogico(int idReporteAnalyticsFiltro, string usuario, List<ReporteAnalyticsFiltroDetalleDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                listaBorrar = GetBy(x => x.IdReporteAnalyticsFiltro == idReporteAnalyticsFiltro, y => new EliminacionIdsDTO
                {
                    Id = y.Id
                }).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el detalle por IdReporteAnalyticsFiltro
        /// </summary>
        /// <returns></returns>
        public List<ReporteAnalyticsFiltroDetalleSimboloDTO> ObtenerDetallePorIdReporteAnalyticsFiltro(int idReporteAnalyticsFiltro)
        {
            try
            {
                List<ReporteAnalyticsFiltroDetalleSimboloDTO> lista = new List<ReporteAnalyticsFiltroDetalleSimboloDTO>();
                string query = "SELECT Texto, Excluir, Simbolo FROM mkt.V_TReporteAnalyticsFiltroDetalle_ObtenerDetalle Where EstadoDetalle=1 and IdReporteAnalyticsFiltro=@idReporteAnalyticsFiltro";
                string respuestaQuery = _dapper.QueryDapper(query, new { idReporteAnalyticsFiltro });
                if (respuestaQuery != "null" && !respuestaQuery.Contains("{}") && !respuestaQuery.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ReporteAnalyticsFiltroDetalleSimboloDTO>>(respuestaQuery);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
