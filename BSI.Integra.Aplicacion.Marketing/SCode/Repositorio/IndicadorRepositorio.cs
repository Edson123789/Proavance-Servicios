
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class IndicadorRepositorio : BaseRepository<TIndicador, IndicadorBO>
    {
        #region Metodos Base
        public IndicadorRepositorio() : base()
        {
        }
        public IndicadorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndicadorBO> GetBy(Expression<Func<TIndicador, bool>> filter)
        {
            IEnumerable<TIndicador> listado = base.GetBy(filter);
            List<IndicadorBO> listadoBO = new List<IndicadorBO>();
            foreach (var itemEntidad in listado)
            {
                IndicadorBO objetoBO = Mapper.Map<TIndicador, IndicadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndicadorBO FirstById(int id)
        {
            try
            {
                TIndicador entidad = base.FirstById(id);
                IndicadorBO objetoBO = new IndicadorBO();
                Mapper.Map<TIndicador, IndicadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndicadorBO FirstBy(Expression<Func<TIndicador, bool>> filter)
        {
            try
            {
                TIndicador entidad = base.FirstBy(filter);
                IndicadorBO objetoBO = Mapper.Map<TIndicador, IndicadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndicadorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndicador entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndicadorBO> listadoBO)
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

        public bool Update(IndicadorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndicador entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndicadorBO> listadoBO)
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
        private void AsignacionId(TIndicador entidad, IndicadorBO objetoBO)
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

        private TIndicador MapeoEntidad(IndicadorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndicador entidad = new TIndicador();
                entidad = Mapper.Map<IndicadorBO, TIndicador>(objetoBO,
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
        /// Obtiene una lista de Indicadores (activos) para ser usados en comboboxes.
        /// </summary>
        /// <returns></returns>
        public List<IndicadorDTO> ObtenerTodoIndicador()
        {
            try
            {
                List<IndicadorDTO> Indicadores = new List<IndicadorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Meta, Verificacion, IdCategoriaIndicador FROM mkt.T_Indicador WHERE Estado = 1";
                var IndicadoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IndicadoresDB) && !IndicadoresDB.Contains("[]"))
                {
                    Indicadores = JsonConvert.DeserializeObject<List<IndicadorDTO>>(IndicadoresDB);
                }
                return Indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene los datos de Indicadores (activos) para ser usados en una grilla (CRUD propio)
        /// </summary>
        /// <returns></returns>
        public List<IndicadorDTO> ObtenerTodoIndicadores()
        {
            try
            {
                List<IndicadorDTO> Indicadores = new List<IndicadorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Meta, Verificacion, IdCategoriaIndicador FROM mkt.T_Indicador WHERE Estado = 1";
                var IndicadoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IndicadoresDB) && !IndicadoresDB.Contains("[]"))
                {
                    Indicadores = JsonConvert.DeserializeObject<List<IndicadorDTO>>(IndicadoresDB);
                }
                return Indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
