
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class IndicadorFrecuenciaRepositorio : BaseRepository<TIndicadorFrecuencia, IndicadorFrecuenciaBO>
    {
        #region Metodos Base
        public IndicadorFrecuenciaRepositorio() : base()
        {
        }
        public IndicadorFrecuenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndicadorFrecuenciaBO> GetBy(Expression<Func<TIndicadorFrecuencia, bool>> filter)
        {
            IEnumerable<TIndicadorFrecuencia> listado = base.GetBy(filter);
            List<IndicadorFrecuenciaBO> listadoBO = new List<IndicadorFrecuenciaBO>();
            foreach (var itemEntidad in listado)
            {
                IndicadorFrecuenciaBO objetoBO = Mapper.Map<TIndicadorFrecuencia, IndicadorFrecuenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndicadorFrecuenciaBO FirstById(int id)
        {
            try
            {
                TIndicadorFrecuencia entidad = base.FirstById(id);
                IndicadorFrecuenciaBO objetoBO = new IndicadorFrecuenciaBO();
                Mapper.Map<TIndicadorFrecuencia, IndicadorFrecuenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndicadorFrecuenciaBO FirstBy(Expression<Func<TIndicadorFrecuencia, bool>> filter)
        {
            try
            {
                TIndicadorFrecuencia entidad = base.FirstBy(filter);
                IndicadorFrecuenciaBO objetoBO = Mapper.Map<TIndicadorFrecuencia, IndicadorFrecuenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndicadorFrecuenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndicadorFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndicadorFrecuenciaBO> listadoBO)
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

        public bool Update(IndicadorFrecuenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndicadorFrecuencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndicadorFrecuenciaBO> listadoBO)
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
        private void AsignacionId(TIndicadorFrecuencia entidad, IndicadorFrecuenciaBO objetoBO)
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

        private TIndicadorFrecuencia MapeoEntidad(IndicadorFrecuenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndicadorFrecuencia entidad = new TIndicadorFrecuencia();
                entidad = Mapper.Map<IndicadorFrecuenciaBO, TIndicadorFrecuencia>(objetoBO,
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
        /// Obtiene una lista de IndicadorFrecuencia dado un Id de Indicador problema.
        /// </summary>
        /// <returns></returns>
        public List<IndicadorFrecuenciaDTO> ObtenerFrecuenciasPorIdIndicadorProblema(int IdIndicadorProblema)
        {
            try
            {
                List<IndicadorFrecuenciaDTO> Indicadores = new List<IndicadorFrecuenciaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdIndicadorProblema, IdHora FROM mkt.T_IndicadorFrecuencia WHERE IdIndicadorProblema=" + IdIndicadorProblema + "AND Estado = 1";
                var frecuenciasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(frecuenciasDB) && !frecuenciasDB.Contains("[]"))
                {
                    Indicadores = JsonConvert.DeserializeObject<List<IndicadorFrecuenciaDTO>>(frecuenciasDB);
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
