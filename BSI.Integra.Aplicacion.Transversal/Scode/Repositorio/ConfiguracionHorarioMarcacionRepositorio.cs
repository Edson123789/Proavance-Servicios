using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class ConfiguracionHorarioMarcacionRepositorio : BaseRepository<TConfiguracionHorarioMarcacion, ConfiguracionHorarioMarcacionBO>
    {
        #region Metodos Base
        public ConfiguracionHorarioMarcacionRepositorio() : base()
        {
        }
        public ConfiguracionHorarioMarcacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionHorarioMarcacionBO> GetBy(Expression<Func<TConfiguracionHorarioMarcacion, bool>> filter)
        {
            IEnumerable<TConfiguracionHorarioMarcacion> listado = base.GetBy(filter);
            List<ConfiguracionHorarioMarcacionBO> listadoBO = new List<ConfiguracionHorarioMarcacionBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionHorarioMarcacionBO objetoBO = Mapper.Map<TConfiguracionHorarioMarcacion, ConfiguracionHorarioMarcacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionHorarioMarcacionBO FirstById(int id)
        {
            try
            {
                TConfiguracionHorarioMarcacion entidad = base.FirstById(id);
                ConfiguracionHorarioMarcacionBO objetoBO = new ConfiguracionHorarioMarcacionBO();
                Mapper.Map<TConfiguracionHorarioMarcacion, ConfiguracionHorarioMarcacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionHorarioMarcacionBO FirstBy(Expression<Func<TConfiguracionHorarioMarcacion, bool>> filter)
        {
            try
            {
                TConfiguracionHorarioMarcacion entidad = base.FirstBy(filter);
                ConfiguracionHorarioMarcacionBO objetoBO = Mapper.Map<TConfiguracionHorarioMarcacion, ConfiguracionHorarioMarcacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionHorarioMarcacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionHorarioMarcacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionHorarioMarcacionBO> listadoBO)
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

        public bool Update(ConfiguracionHorarioMarcacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionHorarioMarcacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionHorarioMarcacionBO> listadoBO)
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
        private void AsignacionId(TConfiguracionHorarioMarcacion entidad, ConfiguracionHorarioMarcacionBO objetoBO)
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

        private TConfiguracionHorarioMarcacion MapeoEntidad(ConfiguracionHorarioMarcacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionHorarioMarcacion entidad = new TConfiguracionHorarioMarcacion();
                entidad = Mapper.Map<ConfiguracionHorarioMarcacionBO, TConfiguracionHorarioMarcacion>(objetoBO,
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

        public List<ConfiguracionHorarioDTO> getConfiguracion()
        {
            try
            {

                List<ConfiguracionHorarioDTO> configuracion = new List<ConfiguracionHorarioDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre,HoraInicio,HoraFin FROM [gp].[V_ConfiguracionHorarioMarcacion]  where Estado= 1";
                var horconfiguracion = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(horconfiguracion) && !horconfiguracion.Contains("[]"))
                {
                    configuracion = JsonConvert.DeserializeObject<List<ConfiguracionHorarioDTO>>(horconfiguracion);
                    foreach (var item in configuracion)
                    {
                        item.IdHorarioGrupoPersonal = new List<ConfiguracionHorarioMarcacionGrupoDTO>();
                        var _queryfiltrotipo = "Select Id,IdHorarioGrupoPersonal,IdConfiguracionHorarioMarcacion FROM [gp].[T_ConfiguracionHorarioMarcacionGrupo] where Estado = 1 and IdConfiguracionHorarioMarcacion =@IdConfiguracionHorarioMarcacion";
                        var SubfiltroCriteriotipo = _dapper.QueryDapper(_queryfiltrotipo, new { IdConfiguracionHorarioMarcacion = item.Id });
                        if (!string.IsNullOrEmpty(SubfiltroCriteriotipo) && !SubfiltroCriteriotipo.Contains("[]"))
                        {
                            item.IdHorarioGrupoPersonal = JsonConvert.DeserializeObject<List<ConfiguracionHorarioMarcacionGrupoDTO>>(SubfiltroCriteriotipo);
                        }                        
                    }
                }
                return configuracion;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
