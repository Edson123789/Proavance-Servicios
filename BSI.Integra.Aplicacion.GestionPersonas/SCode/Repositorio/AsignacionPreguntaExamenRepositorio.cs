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
    /// Repositorio: AsignacionPreguntaExamenRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Asignación de Preguntas por Examen
    /// </summary>
    public class AsignacionPreguntaExamenRepositorio : BaseRepository<TAsignacionPreguntaExamen, AsignacionPreguntaExamenBO>
    {
        #region Metodos Base
        public AsignacionPreguntaExamenRepositorio() : base()
        {
        }
        public AsignacionPreguntaExamenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AsignacionPreguntaExamenBO> GetBy(Expression<Func<TAsignacionPreguntaExamen, bool>> filter)
        {
            IEnumerable<TAsignacionPreguntaExamen> listado = base.GetBy(filter);
            List<AsignacionPreguntaExamenBO> listadoBO = new List<AsignacionPreguntaExamenBO>();
            foreach (var itemEntidad in listado)
            {
                AsignacionPreguntaExamenBO objetoBO = Mapper.Map<TAsignacionPreguntaExamen, AsignacionPreguntaExamenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AsignacionPreguntaExamenBO FirstById(int id)
        {
            try
            {
                TAsignacionPreguntaExamen entidad = base.FirstById(id);
                AsignacionPreguntaExamenBO objetoBO = new AsignacionPreguntaExamenBO();
                Mapper.Map<TAsignacionPreguntaExamen, AsignacionPreguntaExamenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsignacionPreguntaExamenBO FirstBy(Expression<Func<TAsignacionPreguntaExamen, bool>> filter)
        {
            try
            {
                TAsignacionPreguntaExamen entidad = base.FirstBy(filter);
                AsignacionPreguntaExamenBO objetoBO = Mapper.Map<TAsignacionPreguntaExamen, AsignacionPreguntaExamenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AsignacionPreguntaExamenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAsignacionPreguntaExamen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AsignacionPreguntaExamenBO> listadoBO)
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

        public bool Update(AsignacionPreguntaExamenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAsignacionPreguntaExamen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AsignacionPreguntaExamenBO> listadoBO)
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
        private void AsignacionId(TAsignacionPreguntaExamen entidad, AsignacionPreguntaExamenBO objetoBO)
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

        private TAsignacionPreguntaExamen MapeoEntidad(AsignacionPreguntaExamenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAsignacionPreguntaExamen entidad = new TAsignacionPreguntaExamen();
                entidad = Mapper.Map<AsignacionPreguntaExamenBO, TAsignacionPreguntaExamen>(objetoBO,
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

        public List<PreguntaNoAsociadaExamenDTO> ObtenerPreguntaNoAsignadoExamen(int IdExamen)
        {
            try
            {
                List<PreguntaNoAsociadaExamenDTO> PreguntaNoAsociado = new List<PreguntaNoAsociadaExamenDTO>();
                var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_PreguntasNoAsociadasExamen", new { IdExamen });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    PreguntaNoAsociado = JsonConvert.DeserializeObject<List<PreguntaNoAsociadaExamenDTO>>(listaProcesoDB);
                }
                return PreguntaNoAsociado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PreguntaAsociadaExamenDTO> ObtenerPreguntaAsignadoExamen(int IdExamen)
        {
            try
            {
                List<PreguntaAsociadaExamenDTO> PreguntaAsociado = new List<PreguntaAsociadaExamenDTO>();
                var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_PreguntasAsociadasExamen", new { IdExamen });
                if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
                {
                    PreguntaAsociado = JsonConvert.DeserializeObject<List<PreguntaAsociadaExamenDTO>>(listaProcesoDB);
                }
                return PreguntaAsociado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
