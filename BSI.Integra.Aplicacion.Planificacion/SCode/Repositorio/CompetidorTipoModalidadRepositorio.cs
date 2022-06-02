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
    public class CompetidorTipoModalidadRepositorio : BaseRepository<TCompetidorTipoModalidad, CompetidorTipoModalidadBO>
    {
        #region Metodos Base
        public CompetidorTipoModalidadRepositorio() : base()
        {
        }
        public CompetidorTipoModalidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorTipoModalidadBO> GetBy(Expression<Func<TCompetidorTipoModalidad, bool>> filter)
        {
            IEnumerable<TCompetidorTipoModalidad> listado = base.GetBy(filter);
            List<CompetidorTipoModalidadBO> listadoBO = new List<CompetidorTipoModalidadBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorTipoModalidadBO objetoBO = Mapper.Map<TCompetidorTipoModalidad, CompetidorTipoModalidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorTipoModalidadBO FirstById(int id)
        {
            try
            {
                TCompetidorTipoModalidad entidad = base.FirstById(id);
                CompetidorTipoModalidadBO objetoBO = new CompetidorTipoModalidadBO();
                Mapper.Map<TCompetidorTipoModalidad, CompetidorTipoModalidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorTipoModalidadBO FirstBy(Expression<Func<TCompetidorTipoModalidad, bool>> filter)
        {
            try
            {
                TCompetidorTipoModalidad entidad = base.FirstBy(filter);
                CompetidorTipoModalidadBO objetoBO = Mapper.Map<TCompetidorTipoModalidad, CompetidorTipoModalidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorTipoModalidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorTipoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorTipoModalidadBO> listadoBO)
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

        public bool Update(CompetidorTipoModalidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorTipoModalidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorTipoModalidadBO> listadoBO)
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
        private void AsignacionId(TCompetidorTipoModalidad entidad, CompetidorTipoModalidadBO objetoBO)
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

        private TCompetidorTipoModalidad MapeoEntidad(CompetidorTipoModalidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorTipoModalidad entidad = new TCompetidorTipoModalidad();
                entidad = Mapper.Map<CompetidorTipoModalidadBO, TCompetidorTipoModalidad>(objetoBO,
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
        /// Obtiene los CompetidorTipoModalidad dado un Id de Competidor
        /// </summary>
        /// <returns></returns>
        public List<CompetidorTipoModalidadDTO> ObtenerTodoCompetidorTipoModalidadPorIdCompetidor(int IdCompetidor)
        {
            try
            {
                List<CompetidorTipoModalidadDTO> ListaCompetidorTipoModalidad = new List<CompetidorTipoModalidadDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdCompetidor, IdTipoModalidad FROM pla.T_CompetidorTipoModalidad WHERE  Estado = 1 AND IdCompetidor=" + IdCompetidor;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    ListaCompetidorTipoModalidad = JsonConvert.DeserializeObject<List<CompetidorTipoModalidadDTO>>(_resultado);
                }
                return ListaCompetidorTipoModalidad;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
