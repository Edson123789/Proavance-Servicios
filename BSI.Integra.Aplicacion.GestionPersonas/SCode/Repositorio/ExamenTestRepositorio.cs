using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: ExamenTestRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Examenes-Test
    /// </summary>
    public class ExamenTestRepositorio : BaseRepository<TExamenTest, ExamenTestBO>
    {
        #region Metodos Base
        public ExamenTestRepositorio() : base()
        {
        }
        public ExamenTestRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExamenTestBO> GetBy(Expression<Func<TExamenTest, bool>> filter)
        {
            IEnumerable<TExamenTest> listado = base.GetBy(filter);
            List<ExamenTestBO> listadoBO = new List<ExamenTestBO>();
            foreach (var itemEntidad in listado)
            {
                ExamenTestBO objetoBO = Mapper.Map<TExamenTest, ExamenTestBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExamenTestBO FirstById(int id)
        {
            try
            {
                TExamenTest entidad = base.FirstById(id);
                ExamenTestBO objetoBO = new ExamenTestBO();
                Mapper.Map<TExamenTest, ExamenTestBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExamenTestBO FirstBy(Expression<Func<TExamenTest, bool>> filter)
        {
            try
            {
                TExamenTest entidad = base.FirstBy(filter);
                ExamenTestBO objetoBO = Mapper.Map<TExamenTest, ExamenTestBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExamenTestBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExamenTest entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExamenTestBO> listadoBO)
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

        public bool Update(ExamenTestBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExamenTest entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExamenTestBO> listadoBO)
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
        private void AsignacionId(TExamenTest entidad, ExamenTestBO objetoBO)
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

        private TExamenTest MapeoEntidad(ExamenTestBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExamenTest entidad = new TExamenTest();
                entidad = Mapper.Map<ExamenTestBO, TExamenTest>(objetoBO,
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

        public List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int IdEvaluacion)
        {
            try
            {
                List<EvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<EvaluacionAgrupadaComponenteDTO>();
                var campos = "IdAsignacionPreguntaExamen,IdComponente,NombreComponente,IdGrupoComponenteEvaluacion,NombreGrupoComponenteEvaluacion,IdEvaluacion,NombreEvaluacion,IdPregunta,EnunciadoPregunta,NroOrden ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerEvaluacionAgrupadaExamen where IdEvaluacion="+IdEvaluacion+ " order by IdEvaluacion, IdGrupoComponenteEvaluacion,IdComponente,NroOrden";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<EvaluacionAgrupadaComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EstructuraBasicaDTO> ObtenerEvaluacionNoAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<EstructuraBasicaDTO> ProcesoSeleccion = new List<EstructuraBasicaDTO>();
                var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_EvaluacionesNoAsociadosConfiguracion", new { IdProcesoSeleccion });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    ProcesoSeleccion = JsonConvert.DeserializeObject<List<EstructuraBasicaDTO>>(listaProcesoDB);
                }
                return ProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EvaluacionAsignadoProcesoDTO> ObtenerEvaluacionAsignadoProcesoSeleccion(int IdProcesoSeleccion)
        {
            try
            {
                List<EvaluacionAsignadoProcesoDTO> ProcesoSeleccion = new List<EvaluacionAsignadoProcesoDTO>();
                var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_EvaluacionesAsociadosConfiguracion", new { IdProcesoSeleccion });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    ProcesoSeleccion = JsonConvert.DeserializeObject<List<EvaluacionAsignadoProcesoDTO>>(listaProcesoDB);
                }
                return ProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public List<NombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje(int IdProcesoSeleccion)
        {
            try
            {
                List<NombreEvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                var campos = "IdProcesoSeleccion,CalificacionTotal,IdEvaluacion,NombreEvaluacion,IdGrupo,NombreGrupo,IdComponente,NombreComponente";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerExamenesProcesoSeleccion where IdProcesoSeleccion=" + IdProcesoSeleccion ;
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<NombreEvaluacionAgrupadaComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
