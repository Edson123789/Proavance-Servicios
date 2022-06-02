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
    /// Repositorio: CriterioEvaluacionProcesoRepositorio
    /// Autor: Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// Repositorio para de tabla T_CriterioEvaluacionProceso
    /// </summary>
    public class CriterioEvaluacionProcesoRepositorio : BaseRepository<TCriterioEvaluacionProceso, CriterioEvaluacionProcesoBO>
    {
        #region Metodos Base
        public CriterioEvaluacionProcesoRepositorio() : base()
        {
        }
        public CriterioEvaluacionProcesoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionProcesoBO> GetBy(Expression<Func<TCriterioEvaluacionProceso, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionProceso> listado = base.GetBy(filter);
            List<CriterioEvaluacionProcesoBO> listadoBO = new List<CriterioEvaluacionProcesoBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionProcesoBO objetoBO = Mapper.Map<TCriterioEvaluacionProceso, CriterioEvaluacionProcesoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CriterioEvaluacionProcesoBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionProceso entidad = base.FirstById(id);
                CriterioEvaluacionProcesoBO objetoBO = new CriterioEvaluacionProcesoBO();
                Mapper.Map<TCriterioEvaluacionProceso, CriterioEvaluacionProcesoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionProcesoBO FirstBy(Expression<Func<TCriterioEvaluacionProceso, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionProceso entidad = base.FirstBy(filter);
                CriterioEvaluacionProcesoBO objetoBO = Mapper.Map<TCriterioEvaluacionProceso, CriterioEvaluacionProcesoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionProcesoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionProceso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionProcesoBO> listadoBO)
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

        public bool Update(CriterioEvaluacionProcesoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionProceso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionProcesoBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionProceso entidad, CriterioEvaluacionProcesoBO objetoBO)
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

        private TCriterioEvaluacionProceso MapeoEntidad(CriterioEvaluacionProcesoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionProceso entidad = new TCriterioEvaluacionProceso();
                entidad = Mapper.Map<CriterioEvaluacionProcesoBO, TCriterioEvaluacionProceso>(objetoBO,
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
