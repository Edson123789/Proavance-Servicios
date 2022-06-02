using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CompetidorProgramaRelacionadoRepositorio : BaseRepository<TCompetidorProgramaRelacionado, CompetidorProgramaRelacionadoBO>
    {
        #region Metodos Base
        public CompetidorProgramaRelacionadoRepositorio() : base()
        {
        }
        public CompetidorProgramaRelacionadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorProgramaRelacionadoBO> GetBy(Expression<Func<TCompetidorProgramaRelacionado, bool>> filter)
        {
            IEnumerable<TCompetidorProgramaRelacionado> listado = base.GetBy(filter);
            List<CompetidorProgramaRelacionadoBO> listadoBO = new List<CompetidorProgramaRelacionadoBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorProgramaRelacionadoBO objetoBO = Mapper.Map<TCompetidorProgramaRelacionado, CompetidorProgramaRelacionadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorProgramaRelacionadoBO FirstById(int id)
        {
            try
            {
                TCompetidorProgramaRelacionado entidad = base.FirstById(id);
                CompetidorProgramaRelacionadoBO objetoBO = new CompetidorProgramaRelacionadoBO();
                Mapper.Map<TCompetidorProgramaRelacionado, CompetidorProgramaRelacionadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorProgramaRelacionadoBO FirstBy(Expression<Func<TCompetidorProgramaRelacionado, bool>> filter)
        {
            try
            {
                TCompetidorProgramaRelacionado entidad = base.FirstBy(filter);
                CompetidorProgramaRelacionadoBO objetoBO = Mapper.Map<TCompetidorProgramaRelacionado, CompetidorProgramaRelacionadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorProgramaRelacionadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorProgramaRelacionado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorProgramaRelacionadoBO> listadoBO)
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

        public bool Update(CompetidorProgramaRelacionadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorProgramaRelacionado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorProgramaRelacionadoBO> listadoBO)
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
        private void AsignacionId(TCompetidorProgramaRelacionado entidad, CompetidorProgramaRelacionadoBO objetoBO)
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

        private TCompetidorProgramaRelacionado MapeoEntidad(CompetidorProgramaRelacionadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorProgramaRelacionado entidad = new TCompetidorProgramaRelacionado();
                entidad = Mapper.Map<CompetidorProgramaRelacionadoBO, TCompetidorProgramaRelacionado>(objetoBO,
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
        /// Obtiene los CompetidorProgramaRelacionadoes dado un Id de Competidor
        /// </summary>
        /// <returns></returns>
        public List<CompetidorProgramaRelacionadoDTO> ObtenerTodoCompetidorProgramaRelacionadoPorIdCompetidor(int IdCompetidor)
        {
            try
            {
                List<CompetidorProgramaRelacionadoDTO> ListaCompetidorProgramaRelacionado = new List<CompetidorProgramaRelacionadoDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdCompetidor, IdPrograma FROM pla.T_CompetidorProgramaRelacionado WHERE  Estado = 1 AND IdCompetidor=" + IdCompetidor;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    ListaCompetidorProgramaRelacionado = JsonConvert.DeserializeObject<List<CompetidorProgramaRelacionadoDTO>>(_resultado);
                }
                return ListaCompetidorProgramaRelacionado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
