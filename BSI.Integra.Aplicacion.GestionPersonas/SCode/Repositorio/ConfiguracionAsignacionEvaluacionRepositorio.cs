using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: ConfiguracionAsignacionEvaluacionRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión configuración de asignación de evaluaciones por Proceso de Seleccion
    /// </summary>
    public class ConfiguracionAsignacionEvaluacionRepositorio : BaseRepository<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionBO>
    {
        #region Metodos Base
        public ConfiguracionAsignacionEvaluacionRepositorio() : base()
        {
        }
        public ConfiguracionAsignacionEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionAsignacionEvaluacionBO> GetBy(Expression<Func<TConfiguracionAsignacionEvaluacion, bool>> filter)
        {
            IEnumerable<TConfiguracionAsignacionEvaluacion> listado = base.GetBy(filter);
            List<ConfiguracionAsignacionEvaluacionBO> listadoBO = new List<ConfiguracionAsignacionEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionAsignacionEvaluacionBO objetoBO = Mapper.Map<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionAsignacionEvaluacionBO FirstById(int id)
        {
            try
            {
                TConfiguracionAsignacionEvaluacion entidad = base.FirstById(id);
                ConfiguracionAsignacionEvaluacionBO objetoBO = new ConfiguracionAsignacionEvaluacionBO();
                Mapper.Map<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionAsignacionEvaluacionBO FirstBy(Expression<Func<TConfiguracionAsignacionEvaluacion, bool>> filter)
        {
            try
            {
                TConfiguracionAsignacionEvaluacion entidad = base.FirstBy(filter);
                ConfiguracionAsignacionEvaluacionBO objetoBO = Mapper.Map<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionAsignacionEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionAsignacionEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionAsignacionEvaluacionBO> listadoBO)
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

        public bool Update(ConfiguracionAsignacionEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionAsignacionEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionAsignacionEvaluacionBO> listadoBO)
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
        private void AsignacionId(TConfiguracionAsignacionEvaluacion entidad, ConfiguracionAsignacionEvaluacionBO objetoBO)
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

        private TConfiguracionAsignacionEvaluacion MapeoEntidad(ConfiguracionAsignacionEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionAsignacionEvaluacion entidad = new TConfiguracionAsignacionEvaluacion();
                entidad = Mapper.Map<ConfiguracionAsignacionEvaluacionBO, TConfiguracionAsignacionEvaluacion>(objetoBO,
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
    }
}
