
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
    public class ProblemaHorarioRepositorio : BaseRepository<TProblemaHorario, ProblemaHorarioBO>
    {
        #region Metodos Base
        public ProblemaHorarioRepositorio() : base()
        {
        }
        public ProblemaHorarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProblemaHorarioBO> GetBy(Expression<Func<TProblemaHorario, bool>> filter)
        {
            IEnumerable<TProblemaHorario> listado = base.GetBy(filter);
            List<ProblemaHorarioBO> listadoBO = new List<ProblemaHorarioBO>();
            foreach (var itemEntidad in listado)
            {
                ProblemaHorarioBO objetoBO = Mapper.Map<TProblemaHorario, ProblemaHorarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProblemaHorarioBO FirstById(int id)
        {
            try
            {
                TProblemaHorario entidad = base.FirstById(id);
                ProblemaHorarioBO objetoBO = new ProblemaHorarioBO();
                Mapper.Map<TProblemaHorario, ProblemaHorarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProblemaHorarioBO FirstBy(Expression<Func<TProblemaHorario, bool>> filter)
        {
            try
            {
                TProblemaHorario entidad = base.FirstBy(filter);
                ProblemaHorarioBO objetoBO = Mapper.Map<TProblemaHorario, ProblemaHorarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProblemaHorarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProblemaHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProblemaHorarioBO> listadoBO)
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

        public bool Update(ProblemaHorarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProblemaHorario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProblemaHorarioBO> listadoBO)
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
        private void AsignacionId(TProblemaHorario entidad, ProblemaHorarioBO objetoBO)
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

        private TProblemaHorario MapeoEntidad(ProblemaHorarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProblemaHorario entidad = new TProblemaHorario();
                entidad = Mapper.Map<ProblemaHorarioBO, TProblemaHorario>(objetoBO,
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
        /// Obtiene la lista de ProblemaHorario (activos) dado un Id de Problema.
        /// </summary>
        /// <returns></returns>
        public List<ProblemaHorarioDTO> ObtenerTodoHorarioPorIdProblema(int IdProblema)
        {
            try
            {
                List<ProblemaHorarioDTO> IndicadorProblemas = new List<ProblemaHorarioDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdProblema, IdHora FROM mkt.T_ProblemaHorario WHERE IdProblema=" + IdProblema + " AND Estado = 1";
                var IndicadorProblemasBD = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IndicadorProblemasBD) && !IndicadorProblemasBD.Contains("[]"))
                {
                    IndicadorProblemas = JsonConvert.DeserializeObject<List<ProblemaHorarioDTO>>(IndicadorProblemasBD);
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
