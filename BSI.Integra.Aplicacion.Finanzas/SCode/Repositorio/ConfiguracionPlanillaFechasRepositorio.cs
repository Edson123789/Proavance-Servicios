using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class ConfiguracionPlanillaFechasRepositorio : BaseRepository<TConfiguracionPlanillaFechas, ConfiguracionPlanillaFechasBO>
    {
        #region Metodos Base
        public ConfiguracionPlanillaFechasRepositorio() : base()
        {
        }
        public ConfiguracionPlanillaFechasRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionPlanillaFechasBO> GetBy(Expression<Func<TConfiguracionPlanillaFechas, bool>> filter)
        {
            IEnumerable<TConfiguracionPlanillaFechas> listado = base.GetBy(filter);
            List<ConfiguracionPlanillaFechasBO> listadoBO = new List<ConfiguracionPlanillaFechasBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionPlanillaFechasBO objetoBO = Mapper.Map<TConfiguracionPlanillaFechas, ConfiguracionPlanillaFechasBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionPlanillaFechasBO FirstById(int id)
        {
            try
            {
                TConfiguracionPlanillaFechas entidad = base.FirstById(id);
                ConfiguracionPlanillaFechasBO objetoBO = new ConfiguracionPlanillaFechasBO();
                Mapper.Map<TConfiguracionPlanillaFechas, ConfiguracionPlanillaFechasBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionPlanillaFechasBO FirstBy(Expression<Func<TConfiguracionPlanillaFechas, bool>> filter)
        {
            try
            {
                TConfiguracionPlanillaFechas entidad = base.FirstBy(filter);
                ConfiguracionPlanillaFechasBO objetoBO = Mapper.Map<TConfiguracionPlanillaFechas, ConfiguracionPlanillaFechasBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionPlanillaFechasBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionPlanillaFechas entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionPlanillaFechasBO> listadoBO)
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

        public bool Update(ConfiguracionPlanillaFechasBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionPlanillaFechas entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionPlanillaFechasBO> listadoBO)
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
        private void AsignacionId(TConfiguracionPlanillaFechas entidad, ConfiguracionPlanillaFechasBO objetoBO)
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

        private TConfiguracionPlanillaFechas MapeoEntidad(ConfiguracionPlanillaFechasBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionPlanillaFechas entidad = new TConfiguracionPlanillaFechas();
                entidad = Mapper.Map<ConfiguracionPlanillaFechasBO, TConfiguracionPlanillaFechas>(objetoBO,
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

        public List<int> ObtenerIdsTodoConfiguracionPlanillaFechas(int idConfiguracionPlanilla)
        {
            try
            {
                var lista = GetBy(x => x.IdConfiguracionPlanilla == idConfiguracionPlanilla, y => new ConfiguracionPlanillaFechasDTO
                {
                    Id = y.Id,
                }).OrderByDescending(x => x.Id).Select(x=>x.Id).ToList();

                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<ConfiguracionPlanillaFechasDTO> ObtenerFechasConfiguracion(int Id)
        {
            try
            {
                var lista = GetBy(x => x.IdConfiguracionPlanilla==Id, y => new ConfiguracionPlanillaFechasDTO
                {
                    Id = y.Id,
                    FechaProceso = y.FechaProceso,
                    CalculoReal = y.CalculoReal,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
