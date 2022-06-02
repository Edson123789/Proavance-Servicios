using AutoMapper;
using BSI.Integra.Aplicacion.Base.DTO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfiguracionFijaRepositorio : BaseRepository<TConfiguracionFija, ConfiguracionFijaBO>
    {
        #region Metodos Base
        public ConfiguracionFijaRepositorio() : base()
        {
        }
        public ConfiguracionFijaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionFijaBO> GetBy(Expression<Func<TConfiguracionFija, bool>> filter)
        {
            IEnumerable<TConfiguracionFija> listado = base.GetBy(filter);
            List<ConfiguracionFijaBO> listadoBO = new List<ConfiguracionFijaBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionFijaBO objetoBO = Mapper.Map<TConfiguracionFija, ConfiguracionFijaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionFijaBO FirstById(int id)
        {
            try
            {
                TConfiguracionFija entidad = base.FirstById(id);
                ConfiguracionFijaBO objetoBO = new ConfiguracionFijaBO();
                Mapper.Map<TConfiguracionFija, ConfiguracionFijaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionFijaBO FirstBy(Expression<Func<TConfiguracionFija, bool>> filter)
        {
            try
            {
                TConfiguracionFija entidad = base.FirstBy(filter);
                ConfiguracionFijaBO objetoBO = Mapper.Map<TConfiguracionFija, ConfiguracionFijaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionFijaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionFija entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionFijaBO> listadoBO)
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

        public bool Update(ConfiguracionFijaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionFija entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionFijaBO> listadoBO)
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
        private void AsignacionId(TConfiguracionFija entidad, ConfiguracionFijaBO objetoBO)
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

        private TConfiguracionFija MapeoEntidad(ConfiguracionFijaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionFija entidad = new TConfiguracionFija();
                entidad = Mapper.Map<ConfiguracionFijaBO, TConfiguracionFija>(objetoBO,
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
        public List<ValorEstaticoDTO> ObtenerTodosLosRegistros()
        {
            List<ValorEstaticoDTO> harcodeos = new List<ValorEstaticoDTO>();
            var _query = string.Empty;
            _query = "SELECT NombreAtributo,Valor,TipoDato FROM conf.V_ConfiguracionFija_Todo Where Estado = 1";
            var _errores = _dapper.QueryDapper(_query, new { });
            harcodeos = JsonConvert.DeserializeObject<List<ValorEstaticoDTO>>(_errores);
            return harcodeos;
        }
    }
}
