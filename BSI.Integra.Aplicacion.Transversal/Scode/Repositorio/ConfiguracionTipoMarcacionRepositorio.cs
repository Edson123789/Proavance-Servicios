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
    public class ConfiguracionTipoMarcacionRepositorio: BaseRepository<TConfiguracionTipoMarcacion, ConfiguracionTipoMarcacionBO>
    {
        #region Metodos Base
        public ConfiguracionTipoMarcacionRepositorio() : base()
        {
        }
        public ConfiguracionTipoMarcacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionTipoMarcacionBO> GetBy(Expression<Func<TConfiguracionTipoMarcacion, bool>> filter)
        {
            IEnumerable<TConfiguracionTipoMarcacion> listado = base.GetBy(filter);
            List<ConfiguracionTipoMarcacionBO> listadoBO = new List<ConfiguracionTipoMarcacionBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionTipoMarcacionBO objetoBO = Mapper.Map<TConfiguracionTipoMarcacion, ConfiguracionTipoMarcacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionTipoMarcacionBO FirstById(int id)
        {
            try
            {
                TConfiguracionTipoMarcacion entidad = base.FirstById(id);
                ConfiguracionTipoMarcacionBO objetoBO = new ConfiguracionTipoMarcacionBO();
                Mapper.Map<TConfiguracionTipoMarcacion, ConfiguracionTipoMarcacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionTipoMarcacionBO FirstBy(Expression<Func<TConfiguracionTipoMarcacion, bool>> filter)
        {
            try
            {
                TConfiguracionTipoMarcacion entidad = base.FirstBy(filter);
                ConfiguracionTipoMarcacionBO objetoBO = Mapper.Map<TConfiguracionTipoMarcacion, ConfiguracionTipoMarcacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionTipoMarcacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionTipoMarcacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionTipoMarcacionBO> listadoBO)
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

        public bool Update(ConfiguracionTipoMarcacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionTipoMarcacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionTipoMarcacionBO> listadoBO)
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
        private void AsignacionId(TConfiguracionTipoMarcacion entidad, ConfiguracionTipoMarcacionBO objetoBO)
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

        private TConfiguracionTipoMarcacion MapeoEntidad(ConfiguracionTipoMarcacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionTipoMarcacion entidad = new TConfiguracionTipoMarcacion();
                entidad = Mapper.Map<ConfiguracionTipoMarcacionBO, TConfiguracionTipoMarcacion>(objetoBO,
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

        public List<ConfiguracionBotonDTO> getConfiguracionBoton ()
        {
            try
            {
                List<ConfiguracionBotonDTO> boton = new List<ConfiguracionBotonDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,NombreBoton,HoraInicio,HoraFin FROM [gp].[V_ConfiguracionHorarioBoton]  where Estado= 1";
                var horconfiguracion = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(horconfiguracion) && !horconfiguracion.Contains("[]"))
                {
                    boton = JsonConvert.DeserializeObject<List<ConfiguracionBotonDTO>>(horconfiguracion);
                }
                return boton;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public int conseguirorden(int Idgrupo)
        {
            try
            {
                ValorIntDTO filtro = new ValorIntDTO();
                var _queryfiltrocriterio = "Select Valor FROM [gp].[V_ConseguirOrden] where Estado = 1 and Id = @Idgrupo";
                var SubfiltroCriterio = _dapper.FirstOrDefault(_queryfiltrocriterio, new { Idgrupo });
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    filtro = JsonConvert.DeserializeObject<ValorIntDTO>(SubfiltroCriterio);
                }
                return filtro.Valor;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        public List<BotonesOrdenDTO>  todoOrden()
        {
            try
            {
                List<BotonesOrdenDTO> filtro = new List<BotonesOrdenDTO>();
                var _queryfiltrocriterio = "Select Valor FROM [gp].[V_ConseguirOrden] where Estado = 1 ";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, new {});
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    filtro = JsonConvert.DeserializeObject<List<BotonesOrdenDTO>>(SubfiltroCriterio);
                }
                return filtro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        public string getMensaje()
        {
            try
            {
                ValorStringDTO filtro = new ValorStringDTO();
                var _queryfiltrocriterio = "Select Valor FROM [gp].[V_ConfiguracionMarcadorMensaje] where Estado = 1 ";
                var SubfiltroCriterio = _dapper.FirstOrDefault(_queryfiltrocriterio, new { });
                if (SubfiltroCriterio == null || filtro == null ) filtro.Valor = "Gracias por registrar su asistencia";
                else
                {
                    if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                    {
                        filtro = JsonConvert.DeserializeObject<ValorStringDTO>(SubfiltroCriterio);
                    }

                }
                
                
                return filtro.Valor;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
