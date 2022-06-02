using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FrecuenciaRepositorio : BaseRepository<TFrecuencia, FrecuenciaBO>
    {
        #region Metodos Base
        public FrecuenciaRepositorio() : base()
        {
        }
        public FrecuenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FrecuenciaBO> GetBy(Expression<Func<TFrecuencia, bool>> filter)
        {
            IEnumerable<TFrecuencia> listado = base.GetBy(filter);
            List<FrecuenciaBO> listadoBO = new List<FrecuenciaBO>();
            foreach (var itemEntidad in listado)
            {
                FrecuenciaBO objetoBO = Mapper.Map<TFrecuencia, FrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FrecuenciaBO FirstById(int id)
        {
            try
            {
                TFrecuencia entidad = base.FirstById(id);
                FrecuenciaBO objetoBO = new FrecuenciaBO();
                Mapper.Map<TFrecuencia, FrecuenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FrecuenciaBO FirstBy(Expression<Func<TFrecuencia, bool>> filter)
        {
            try
            {
                TFrecuencia entidad = base.FirstBy(filter);
                FrecuenciaBO objetoBO = Mapper.Map<TFrecuencia, FrecuenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FrecuenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FrecuenciaBO> listadoBO)
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

        public bool Update(FrecuenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FrecuenciaBO> listadoBO)
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
        private void AsignacionId(TFrecuencia entidad, FrecuenciaBO objetoBO)
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

        private TFrecuencia MapeoEntidad(FrecuenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFrecuencia entidad = new TFrecuencia();
                entidad = Mapper.Map<FrecuenciaBO, TFrecuencia>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<FrecuenciaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TFrecuencia, bool>>> filters, Expression<Func<TFrecuencia, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TFrecuencia> listado = base.GetFiltered(filters, orderBy, ascending);
            List<FrecuenciaBO> listadoBO = new List<FrecuenciaBO>();

            foreach (var itemEntidad in listado)
            {
                FrecuenciaBO objetoBO = Mapper.Map<TFrecuencia, FrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        /// <summary>
        /// Obtiene datos de Frecuencia
        /// </summary>
        /// <param name="idFrecuencia"></param>
        /// <returns></returns>
        public DatosFrecuenciaGeneralDTO ObtenerFrecuencia(int idFrecuencia)
        {
            try
            {
                string _queryFrecuencia = "Select Id,Nombre,NumDias From pla.V_TFrecuenciaPorId Where Estado=1 and Id=@IdFrecuencia";
                var queryFrecuencia = _dapper.FirstOrDefault(_queryFrecuencia, new { IdFrecuencia = idFrecuencia });
                return JsonConvert.DeserializeObject<DatosFrecuenciaGeneralDTO>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// Obtiene Lista de frecuencia
        /// </summary>
        public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuencia()
        {
            try
            {
                string _queryFrecuencia = "Select Id,Nombre From pla.V_TFrecuenciaPorId Where Estado=1 and Id in (3,5,6,7)";
                var queryFrecuencia = _dapper.QueryDapper(_queryFrecuencia,null);
                return JsonConvert.DeserializeObject<List<DatosFrecuenciaGeneralDTO>>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
		/// <summary>
		/// Obtiene Lista de frecuencia
		/// </summary>
		public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaPlanificacion()
		{
			try
			{
				string _queryFrecuencia = "Select Id,Nombre From pla.V_TFrecuenciaPorId Where Estado=1";
				var queryFrecuencia = _dapper.QueryDapper(_queryFrecuencia, null);
				return JsonConvert.DeserializeObject<List<DatosFrecuenciaGeneralDTO>>(queryFrecuencia);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
		/// <summary>
		/// Obtiene Lista de frecuencia para Actividades
		/// </summary>
		public List<DatosFrecuenciaGeneralDTO> ObtenerListaFrecuenciaActividad()
        {
            try
            {
                string _queryFrecuencia = "Select Id,Nombre From pla.V_TFrecuenciaPorId Where Estado=1 and Id in (1,3,5)";
                var queryFrecuencia = _dapper.QueryDapper(_queryFrecuencia,null);
                return JsonConvert.DeserializeObject<List<DatosFrecuenciaGeneralDTO>>(queryFrecuencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
