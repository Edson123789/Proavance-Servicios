
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
    public class IndicadorProblemaRepositorio : BaseRepository<TIndicadorProblema, IndicadorProblemaBO>
    {
        #region Metodos Base
        public IndicadorProblemaRepositorio() : base()
        {
        }
        public IndicadorProblemaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndicadorProblemaBO> GetBy(Expression<Func<TIndicadorProblema, bool>> filter)
        {
            IEnumerable<TIndicadorProblema> listado = base.GetBy(filter);
            List<IndicadorProblemaBO> listadoBO = new List<IndicadorProblemaBO>();
            foreach (var itemEntidad in listado)
            {
                IndicadorProblemaBO objetoBO = Mapper.Map<TIndicadorProblema, IndicadorProblemaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndicadorProblemaBO FirstById(int id)
        {
            try
            {
                TIndicadorProblema entidad = base.FirstById(id);
                IndicadorProblemaBO objetoBO = new IndicadorProblemaBO();
                Mapper.Map<TIndicadorProblema, IndicadorProblemaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndicadorProblemaBO FirstBy(Expression<Func<TIndicadorProblema, bool>> filter)
        {
            try
            {
                TIndicadorProblema entidad = base.FirstBy(filter);
                IndicadorProblemaBO objetoBO = Mapper.Map<TIndicadorProblema, IndicadorProblemaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndicadorProblemaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndicadorProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndicadorProblemaBO> listadoBO)
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

        public bool Update(IndicadorProblemaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndicadorProblema entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndicadorProblemaBO> listadoBO)
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
        private void AsignacionId(TIndicadorProblema entidad, IndicadorProblemaBO objetoBO)
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

        private TIndicadorProblema MapeoEntidad(IndicadorProblemaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndicadorProblema entidad = new TIndicadorProblema();
                entidad = Mapper.Map<IndicadorProblemaBO, TIndicadorProblema>(objetoBO,
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
        /// Obtiene la lista de IndicadorProblemas (activos) registradas en el sistema dado un Id de Problema.
        /// </summary>
        /// <returns></returns>
        public List<IndicadorProblemaDTO> ObtenerTodoIndicadorProblemaPorIdProblema(int IdProblema)
        {
            try
            {
                List<IndicadorProblemaDTO> IndicadorProblemas = new List<IndicadorProblemaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdProblema, IdIndicador, IdOperadorComparacion, Valor, MuestraMinima FROM mkt.T_IndicadorProblema WHERE IdProblema=" + IdProblema+" AND Estado = 1";
                var IndicadorProblemasBD = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IndicadorProblemasBD) && !IndicadorProblemasBD.Contains("[]"))
                {
                    IndicadorProblemas = JsonConvert.DeserializeObject<List<IndicadorProblemaDTO>>(IndicadorProblemasBD);
                }
                return IndicadorProblemas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
