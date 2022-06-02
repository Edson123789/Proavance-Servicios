
using System;
using System.Collections.Generic;
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
    public class ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio : BaseRepository<TConfiguracionRutinaBncObsoletoCategoriaOrigen, ConfiguracionRutinaBncObsoletoCategoriaOrigenBO>
    {
        #region Metodos Base
        public ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio() : base()
        {
        }
        public ConfiguracionRutinaBncObsoletoCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO> GetBy(Expression<Func<TConfiguracionRutinaBncObsoletoCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TConfiguracionRutinaBncObsoletoCategoriaOrigen> listado = base.GetBy(filter);
            List<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO> listadoBO = new List<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoletoCategoriaOrigen, ConfiguracionRutinaBncObsoletoCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionRutinaBncObsoletoCategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad = base.FirstById(id);
                ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO = new ConfiguracionRutinaBncObsoletoCategoriaOrigenBO();
                Mapper.Map<TConfiguracionRutinaBncObsoletoCategoriaOrigen, ConfiguracionRutinaBncObsoletoCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionRutinaBncObsoletoCategoriaOrigenBO FirstBy(Expression<Func<TConfiguracionRutinaBncObsoletoCategoriaOrigen, bool>> filter)
        {
            try
            {
                TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad = base.FirstBy(filter);
                ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoletoCategoriaOrigen, ConfiguracionRutinaBncObsoletoCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO> listadoBO)
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

        public bool Update(ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad, ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO)
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

        private TConfiguracionRutinaBncObsoletoCategoriaOrigen MapeoEntidad(ConfiguracionRutinaBncObsoletoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionRutinaBncObsoletoCategoriaOrigen entidad = new TConfiguracionRutinaBncObsoletoCategoriaOrigen();
                entidad = Mapper.Map<ConfiguracionRutinaBncObsoletoCategoriaOrigenBO, TConfiguracionRutinaBncObsoletoCategoriaOrigen>(objetoBO,
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


        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los CategoriaOrigens Asociados a una ConfiguracionBnc
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns>
        public List<ConfiguracionRutinaBncObsoletoCategoriaOrigenDTO> ObtenerCategoriaOrigenPorIdConfiguracionBnc(int IdConfiguracionRutinaBncObsoleto)
        {
            try
            {
                List<ConfiguracionRutinaBncObsoletoCategoriaOrigenDTO> Lista = new List<ConfiguracionRutinaBncObsoletoCategoriaOrigenDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdConfiguracionRutinaBncObsoleto, IdCategoriaOrigen FROM mkt.T_ConfiguracionRutinaBncObsoletoCategoriaOrigen WHERE Estado = 1 AND IdConfiguracionRutinaBncObsoleto=@IdConfiguracionRutinaBncObsoleto";
                var resultadosDB = _dapper.QueryDapper(_query, new { IdConfiguracionRutinaBncObsoleto = IdConfiguracionRutinaBncObsoleto });
                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    Lista = JsonConvert.DeserializeObject<List<ConfiguracionRutinaBncObsoletoCategoriaOrigenDTO>>(resultadosDB);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}