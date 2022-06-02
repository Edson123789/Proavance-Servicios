using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class ActividadBaseRepositorio : BaseRepository<TActividadBase, ActividadBaseBO>
    {
        #region Metodos Base
        public ActividadBaseRepositorio() : base()
        {
        }
        public ActividadBaseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ActividadBaseBO> GetBy(Expression<Func<TActividadBase, bool>> filter)
        {
            IEnumerable<TActividadBase> listado = base.GetBy(filter);
            List<ActividadBaseBO> listadoBO = new List<ActividadBaseBO>();
            foreach (var itemEntidad in listado)
            {
                ActividadBaseBO objetoBO = Mapper.Map<TActividadBase, ActividadBaseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ActividadBaseBO FirstById(int id)
        {
            try
            {
                TActividadBase entidad = base.FirstById(id);
                ActividadBaseBO objetoBO = new ActividadBaseBO();
                Mapper.Map<TActividadBase, ActividadBaseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadBaseBO FirstBy(Expression<Func<TActividadBase, bool>> filter)
        {
            try
            {
                TActividadBase entidad = base.FirstBy(filter);
                ActividadBaseBO objetoBO = Mapper.Map<TActividadBase, ActividadBaseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ActividadBaseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TActividadBase entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ActividadBaseBO> listadoBO)
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

        public bool Update(ActividadBaseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TActividadBase entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ActividadBaseBO> listadoBO)
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
        private void AsignacionId(TActividadBase entidad, ActividadBaseBO objetoBO)
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

        private TActividadBase MapeoEntidad(ActividadBaseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TActividadBase entidad = new TActividadBase();
                entidad = Mapper.Map<ActividadBaseBO, TActividadBase>(objetoBO,
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
        /// Obtiene una lista con Id y Nombre de ActividadRepositorio (para llenado de combobox)
        /// </summary>
        /// <returns>Todas las actividades base permitidas para la visualizacion del usuario</returns>
        public List<ActividadBaseDTO> ObtenerActividadesBase()
        {
            try
            { 
                string query = "SELECT Id, Nombre FROM mkt.V_TActividadBase_ObtenerParaCombo WHERE Id=1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<ActividadBaseDTO> listaActividadBase = JsonConvert.DeserializeObject<List<ActividadBaseDTO>>(responseQuery);
                return listaActividadBase;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 18/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de actividades base para actividades automaticas
        /// </summary>
        /// <returns>List<ActividadBaseDTO></returns>
        public List<ActividadBaseDTO> ObtenerActividadesBaseMasivo()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM mkt.V_TActividadBase_ObtenerParaCombo WHERE Id!=1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<ActividadBaseDTO> listaActividadBase = JsonConvert.DeserializeObject<List<ActividadBaseDTO>>(responseQuery);
                return listaActividadBase;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
