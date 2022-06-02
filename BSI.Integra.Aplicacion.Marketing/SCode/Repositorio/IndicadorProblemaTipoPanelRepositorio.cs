
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
    public class IndicadorProblemaTipoPanelRepositorio : BaseRepository<TIndicadorProblemaTipoPanel, IndicadorProblemaTipoPanelBO>
    {
        #region Metodos Base
        public IndicadorProblemaTipoPanelRepositorio() : base()
        {
        }
        public IndicadorProblemaTipoPanelRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<IndicadorProblemaTipoPanelBO> GetBy(Expression<Func<TIndicadorProblemaTipoPanel, bool>> filter)
        {
            IEnumerable<TIndicadorProblemaTipoPanel> listado = base.GetBy(filter);
            List<IndicadorProblemaTipoPanelBO> listadoBO = new List<IndicadorProblemaTipoPanelBO>();
            foreach (var itemEntidad in listado)
            {
                IndicadorProblemaTipoPanelBO objetoBO = Mapper.Map<TIndicadorProblemaTipoPanel, IndicadorProblemaTipoPanelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IndicadorProblemaTipoPanelBO FirstById(int id)
        {
            try
            {
                TIndicadorProblemaTipoPanel entidad = base.FirstById(id);
                IndicadorProblemaTipoPanelBO objetoBO = new IndicadorProblemaTipoPanelBO();
                Mapper.Map<TIndicadorProblemaTipoPanel, IndicadorProblemaTipoPanelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IndicadorProblemaTipoPanelBO FirstBy(Expression<Func<TIndicadorProblemaTipoPanel, bool>> filter)
        {
            try
            {
                TIndicadorProblemaTipoPanel entidad = base.FirstBy(filter);
                IndicadorProblemaTipoPanelBO objetoBO = Mapper.Map<TIndicadorProblemaTipoPanel, IndicadorProblemaTipoPanelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IndicadorProblemaTipoPanelBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TIndicadorProblemaTipoPanel entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<IndicadorProblemaTipoPanelBO> listadoBO)
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

        public bool Update(IndicadorProblemaTipoPanelBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TIndicadorProblemaTipoPanel entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<IndicadorProblemaTipoPanelBO> listadoBO)
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
        private void AsignacionId(TIndicadorProblemaTipoPanel entidad, IndicadorProblemaTipoPanelBO objetoBO)
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

        private TIndicadorProblemaTipoPanel MapeoEntidad(IndicadorProblemaTipoPanelBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TIndicadorProblemaTipoPanel entidad = new TIndicadorProblemaTipoPanel();
                entidad = Mapper.Map<IndicadorProblemaTipoPanelBO, TIndicadorProblemaTipoPanel>(objetoBO,
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
        /// Obtiene la lista de TipoPanel (activos) dado un Id de IndicadorProblema.
        /// </summary>
        /// <returns></returns>
        public List<IndicadorProblemaTipoPanelDTO> ObtenerTodoIndicadorPorProblema(int IdIndicadorProblema)
        {
            try
            {
                List<IndicadorProblemaTipoPanelDTO> IPTipoPanels = new List<IndicadorProblemaTipoPanelDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdIndicadorProblema, IdHojaOportunidadTipoPanel FROM mkt.T_IndicadorProblemaTipoPanel WHERE IdIndicadorProblema=" + IdIndicadorProblema+" AND Estado = 1";
                var IPTipoPanelBD = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IPTipoPanelBD) && !IPTipoPanelBD.Contains("[]"))
                {
                    IPTipoPanels = JsonConvert.DeserializeObject<List<IndicadorProblemaTipoPanelDTO>>(IPTipoPanelBD);
                }
                return IPTipoPanels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
