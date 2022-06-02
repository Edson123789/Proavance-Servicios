using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class LogFiltroSegmentoEjecutadoRepositorio : BaseRepository<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutadoBO>
    {
        #region Metodos Base
        public LogFiltroSegmentoEjecutadoRepositorio() : base()
        {
        }
        public LogFiltroSegmentoEjecutadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LogFiltroSegmentoEjecutadoBO> GetBy(Expression<Func<TLogFiltroSegmentoEjecutado, bool>> filter)
        {
            IEnumerable<TLogFiltroSegmentoEjecutado> listado = base.GetBy(filter);
            List<LogFiltroSegmentoEjecutadoBO> listadoBO = new List<LogFiltroSegmentoEjecutadoBO>();
            foreach (var itemEntidad in listado)
            {
                LogFiltroSegmentoEjecutadoBO objetoBO = Mapper.Map<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LogFiltroSegmentoEjecutadoBO FirstById(int id)
        {
            try
            {
                TLogFiltroSegmentoEjecutado entidad = base.FirstById(id);
                LogFiltroSegmentoEjecutadoBO objetoBO = new LogFiltroSegmentoEjecutadoBO();
                Mapper.Map<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LogFiltroSegmentoEjecutadoBO FirstBy(Expression<Func<TLogFiltroSegmentoEjecutado, bool>> filter)
        {
            try
            {
                TLogFiltroSegmentoEjecutado entidad = base.FirstBy(filter);
                LogFiltroSegmentoEjecutadoBO objetoBO = Mapper.Map<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LogFiltroSegmentoEjecutadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLogFiltroSegmentoEjecutado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LogFiltroSegmentoEjecutadoBO> listadoBO)
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

        public bool Update(LogFiltroSegmentoEjecutadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLogFiltroSegmentoEjecutado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LogFiltroSegmentoEjecutadoBO> listadoBO)
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
        private void AsignacionId(TLogFiltroSegmentoEjecutado entidad, LogFiltroSegmentoEjecutadoBO objetoBO)
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

        private TLogFiltroSegmentoEjecutado MapeoEntidad(LogFiltroSegmentoEjecutadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLogFiltroSegmentoEjecutado entidad = new TLogFiltroSegmentoEjecutado();
                entidad = Mapper.Map<LogFiltroSegmentoEjecutadoBO, TLogFiltroSegmentoEjecutado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<LogFiltroSegmentoEjecutadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TLogFiltroSegmentoEjecutado, bool>>> filters, Expression<Func<TLogFiltroSegmentoEjecutado, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TLogFiltroSegmentoEjecutado> listado = base.GetFiltered(filters, orderBy, ascending);
            List<LogFiltroSegmentoEjecutadoBO> listadoBO = new List<LogFiltroSegmentoEjecutadoBO>();

            foreach (var itemEntidad in listado)
            {
                LogFiltroSegmentoEjecutadoBO objetoBO = Mapper.Map<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los log de filtro segmento ejecutado por filtro segmento
        /// </summary>
        /// <param name="idFiltroSegmento"></param>
        /// <returns></returns>
        public List<LogFiltroSegmentoEjecutadoDTO> ObtenerPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<LogFiltroSegmentoEjecutadoDTO> listadoFiltroSegmentoEjecutado = new List<LogFiltroSegmentoEjecutadoDTO>();
                var query = "SELECT Id, NombreCentroCosto, NombreOrigen, NombreTipoDato, NombreFaseOportunidad, TotalOportunidadesCreadas, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion FROM mkt.V_ObtenerLogFiltroSegmentoEjecutado WHERE IdFiltroSegmento = @idFiltroSegmento AND EstadoLogFiltroSegmentoEjecutado = 1;";
                var listadoFiltroSegmentoEjecutadoDB = _dapper.QueryDapper(query, new { idFiltroSegmento });
                if (!string.IsNullOrEmpty(listadoFiltroSegmentoEjecutadoDB) && !listadoFiltroSegmentoEjecutadoDB.Contains("[]"))
                {
                    listadoFiltroSegmentoEjecutado = JsonConvert.DeserializeObject<List<LogFiltroSegmentoEjecutadoDTO>>(listadoFiltroSegmentoEjecutadoDB);
                }
                return listadoFiltroSegmentoEjecutado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
