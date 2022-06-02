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
    public class CompetidorCapacitacionRepositorio : BaseRepository<TCompetidorCapacitacion, CompetidorCapacitacionBO>
    {
        #region Metodos Base
        public CompetidorCapacitacionRepositorio() : base()
        {
        }
        public CompetidorCapacitacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorCapacitacionBO> GetBy(Expression<Func<TCompetidorCapacitacion, bool>> filter)
        {
            IEnumerable<TCompetidorCapacitacion> listado = base.GetBy(filter);
            List<CompetidorCapacitacionBO> listadoBO = new List<CompetidorCapacitacionBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorCapacitacionBO objetoBO = Mapper.Map<TCompetidorCapacitacion, CompetidorCapacitacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorCapacitacionBO FirstById(int id)
        {
            try
            {
                TCompetidorCapacitacion entidad = base.FirstById(id);
                CompetidorCapacitacionBO objetoBO = new CompetidorCapacitacionBO();
                Mapper.Map<TCompetidorCapacitacion, CompetidorCapacitacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorCapacitacionBO FirstBy(Expression<Func<TCompetidorCapacitacion, bool>> filter)
        {
            try
            {
                TCompetidorCapacitacion entidad = base.FirstBy(filter);
                CompetidorCapacitacionBO objetoBO = Mapper.Map<TCompetidorCapacitacion, CompetidorCapacitacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorCapacitacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidorCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorCapacitacionBO> listadoBO)
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

        public bool Update(CompetidorCapacitacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidorCapacitacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorCapacitacionBO> listadoBO)
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
        private void AsignacionId(TCompetidorCapacitacion entidad, CompetidorCapacitacionBO objetoBO)
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

        private TCompetidorCapacitacion MapeoEntidad(CompetidorCapacitacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidorCapacitacion entidad = new TCompetidorCapacitacion();
                entidad = Mapper.Map<CompetidorCapacitacionBO, TCompetidorCapacitacion>(objetoBO,
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
        /// Obtiene los CompetidorCapacitaciones dado un Id de Competidor
        /// </summary>
        /// <returns></returns>
        public List<CompetidorCapacitacionDTO> ObtenerTodoCompetidorCapacitacionPorIdCompetidor(int IdCompetidor)
        {
            try
            {
                List<CompetidorCapacitacionDTO> ListaCompetidorCapacitacion = new List<CompetidorCapacitacionDTO>();
                var _query = string.Empty;
                _query = "Select Id,  IdCompetidor, IdProgramaCapacitacion FROM pla.T_CompetidorCapacitacion WHERE  Estado = 1 AND IdCompetidor=" + IdCompetidor;
                var _resultado = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_resultado) && !_resultado.Contains("[]"))
                {
                    ListaCompetidorCapacitacion = JsonConvert.DeserializeObject<List<CompetidorCapacitacionDTO>>(_resultado);
                }
                return ListaCompetidorCapacitacion;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
