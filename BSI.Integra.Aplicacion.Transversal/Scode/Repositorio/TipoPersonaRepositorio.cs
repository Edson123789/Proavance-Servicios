using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoPersonaRepositorio : BaseRepository<TTipoPersona, TipoPersonaBO>
    {
        #region Metodos Base
        public TipoPersonaRepositorio() : base()
        {
        }
        public TipoPersonaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoPersonaBO> GetBy(Expression<Func<TTipoPersona, bool>> filter)
        {
            IEnumerable<TTipoPersona> listado = base.GetBy(filter);
            List<TipoPersonaBO> listadoBO = new List<TipoPersonaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoPersonaBO objetoBO = Mapper.Map<TTipoPersona, TipoPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoPersonaBO FirstById(int id)
        {
            try
            {
                TTipoPersona entidad = base.FirstById(id);
                TipoPersonaBO objetoBO = new TipoPersonaBO();
                Mapper.Map<TTipoPersona, TipoPersonaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoPersonaBO FirstBy(Expression<Func<TTipoPersona, bool>> filter)
        {
            try
            {
                TTipoPersona entidad = base.FirstBy(filter);
                TipoPersonaBO objetoBO = Mapper.Map<TTipoPersona, TipoPersonaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoPersonaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoPersonaBO> listadoBO)
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

        public bool Update(TipoPersonaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoPersonaBO> listadoBO)
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
        private void AsignacionId(TTipoPersona entidad, TipoPersonaBO objetoBO)
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

        private TTipoPersona MapeoEntidad(TipoPersonaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPersona entidad = new TTipoPersona();
                entidad = Mapper.Map<TipoPersonaBO, TTipoPersona>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoPersonaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoPersona, bool>>> filters, Expression<Func<TTipoPersona, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoPersona> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoPersonaBO> listadoBO = new List<TipoPersonaBO>();

            foreach (var itemEntidad in listado)
            {
                TipoPersonaBO objetoBO = Mapper.Map<TTipoPersona, TipoPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene el id y nombre de TipoPersona para un combo
        /// </summary>
        /// <returns></returns>
        public List<TipoPersonaFiltroDTO> ObtenerListaTipoPersona()
        {
            try
            {
                return this.GetBy(x => true, y => new TipoPersonaFiltroDTO { Id = y.Id, Nombre = y.Nombre, }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los tipo de materiales
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<FiltroDTO> ObtenerFiltroCriterioEvaluacion()
        {
            try
            {
                List<FiltroDTO> tipoPersona = new List<FiltroDTO>();
                string _queryPersona = string.Empty;
                _queryPersona = "SELECT Id,Nombre FROM pla.V_TipoPersona_Filtro WHERE Estado=1";
                var queryModalidad = _dapper.QueryDapper(_queryPersona, null);
                if (!string.IsNullOrEmpty(queryModalidad) && !queryModalidad.Contains("[]"))
                {
                    tipoPersona = JsonConvert.DeserializeObject<List<FiltroDTO>>(queryModalidad);
                }
                return tipoPersona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
