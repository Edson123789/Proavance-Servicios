using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class ComprobantePagoPorFurRepositorio : BaseRepository<TComprobantePagoPorFur, ComprobantePagoPorFurBO>
    {
        #region Metodos Base
        public ComprobantePagoPorFurRepositorio() : base()
        {
        }
        public ComprobantePagoPorFurRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ComprobantePagoPorFurBO> GetBy(Expression<Func<TComprobantePagoPorFur, bool>> filter)
        {
            IEnumerable<TComprobantePagoPorFur> listado = base.GetBy(filter);
            List<ComprobantePagoPorFurBO> listadoBO = new List<ComprobantePagoPorFurBO>();
            foreach (var itemEntidad in listado)
            {
                ComprobantePagoPorFurBO objetoBO = Mapper.Map<TComprobantePagoPorFur, ComprobantePagoPorFurBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ComprobantePagoPorFurBO FirstById(int id)
        {
            try
            {
                TComprobantePagoPorFur entidad = base.FirstById(id);
                ComprobantePagoPorFurBO objetoBO = new ComprobantePagoPorFurBO();
                Mapper.Map<TComprobantePagoPorFur, ComprobantePagoPorFurBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComprobantePagoPorFurBO FirstBy(Expression<Func<TComprobantePagoPorFur, bool>> filter)
        {
            try
            {
                TComprobantePagoPorFur entidad = base.FirstBy(filter);
                ComprobantePagoPorFurBO objetoBO = Mapper.Map<TComprobantePagoPorFur, ComprobantePagoPorFurBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ComprobantePagoPorFurBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TComprobantePagoPorFur entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ComprobantePagoPorFurBO> listadoBO)
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

        public bool Update(ComprobantePagoPorFurBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TComprobantePagoPorFur entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ComprobantePagoPorFurBO> listadoBO)
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
        private void AsignacionId(TComprobantePagoPorFur entidad, ComprobantePagoPorFurBO objetoBO)
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

        private TComprobantePagoPorFur MapeoEntidad(ComprobantePagoPorFurBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TComprobantePagoPorFur entidad = new TComprobantePagoPorFur();
                entidad = Mapper.Map<ComprobantePagoPorFurBO, TComprobantePagoPorFur>(objetoBO,
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
        /// Obtiene el monto utilizado de un comprobante basandose en los registros de T_ComprobantePagoPorFur
        /// </summary>
        /// <returns></returns>
        public List<ComprobanteMontoUtilizadoDTO> ObtenerMontoUtilizadoComprobante(int IdComprobante)
        {
            try
            {
                List<ComprobanteMontoUtilizadoDTO> ComprobanteMontoUtilizado = new List<ComprobanteMontoUtilizadoDTO>();
                var _query = "SELECT IdComprobantePago, MontoUtilizado from ( select IdComprobantePago, sum(Monto) as MontoUtilizado from fin.T_ComprobantePagoPorFur where Estado=1 group by (IdComprobantePago)  ) SumatoriaComprobante where IdComprobantePago=" + IdComprobante;
                var ComprobanteMontoUtilizadoDB = _dapper.QueryDapper(_query, null);
                if (!ComprobanteMontoUtilizadoDB.Contains("[]") && !string.IsNullOrEmpty(ComprobanteMontoUtilizadoDB))
                {
                    ComprobanteMontoUtilizado = JsonConvert.DeserializeObject<List<ComprobanteMontoUtilizadoDTO>>(ComprobanteMontoUtilizadoDB);
                }
                return ComprobanteMontoUtilizado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera el Reporte de Egreso por Rubro
        /// </summary>
        /// <param name="Sedes"></param>
        /// <param name="Anio"></param>
        /// <returns></returns>
        public List<ReporteEgresoPorRubroDTO> ObtenerDatosReporteEgresosPorRubro(string Sedes, int Anio)
        {
            try
            {
                List<ReporteEgresoPorRubroDTO> gastosRubro = new List<ReporteEgresoPorRubroDTO>();
                var _query = "exec [fin].[SP_GenerarReporteEgresoPorRubro] '" + Sedes + "' , " + Anio;
                var gastosRubroDB = _dapper.QueryDapper(_query, null);
                if (!gastosRubroDB.Contains("[]") && !string.IsNullOrEmpty(gastosRubroDB))
                {
                    gastosRubro = JsonConvert.DeserializeObject<List<ReporteEgresoPorRubroDTO>>(gastosRubroDB);
                }
                return gastosRubro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera el desgloce de ReporteEgresoPorRubro
        /// </summary>
        /// <param name="Sedes"></param>
        /// <param name="Anio"></param>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public List<DesgloseReporteEgresoPorRubroDTO> ObtenerDesgloceReporteEgresosPorRubro(string Sedes, int Anio, int IdRubro)
        {
            try
            {
                List<DesgloseReporteEgresoPorRubroDTO> gastosRubro = new List<DesgloseReporteEgresoPorRubroDTO>();
                var _query = "exec [fin].[SP_GenerarDesgloseReporteEgresoPorRubro] '" + Sedes + "' , " + Anio + ", " + IdRubro;
                var gastosRubroDB = _dapper.QueryDapper(_query, null);
                if (!gastosRubroDB.Contains("[]") && !string.IsNullOrEmpty(gastosRubroDB))
                {
                    gastosRubro = JsonConvert.DeserializeObject<List<DesgloseReporteEgresoPorRubroDTO>>(gastosRubroDB);
                }
                return gastosRubro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el todos los comprobantes asociados al idfur de T_ComprobantePagoPorFur
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerComprobantePorFur(int IdFur)
        {
            try
            {
                List<FiltroDTO> ComprobantePorFur = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre from fin.V_ObtenerAsociacionComprobantesPorFur where IdFur = @IdFur AND EstadoCPPF = 1";
                var ComprobantePorFurDB = _dapper.QueryDapper(_query, new { IdFur });
                if (!ComprobantePorFurDB.Contains("[]") && !string.IsNullOrEmpty(ComprobantePorFurDB))
                {
                    ComprobantePorFur = JsonConvert.DeserializeObject<List<FiltroDTO>>(ComprobantePorFurDB);
                }
                return ComprobantePorFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene el Id del Comprobante asociado al Comprobante Pago por Fur.
        /// </summary>
        /// <returns></returns>
        public int ObtenerIdComprobante(int? IdComprobantePagoPorFur)
        {
            try
            {
                int IdComprobante = this.GetBy(x => x.Estado == true && x.Id == IdComprobantePagoPorFur).Select(x => x.IdComprobantePago).FirstOrDefault();
                return IdComprobante;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene el todos los comprobantes asociados al idfur de T_ComprobantePagoPorFur
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerComprobantesPorFur(int IdFur)
        {
            try
            {
                List<FiltroDTO> ComprobantePorFur = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre from fin.V_ObtenerAsociacionComprobantesPorFur where IdFur = @IdFur AND EstadoCPPF = 1";
                var ComprobantePorFurDB = _dapper.QueryDapper(_query, new { IdFur });
                if (!ComprobantePorFurDB.Contains("[]") && !string.IsNullOrEmpty(ComprobantePorFurDB))
                {
                    ComprobantePorFur = JsonConvert.DeserializeObject<List<FiltroDTO>>(ComprobantePorFurDB);
                }
                return ComprobantePorFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el todos los comprobantes asociados al idfur de T_ComprobantePagoPorFur para registrar el pago
        /// </summary>
        /// <returns></returns>
        public List<ComprobantePorFurDTO> ObtenerComprobantesPorFurParaPago(int IdFur)
        {
            try
            {
                List<ComprobantePorFurDTO> ComprobantePorFur = new List<ComprobantePorFurDTO>();
                var _query = "SELECT Id, Comprobante, Proveedor, IdAsociacion, NombreAsociacion, IdMoneda, MontoAsociado, MontoAmortizar from fin.V_ObtenerComprobantesPorFur where IdFur = @IdFur AND MontoAmortizar > 0 and (FurCancelado = 0 OR FurCancelado IS NULL) AND EstadoAsociacion = 1";
                var ComprobantePorFurDB = _dapper.QueryDapper(_query, new { IdFur });
                if (!ComprobantePorFurDB.Contains("[]") && !string.IsNullOrEmpty(ComprobantePorFurDB))
                {
                    ComprobantePorFur = JsonConvert.DeserializeObject<List<ComprobantePorFurDTO>>(ComprobantePorFurDB);
                }
                return ComprobantePorFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Comprobante por Fur asociados a un Comprobante
        /// </summary>
        /// <param name="idComprobante"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorComprobante(int idComprobante, string usuario, List<DatosComprobantePagoPorFurDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdComprobantePago == idComprobante && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.IdFur));
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

        /// <summary>
        ///  Obtiene el Id del Comprobantepagoporfur asociado al Comprobante Pago y al Fur.
        /// </summary>
        /// <returns></returns>
        public int ObtenerIdComprobantePorFur(int IdComprobante,int idFur)
        {
            try
            {
                int IdComprobantePagoPorFur = this.GetBy(x => x.Estado == true && x.IdComprobantePago == IdComprobante && x.IdFur == idFur).Select(x => x.Id).FirstOrDefault();
                return IdComprobantePagoPorFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        ///  Obtiene el Monto del Comprobantepagoporfur asociado al Comprobante Pago y al Fur.
        /// </summary>
        /// <returns></returns>
        public decimal ObtenerMontoPorComprobanteFur(int IdComprobante, int idFur)
        {
            try
            {
                decimal MontoAsociado = this.GetBy(x => x.Estado == true && x.IdComprobantePago == IdComprobante && x.IdFur == idFur).Select(x => x.Monto).FirstOrDefault();
                return MontoAsociado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
