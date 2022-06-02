using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Globalization;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CronogramaPagoDetalleFinalPorPeriodoRepositorio : BaseRepository<TCronogramaPagoDetalleFinalPorPeriodo, CronogramaPagoDetalleFinalPorPeriodoBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleFinalPorPeriodoRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleFinalPorPeriodoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleFinalPorPeriodoBO> GetBy(Expression<Func<TCronogramaPagoDetalleFinalPorPeriodo, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalleFinalPorPeriodo> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleFinalPorPeriodoBO> listadoBO = new List<CronogramaPagoDetalleFinalPorPeriodoBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleFinalPorPeriodoBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalPorPeriodo, CronogramaPagoDetalleFinalPorPeriodoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoDetalleFinalPorPeriodoBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleFinalPorPeriodo entidad = base.FirstById(id);
                CronogramaPagoDetalleFinalPorPeriodoBO objetoBO = new CronogramaPagoDetalleFinalPorPeriodoBO();
                Mapper.Map<TCronogramaPagoDetalleFinalPorPeriodo, CronogramaPagoDetalleFinalPorPeriodoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleFinalPorPeriodoBO FirstBy(Expression<Func<TCronogramaPagoDetalleFinalPorPeriodo, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleFinalPorPeriodo entidad = base.FirstBy(filter);
                CronogramaPagoDetalleFinalPorPeriodoBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalPorPeriodo, CronogramaPagoDetalleFinalPorPeriodoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleFinalPorPeriodoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleFinalPorPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleFinalPorPeriodoBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleFinalPorPeriodoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleFinalPorPeriodo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleFinalPorPeriodoBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleFinalPorPeriodo entidad, CronogramaPagoDetalleFinalPorPeriodoBO objetoBO)
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

        private TCronogramaPagoDetalleFinalPorPeriodo MapeoEntidad(CronogramaPagoDetalleFinalPorPeriodoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleFinalPorPeriodo entidad = new TCronogramaPagoDetalleFinalPorPeriodo();
                entidad = Mapper.Map<CronogramaPagoDetalleFinalPorPeriodoBO, TCronogramaPagoDetalleFinalPorPeriodo>(objetoBO,
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
        /// Guarda el cierre correspondiente al periodo seleccionado
        /// </summary>
        /// <returns></returns>
        public bool GenerarCierreReporteResumenMontos(FiltroCierreResumenMontosDTO FiltroCierreResumenMontos)
        {
            try
            {
                bool items= false;
                var query = _dapper.QuerySPDapper("[fin].[SP_GenerarCierreCronogramaPagoDetalleFinal]", new { idPeriodo = FiltroCierreResumenMontos.IdPeriodo, usuario = FiltroCierreResumenMontos.Usuario});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = true;
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene si existe el periodo enviado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExisteIdPeriodo(int idPeriodo)
        {
            try
            {
                var _resultado = new ValorBoolDTO();
                var query = $@"
                            SELECT
                                    CASE WHEN EXISTS 
                                    (
                                          SELECT * FROM fin.T_CronogramaPagoDetalleFinalPorPeriodo where IdPeriodo = @IdPeriodo
                                    )
                                    THEN 1
                                    ELSE 0
                                 END
                            AS Valor
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { IdPeriodo = idPeriodo });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }


}
