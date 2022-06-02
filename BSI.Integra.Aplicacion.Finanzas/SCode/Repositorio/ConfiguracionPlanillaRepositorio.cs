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
    public class ConfiguracionPlanillaRepositorio : BaseRepository<TConfiguracionPlanilla, ConfiguracionPlanillaBO>
    {
        #region Metodos Base
        public ConfiguracionPlanillaRepositorio() : base()
        {
        }
        public ConfiguracionPlanillaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionPlanillaBO> GetBy(Expression<Func<TConfiguracionPlanilla, bool>> filter)
        {
            IEnumerable<TConfiguracionPlanilla> listado = base.GetBy(filter);
            List<ConfiguracionPlanillaBO> listadoBO = new List<ConfiguracionPlanillaBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionPlanillaBO objetoBO = Mapper.Map<TConfiguracionPlanilla, ConfiguracionPlanillaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionPlanillaBO FirstById(int id)
        {
            try
            {
                TConfiguracionPlanilla entidad = base.FirstById(id);
                ConfiguracionPlanillaBO objetoBO = new ConfiguracionPlanillaBO();
                Mapper.Map<TConfiguracionPlanilla, ConfiguracionPlanillaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionPlanillaBO FirstBy(Expression<Func<TConfiguracionPlanilla, bool>> filter)
        {
            try
            {
                TConfiguracionPlanilla entidad = base.FirstBy(filter);
                ConfiguracionPlanillaBO objetoBO = Mapper.Map<TConfiguracionPlanilla, ConfiguracionPlanillaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionPlanillaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionPlanilla entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionPlanillaBO> listadoBO)
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

        public bool Update(ConfiguracionPlanillaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionPlanilla entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionPlanillaBO> listadoBO)
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
        private void AsignacionId(TConfiguracionPlanilla entidad, ConfiguracionPlanillaBO objetoBO)
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

        private TConfiguracionPlanilla MapeoEntidad(ConfiguracionPlanillaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionPlanilla entidad = new TConfiguracionPlanilla();
                entidad = Mapper.Map<ConfiguracionPlanillaBO, TConfiguracionPlanilla>(objetoBO,
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
        /// Retorna una lista de los cargos para ser usados en grilla de su propio CRUD 
        /// </summary>
        /// <returns></returns>
        public List<ConfiguracionPlanillaDTO> ObtenerTodoConfiguracionPlanilla()
        {
            try
            {
                var lista = GetBy(x => true, y => new ConfiguracionPlanillaDTO
                {
                    Id = y.Id,
                    IdTipoRemuneracionAdicional = y.IdTipoRemuneracionAdicional,
                    Nombre = y.Nombre,
                    Recurrente = y.Recurrente
                }).OrderByDescending(x => x.Id).ToList();

                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obitene el DTO para Configuracion de planillas, en la vista extraelas fechas en las que se va a crear una planilla en caso de ser recurrente
        /// si la planilla no es recurrente se va a crear en  las fechas indicadas
        /// </summary>
        /// <returns></returns>
        public List<ConfiguracionPlanillaFechaGeneracionDTO> ObtenerRegistroConfiguracionPlanilla()
        {
            try
            {
                List<ConfiguracionPlanillaFechaGeneracionDTO> planillaConf = new List<ConfiguracionPlanillaFechaGeneracionDTO>();
                var _query = "SELECT IdConfiguracionPlanilla,EsMensual, FechaProceso ,IdTipoRemuneracion,NombreRemuneracion,Estado FROM fin.V_ObtenerConfiguracionPlanilla WHERE Estado = 1";

                var PlanillaConfiguracionesDB = _dapper.QueryDapper(_query, null);
                if (!PlanillaConfiguracionesDB.Contains("[]") && !string.IsNullOrEmpty(PlanillaConfiguracionesDB))
                {
                    planillaConf = JsonConvert.DeserializeObject<List<ConfiguracionPlanillaFechaGeneracionDTO>>(PlanillaConfiguracionesDB);
                }
                return planillaConf;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<DatoPlanillaCreacionFurDTO> ObtenerDatosPlanillaPersonalActivo()
        {
            try
            {
                List<DatoPlanillaCreacionFurDTO> planillas = new List<DatoPlanillaCreacionFurDTO>();
                var _query = "SELECT IdPersonal,IdContrato,NombrePersonal,AreaTrabajo,RemuneracionFija,FechaInicioContratoAnterior,FechaFinContratoAnterior,FechaInicioContrato,FechaFinContrato,EsTrabajadorContinuo,RecibeBeneficios " +
                    ",RecibeBeneficiosDesdeContratoAnterior,IdTipoContratoAnterior,IdTipoContrato,TipoContrato,TieneContratoVigente,NroMesesBeneficio,NroDiasBeneficio,EsCesado, TieneBono , TieneAsignacionFamiliar,TieneSistemaPensionario, BonoTotal, IdProveedor, IdSedeTrabajo,PorcentajeSistemaPensionario " +
                    "FROM fin.V_ObtenerDatosPlanilla";

                var PlanillaConfiguracionesDB = _dapper.QueryDapper(_query, null);
                if (!PlanillaConfiguracionesDB.Contains("[]") && !string.IsNullOrEmpty(PlanillaConfiguracionesDB))
                {
                    planillas = JsonConvert.DeserializeObject<List<DatoPlanillaCreacionFurDTO>>(PlanillaConfiguracionesDB);
                }
                return planillas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
