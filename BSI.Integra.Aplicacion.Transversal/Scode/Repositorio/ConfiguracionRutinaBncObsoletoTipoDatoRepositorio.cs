
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
    public class ConfiguracionRutinaBncObsoletoTipoDatoRepositorio : BaseRepository<TConfiguracionRutinaBncObsoletoTipoDato, ConfiguracionRutinaBncObsoletoTipoDatoBO>
    {
        #region Metodos Base
        public ConfiguracionRutinaBncObsoletoTipoDatoRepositorio() : base()
        {
        }
        public ConfiguracionRutinaBncObsoletoTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionRutinaBncObsoletoTipoDatoBO> GetBy(Expression<Func<TConfiguracionRutinaBncObsoletoTipoDato, bool>> filter)
        {
            IEnumerable<TConfiguracionRutinaBncObsoletoTipoDato> listado = base.GetBy(filter);
            List<ConfiguracionRutinaBncObsoletoTipoDatoBO> listadoBO = new List<ConfiguracionRutinaBncObsoletoTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoletoTipoDato, ConfiguracionRutinaBncObsoletoTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionRutinaBncObsoletoTipoDatoBO FirstById(int id)
        {
            try
            {
                TConfiguracionRutinaBncObsoletoTipoDato entidad = base.FirstById(id);
                ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO = new ConfiguracionRutinaBncObsoletoTipoDatoBO();
                Mapper.Map<TConfiguracionRutinaBncObsoletoTipoDato, ConfiguracionRutinaBncObsoletoTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionRutinaBncObsoletoTipoDatoBO FirstBy(Expression<Func<TConfiguracionRutinaBncObsoletoTipoDato, bool>> filter)
        {
            try
            {
                TConfiguracionRutinaBncObsoletoTipoDato entidad = base.FirstBy(filter);
                ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoletoTipoDato, ConfiguracionRutinaBncObsoletoTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionRutinaBncObsoletoTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionRutinaBncObsoletoTipoDatoBO> listadoBO)
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

        public bool Update(ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionRutinaBncObsoletoTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionRutinaBncObsoletoTipoDatoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionRutinaBncObsoletoTipoDato entidad, ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO)
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

        private TConfiguracionRutinaBncObsoletoTipoDato MapeoEntidad(ConfiguracionRutinaBncObsoletoTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionRutinaBncObsoletoTipoDato entidad = new TConfiguracionRutinaBncObsoletoTipoDato();
                entidad = Mapper.Map<ConfiguracionRutinaBncObsoletoTipoDatoBO, TConfiguracionRutinaBncObsoletoTipoDato>(objetoBO,
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
        /// Obtiene los TipoDatos Asociados a una ConfiguracionBnc
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns>
        public List<ConfiguracionRutinaBncObsoletoTipoDatoDTO> ObtenerTipoDatoPorIdConfiguracionBnc(int IdConfiguracionRutinaBncObsoleto)
        {
            try
            {
                List<ConfiguracionRutinaBncObsoletoTipoDatoDTO> Lista = new List<ConfiguracionRutinaBncObsoletoTipoDatoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdConfiguracionRutinaBncObsoleto, IdTipoDato FROM mkt.T_ConfiguracionRutinaBncObsoletoTipoDato WHERE Estado = 1 AND IdConfiguracionRutinaBncObsoleto=@IdConfiguracionRutinaBncObsoleto";
                var resultadosDB = _dapper.QueryDapper(_query, new { IdConfiguracionRutinaBncObsoleto = IdConfiguracionRutinaBncObsoleto });
                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    Lista = JsonConvert.DeserializeObject<List<ConfiguracionRutinaBncObsoletoTipoDatoDTO>>(resultadosDB);
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