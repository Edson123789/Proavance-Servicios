
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
    public class ConfiguracionRutinaBncObsoletoRepositorio : BaseRepository<TConfiguracionRutinaBncObsoleto, ConfiguracionRutinaBncObsoletoBO>
    {
        #region Metodos Base
        public ConfiguracionRutinaBncObsoletoRepositorio() : base()
        {
        }
        public ConfiguracionRutinaBncObsoletoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionRutinaBncObsoletoBO> GetBy(Expression<Func<TConfiguracionRutinaBncObsoleto, bool>> filter)
        {
            IEnumerable<TConfiguracionRutinaBncObsoleto> listado = base.GetBy(filter);
            List<ConfiguracionRutinaBncObsoletoBO> listadoBO = new List<ConfiguracionRutinaBncObsoletoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionRutinaBncObsoletoBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoleto, ConfiguracionRutinaBncObsoletoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionRutinaBncObsoletoBO FirstById(int id)
        {
            try
            {
                TConfiguracionRutinaBncObsoleto entidad = base.FirstById(id);
                ConfiguracionRutinaBncObsoletoBO objetoBO = new ConfiguracionRutinaBncObsoletoBO();
                Mapper.Map<TConfiguracionRutinaBncObsoleto, ConfiguracionRutinaBncObsoletoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionRutinaBncObsoletoBO FirstBy(Expression<Func<TConfiguracionRutinaBncObsoleto, bool>> filter)
        {
            try
            {
                TConfiguracionRutinaBncObsoleto entidad = base.FirstBy(filter);
                ConfiguracionRutinaBncObsoletoBO objetoBO = Mapper.Map<TConfiguracionRutinaBncObsoleto, ConfiguracionRutinaBncObsoletoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionRutinaBncObsoletoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionRutinaBncObsoleto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionRutinaBncObsoletoBO> listadoBO)
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

        public bool Update(ConfiguracionRutinaBncObsoletoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionRutinaBncObsoleto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionRutinaBncObsoletoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionRutinaBncObsoleto entidad, ConfiguracionRutinaBncObsoletoBO objetoBO)
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

        private TConfiguracionRutinaBncObsoleto MapeoEntidad(ConfiguracionRutinaBncObsoletoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionRutinaBncObsoleto entidad = new TConfiguracionRutinaBncObsoleto();
                entidad = Mapper.Map<ConfiguracionRutinaBncObsoletoBO, TConfiguracionRutinaBncObsoleto>(objetoBO,
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
        /// Fecha: 12/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion del Modulo Configuracion Cerrar BNC
        /// </summary>
        /// <param></param>
        /// <returns>Objeto Lista</returns>
        public List<ConfiguracionRutinaBncObsoletoDTO> ObtenerTodoConfiguracion()
        {
            try
            {
                List<ConfiguracionRutinaBncObsoletoDTO> lista = new List<ConfiguracionRutinaBncObsoletoDTO>();
                var query = string.Empty;
                query = "Select Id,Nombre,NumDiasProbabilidadMedia,NumDiasProbabilidadAlta,NumDiasProbabilidadMuyAlta,EjecutarRutinaProbabilidadMedia,EjecutarRutinaProbabilidadAlta" +
                                         " ,EjecutarRutinaProbabilidadMuyAlta,IdOcurrenciaDestino,EjecutarRutinaEnviarCorreo,IdPlantillaCorreo,IdPersonalCorreoNoExistente From mkt.V_TConfiguracionRutinaBncObsoleto_ObtenerTodo Where Estado =1";
                var resultadosDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadosDB) && !resultadosDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ConfiguracionRutinaBncObsoletoDTO>>(resultadosDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Oportunidades BNC 
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>

        public List<OportunidadesBNCObsoletasDTO> ObtenerOportunidadesBNC_RN5()
        {
            ConfiguracionRutinaBncObsoletoRepositorio repoConfiguracionRutinaBncObsoleto = new ConfiguracionRutinaBncObsoletoRepositorio();
            PersonalRepositorio repPersonal = new PersonalRepositorio();
            var entidades = repoConfiguracionRutinaBncObsoleto.ObtenerOportunidadesBNC_A_RN5();            

            List<int> listaIdPersonalActivo = (from x in repPersonal.GetTodoPersonalActivoParaFiltro() select x.Id).ToList();

            List<OportunidadesBNCObsoletasDTO> listafinal = null;
            listafinal = entidades.Where(m => !listaIdPersonalActivo.Contains(m.IdPersonal_Asignado)).ToList();
            foreach (var enti in listafinal)
            {
                foreach (var test in entidades)
                {
                    if (test.Id.ToString().Equals(enti.Id.ToString()))
                    {
                        test.IdPersonal_Asignado = 0;
                    }
                }
            }
            return entidades;
        }
        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Oportunidades BNC Historico Por Probabilidad
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>

        public List<OportunidadesBNCObsoletasDTO> ObtenerOportunidadesBNC_A_RN5()
        {
            try
            {
                List<OportunidadesBNCObsoletasDTO> obtenerOportunidadesBNC_A_RN5 = new List<OportunidadesBNCObsoletasDTO>();

                var obtenerOportunidadesBNC_A_RN5DB = _dapper.QuerySPDapper("[mkt].[SP_PanelCerrarBNCHistoricosPorProbabilidadNuevoModelo]", null);
                if (!string.IsNullOrEmpty(obtenerOportunidadesBNC_A_RN5DB) && !obtenerOportunidadesBNC_A_RN5DB.Contains("[]"))
                {
                    obtenerOportunidadesBNC_A_RN5 = JsonConvert.DeserializeObject<List<OportunidadesBNCObsoletasDTO>>(obtenerOportunidadesBNC_A_RN5DB);
                }
                return obtenerOportunidadesBNC_A_RN5;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}