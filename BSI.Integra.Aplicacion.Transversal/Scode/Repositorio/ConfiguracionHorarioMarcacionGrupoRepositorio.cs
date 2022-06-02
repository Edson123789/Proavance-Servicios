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
    public class ConfiguracionHorarioMarcacionGrupoRepositorio : BaseRepository<TConfiguracionHorarioMarcacionGrupo, ConfiguracionHorarioMarcacionGrupoBO>
    {
        #region Metodos Base
        public ConfiguracionHorarioMarcacionGrupoRepositorio() : base()
        {
        }
        public ConfiguracionHorarioMarcacionGrupoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionHorarioMarcacionGrupoBO> GetBy(Expression<Func<TConfiguracionHorarioMarcacionGrupo, bool>> filter)
        {
            IEnumerable<TConfiguracionHorarioMarcacionGrupo> listado = base.GetBy(filter);
            List<ConfiguracionHorarioMarcacionGrupoBO> listadoBO = new List<ConfiguracionHorarioMarcacionGrupoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionHorarioMarcacionGrupoBO objetoBO = Mapper.Map<TConfiguracionHorarioMarcacionGrupo, ConfiguracionHorarioMarcacionGrupoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionHorarioMarcacionGrupoBO FirstById(int id)
        {
            try
            {
                TConfiguracionHorarioMarcacionGrupo entidad = base.FirstById(id);
                ConfiguracionHorarioMarcacionGrupoBO objetoBO = new ConfiguracionHorarioMarcacionGrupoBO();
                Mapper.Map<TConfiguracionHorarioMarcacionGrupo, ConfiguracionHorarioMarcacionGrupoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionHorarioMarcacionGrupoBO FirstBy(Expression<Func<TConfiguracionHorarioMarcacionGrupo, bool>> filter)
        {
            try
            {
                TConfiguracionHorarioMarcacionGrupo entidad = base.FirstBy(filter);
                ConfiguracionHorarioMarcacionGrupoBO objetoBO = Mapper.Map<TConfiguracionHorarioMarcacionGrupo, ConfiguracionHorarioMarcacionGrupoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionHorarioMarcacionGrupoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionHorarioMarcacionGrupo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionHorarioMarcacionGrupoBO> listadoBO)
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

        public bool Update(ConfiguracionHorarioMarcacionGrupoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionHorarioMarcacionGrupo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionHorarioMarcacionGrupoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionHorarioMarcacionGrupo entidad, ConfiguracionHorarioMarcacionGrupoBO objetoBO)
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

        private TConfiguracionHorarioMarcacionGrupo MapeoEntidad(ConfiguracionHorarioMarcacionGrupoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionHorarioMarcacionGrupo entidad = new TConfiguracionHorarioMarcacionGrupo();
                entidad = Mapper.Map<ConfiguracionHorarioMarcacionGrupoBO, TConfiguracionHorarioMarcacionGrupo>(objetoBO,
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
        public void DeleteLogicoPorGrupo(int IdConfiguracion, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM gp.T_ConfiguracionHorarioMarcacionGrupo WHERE Estado = 1 and IdConfiguracionHorarioMarcacion = @IdConfiguracion ";
                var query = _dapper.QueryDapper(_query, new { IdConfiguracion });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ConfiguracionHorarioMarcacionGrupoDTO> listargruposconfiguracion(int Idgrupo)
        {
            try
            {
                List<ConfiguracionHorarioMarcacionGrupoDTO> filtro = new List<ConfiguracionHorarioMarcacionGrupoDTO>();
                var _queryfiltrocriterio = "Select IdHorarioGrupoPersonal FROM [gp].[T_ConfiguracionHorarioMarcacionGrupo] where Estado = 1 and IdConfiguracionHorarioMarcacion = @Idgrupo";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, new { Idgrupo });
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    filtro = JsonConvert.DeserializeObject<List<ConfiguracionHorarioMarcacionGrupoDTO>>(SubfiltroCriterio);
                }
                return filtro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
