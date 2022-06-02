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
    public class CronogramaPagoDetalleFinalPorDiaRepositorio : BaseRepository<TCronogramaPagoDetalleFinalPorDia, CronogramaPagoDetalleFinalPorDiaBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleFinalPorDiaRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleFinalPorDiaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleFinalPorDiaBO> GetBy(Expression<Func<TCronogramaPagoDetalleFinalPorDia, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalleFinalPorDia> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleFinalPorDiaBO> listadoBO = new List<CronogramaPagoDetalleFinalPorDiaBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleFinalPorDiaBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalPorDia, CronogramaPagoDetalleFinalPorDiaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoDetalleFinalPorDiaBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleFinalPorDia entidad = base.FirstById(id);
                CronogramaPagoDetalleFinalPorDiaBO objetoBO = new CronogramaPagoDetalleFinalPorDiaBO();
                Mapper.Map<TCronogramaPagoDetalleFinalPorDia, CronogramaPagoDetalleFinalPorDiaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleFinalPorDiaBO FirstBy(Expression<Func<TCronogramaPagoDetalleFinalPorDia, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleFinalPorDia entidad = base.FirstBy(filter);
                CronogramaPagoDetalleFinalPorDiaBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinalPorDia, CronogramaPagoDetalleFinalPorDiaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleFinalPorDiaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleFinalPorDia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleFinalPorDiaBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleFinalPorDiaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleFinalPorDia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleFinalPorDiaBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleFinalPorDia entidad, CronogramaPagoDetalleFinalPorDiaBO objetoBO)
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

        private TCronogramaPagoDetalleFinalPorDia MapeoEntidad(CronogramaPagoDetalleFinalPorDiaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleFinalPorDia entidad = new TCronogramaPagoDetalleFinalPorDia();
                entidad = Mapper.Map<CronogramaPagoDetalleFinalPorDiaBO, TCronogramaPagoDetalleFinalPorDia>(objetoBO,
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
        /// Guarda el cierre correspondiente a la fecha seleccionada
        /// </summary>
        /// <returns></returns>
        public bool GenerarCierrePorDia(FiltroCierrePorDiaDTO FiltroCierrePorDia)
        {
            try
            {
                bool items= false;
                var fechaCierre = new DateTime(FiltroCierrePorDia.FechaCierre.Year, FiltroCierrePorDia.FechaCierre.Month, FiltroCierrePorDia.FechaCierre.Day, 0, 0, 0);
                var query = _dapper.QuerySPDapper("[fin].[SP_GenerarCierreCronogramaPagoDetalleFinalPorDia]", new { fechaCierre, usuario = FiltroCierrePorDia.Usuario});

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
        /// Guarda el cierre correspondiente a la fecha seleccionada
        /// </summary>
        /// <returns></returns>
        public bool GenerarCierreCronogramaVersionPorDia(FiltroCierrePorDiaDTO FiltroCierrePorDia)
        {
            try
            {
                bool items = false;
                var fechaCierre = new DateTime(FiltroCierrePorDia.FechaCierre.Year, FiltroCierrePorDia.FechaCierre.Month, FiltroCierrePorDia.FechaCierre.Day, 0, 0, 0);
                var query = _dapper.QuerySPDapper("[fin].[SP_InsertarTablaCronogramaVersionFinalPorDiaCierre]", new { fechaCierre});

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
        public bool ExisteIdPeriodo(DateTime FechaCierre)
        {
            try
            {
                var fechaCierre = new DateTime(FechaCierre.Year, FechaCierre.Month, FechaCierre.Day, 0, 0, 0);
                var _resultado = new ValorBoolDTO();
                var query = $@"
                            SELECT
                                    CASE WHEN EXISTS 
                                    (
                                          SELECT * FROM fin.T_CronogramaPagoDetalleFinalPorDia where FechaCierre = @FechaCierre
                                    )
                                    THEN 1
                                    ELSE 0
                                 END
                            AS Valor
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { FechaCierre = fechaCierre });

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
