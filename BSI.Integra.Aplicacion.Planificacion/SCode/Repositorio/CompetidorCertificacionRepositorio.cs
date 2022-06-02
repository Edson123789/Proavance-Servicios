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
    public class CompetidorCertificacionRepositorio : BaseRepository<TCompetidorCertificacion, CompetidorCertificacionBO>
    {
        #region Metodos Base
        public CompetidorCertificacionRepositorio() : base()
        {
        }
        public CompetidorCertificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorCertificacionBO> GetBy(Expression<Func<TCompetidorCertificacion, bool>> filter)
        {
            IEnumerable<TCompetidorCertificacion> listado = base.GetBy(filter);
            List<CompetidorCertificacionBO> listadoBO = new List<CompetidorCertificacionBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorCertificacionBO objetoBO = Mapper.Map<TCompetidorCertificacion, CompetidorCertificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorCertificacionBO FirstById(int id)
        {
            try
            {
                TCompetidorCertificacion entidad = base.FirstById(id);
                CompetidorCertificacionBO objetoBO = new CompetidorCertificacionBO();
                Mapper.Map<TCompetidorCertificacion, CompetidorCertificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorCertificacionBO FirstBy(Expression<Func<TCompetidorCertificacion, bool>> filter)
        {
            try
            {
                TCompetidorCertificacion entidad = base.FirstBy(filter);
                CompetidorCertificacionBO objetoBO = Mapper.Map<TCompetidorCertificacion, CompetidorCertificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorCertificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorCertificacionBO> listadoBO)
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

        public bool Update(CompetidorCertificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorCertificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorCertificacionBO> listadoBO)
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
        private void AsignacionId(TCompetidorCertificacion entidad, CompetidorCertificacionBO objetoBO)
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

        private TCompetidorCertificacion MapeoEntidad(CompetidorCertificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorCertificacion entidad = new TCompetidorCertificacion();
                entidad = Mapper.Map<CompetidorCertificacionBO, TCompetidorCertificacion>(objetoBO,
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
        /// Obtiene los CompetidorCertificaciones dado un Id de Competidor
        /// </summary>
        /// <returns></returns>
        public List<CompetidorCertificacionDTO> ObtenerTodoCompetidorCertificacionPorIdCompetidor(int IdCompetidor)
        {
            try
            {
                List<CompetidorCertificacionDTO> ListaCompetidorCertificacion = new List<CompetidorCertificacionDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdCompetidor, IdCertificacion FROM pla.T_CompetidorCertificacion WHERE  Estado = 1 AND IdCompetidor="+IdCompetidor;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    ListaCompetidorCertificacion = JsonConvert.DeserializeObject<List<CompetidorCertificacionDTO>>(_resultado);
                }
                return ListaCompetidorCertificacion;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
