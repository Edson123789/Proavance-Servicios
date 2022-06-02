
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
    public class HojaOportunidadTipoPanelRepositorio : BaseRepository<THojaOportunidadTipoPanel, HojaOportunidadTipoPanelBO>
    {
        #region Metodos Base
        public HojaOportunidadTipoPanelRepositorio() : base()
        {
        }
        public HojaOportunidadTipoPanelRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HojaOportunidadTipoPanelBO> GetBy(Expression<Func<THojaOportunidadTipoPanel, bool>> filter)
        {
            IEnumerable<THojaOportunidadTipoPanel> listado = base.GetBy(filter);
            List<HojaOportunidadTipoPanelBO> listadoBO = new List<HojaOportunidadTipoPanelBO>();
            foreach (var itemEntidad in listado)
            {
                HojaOportunidadTipoPanelBO objetoBO = Mapper.Map<THojaOportunidadTipoPanel, HojaOportunidadTipoPanelBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HojaOportunidadTipoPanelBO FirstById(int id)
        {
            try
            {
                THojaOportunidadTipoPanel entidad = base.FirstById(id);
                HojaOportunidadTipoPanelBO objetoBO = new HojaOportunidadTipoPanelBO();
                Mapper.Map<THojaOportunidadTipoPanel, HojaOportunidadTipoPanelBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HojaOportunidadTipoPanelBO FirstBy(Expression<Func<THojaOportunidadTipoPanel, bool>> filter)
        {
            try
            {
                THojaOportunidadTipoPanel entidad = base.FirstBy(filter);
                HojaOportunidadTipoPanelBO objetoBO = Mapper.Map<THojaOportunidadTipoPanel, HojaOportunidadTipoPanelBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HojaOportunidadTipoPanelBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THojaOportunidadTipoPanel entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HojaOportunidadTipoPanelBO> listadoBO)
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

        public bool Update(HojaOportunidadTipoPanelBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THojaOportunidadTipoPanel entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HojaOportunidadTipoPanelBO> listadoBO)
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
        private void AsignacionId(THojaOportunidadTipoPanel entidad, HojaOportunidadTipoPanelBO objetoBO)
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

        private THojaOportunidadTipoPanel MapeoEntidad(HojaOportunidadTipoPanelBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THojaOportunidadTipoPanel entidad = new THojaOportunidadTipoPanel();
                entidad = Mapper.Map<HojaOportunidadTipoPanelBO, THojaOportunidadTipoPanel>(objetoBO,
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
        /// Obtiene una lista de TipoPanel para llenar en comboboxes
        /// </summary>
        /// <returns></returns>
        public List<HojaOportunidadTipoPanelDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<HojaOportunidadTipoPanelDTO> Paneles = new List<HojaOportunidadTipoPanelDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM mkt.T_HojaOportunidadTipoPanel WHERE Estado = 1";
                var panelesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(panelesDB) && !panelesDB.Contains("[]"))
                {
                    Paneles = JsonConvert.DeserializeObject<List<HojaOportunidadTipoPanelDTO>>(panelesDB);
                }
                return Paneles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
