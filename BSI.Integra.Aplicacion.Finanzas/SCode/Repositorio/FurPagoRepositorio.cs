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
    public class FurPagoRepositorio : BaseRepository<TFurPago, FurPagoBO>
    {
        #region Metodos Base
        public FurPagoRepositorio() : base()
        {
        }
        public FurPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurPagoBO> GetBy(Expression<Func<TFurPago, bool>> filter)
        {
            IEnumerable<TFurPago> listado = base.GetBy(filter);
            List<FurPagoBO> listadoBO = new List<FurPagoBO>();
            foreach (var itemEntidad in listado)
            {
                FurPagoBO objetoBO = Mapper.Map<TFurPago, FurPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurPagoBO FirstById(int id)
        {
            try
            {
                TFurPago entidad = base.FirstById(id);
                FurPagoBO objetoBO = new FurPagoBO();
                Mapper.Map<TFurPago, FurPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurPagoBO FirstBy(Expression<Func<TFurPago, bool>> filter)
        {
            try
            {
                TFurPago entidad = base.FirstBy(filter);
                FurPagoBO objetoBO = Mapper.Map<TFurPago, FurPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurPagoBO> listadoBO)
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

        public bool Update(FurPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurPagoBO> listadoBO)
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
        private void AsignacionId(TFurPago entidad, FurPagoBO objetoBO)
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

        private TFurPago MapeoEntidad(FurPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurPago entidad = new TFurPago();
                entidad = Mapper.Map<FurPagoBO, TFurPago>(objetoBO,
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
        ///  Obtiene todos los pagos de un respectivo FUR.
        /// </summary>
        /// <returns></returns>
        public int ObtenerFurPago (int? IdFur)
        {
            try
            {
                //FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                var correlativo = GetBy(x => x.Estado == true && x.IdFur == IdFur, x => new { x.NumeroPago }).ToList();

                if (correlativo.Count() == 0  || correlativo == null)
                {
                    return 1;
                }
                else
                {
                    var correlativo2 = correlativo.Max(x => x.NumeroPago);

                    if (correlativo2 > 0)
                    {
                        return correlativo2 + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        ///  Obtiene todos la lista de furpagos relacionado a un fur.
        /// </summary>
        /// <returns></returns>
        public List<FurPagoDTO> ObtenerListaFurPagos(int IdFur)
        {
            try
            {
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                List<FurPagoDTO> furPago = new List<FurPagoDTO>();
                var _query = "SELECT NumeroPago, IdFormaPago, NumeroCuenta, NumeroRecibo, IdMoneda, FechaCobroBanco, PrecioToTalMonedaOrigen, PrecioTotalMonedaDolares FROM fin.V_ObtenerFurPago WHERE Id = @IdFur AND Estado = 1";
                var furPagoDB = _dapper.QueryDapper(_query, new { IdFur });
                if (!string.IsNullOrEmpty(furPagoDB) && !furPagoDB.Contains("[]"))
                {
                    furPago = JsonConvert.DeserializeObject<List<FurPagoDTO>>(furPagoDB);
                }
                return furPago;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        ///  Obtiene todos el tipo de moneda utilizados en el FurPago.
        /// </summary>
        /// <returns></returns>
        public List<MonedaFurPagoDTO> ObtenerTipoMonedaFurPago()
        {
            try
            {
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                List<MonedaFurPagoDTO> monedaFurPago = new List<MonedaFurPagoDTO>();
                var _query = "SELECT DISTINCT Id, Nombre FROM fin.V_ObtenerTipoMonedaFurPago WHERE EstadoMoneda = 1 AND EstadoPago = 1";
                var monedaFurPagoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(monedaFurPagoDB) && !monedaFurPagoDB.Contains("[]"))
                {
                    monedaFurPago = JsonConvert.DeserializeObject<List<MonedaFurPagoDTO>>(monedaFurPagoDB);
                }
                return monedaFurPago;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        ///  Obtiene todos la lista de estado cancelado o pendiente relacionado a los fur.
        /// </summary>
        /// <returns></returns>
        public List<EstadoFurPagoDTO> ObtenerEstadoFurPago()
        {
            try
            {
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                List<EstadoFurPagoDTO> estadoFurPago = new List<EstadoFurPagoDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_ObtenerTipoCanceladoFur";
                var estadoFurPagoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(estadoFurPagoDB) && !estadoFurPagoDB.Contains("[]"))
                {
                    estadoFurPago = JsonConvert.DeserializeObject<List<EstadoFurPagoDTO>>(estadoFurPagoDB);
                }
                return estadoFurPago;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        ///  Obtiene todos la lista de furpagos relacionado a un fur.
        /// </summary>
        /// <returns></returns>
        public List<ListaFurPagoDTO> BuscarListaFurPagos(int? area, int? ciudad, int? anio, int? semana, int? moneda, bool? estado)
        {
            try
            {
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                List<ListaFurPagoDTO> listaFurPago = new List<ListaFurPagoDTO>();

                var _query = "SELECT IdFur, Codigo, IdProveedor, NombreProveedor, NombreProveedorComprobante, IdProducto, NombreProducto, IdCC, IdPais, NombreCentroCosto, NumeroCuenta, DescripcionCuenta, Cantidad, MonedaFur, NombreMonedaFur, PrecioUnitarioSoles, PrecioUnitarioDolares, PrecioTotalSoles, PrecioTotalDolares, IdDocumentoPago, NombreDocumento, NumeroRecibo, Descripcion, NumeroComprobante, FechaEfectivo, PagoMonedaOrigen, PagoDolares, Usuario, FechaModificacion, IdPersonaTrabajo, IdCiudad, NumeroSemana, EstadoCancelado, FechaAnio, MonedaPagoRealizado, NombreMonedaPagoRealizado FROM fin.V_ObtenerFurConFurPago WHERE (IdPersonaTrabajo = @area OR @area is null) AND (IdCiudad = @ciudad OR @ciudad is null) AND (NumeroSemana = @semana OR @semana is null) AND (EstadoCancelado = @estado OR @estado is null) AND (FechaAnio = @anio OR @anio is null) AND (MonedaPagoRealizado = @moneda OR @moneda is null) AND FaseAprobacion = 5 AND (Antiguo = 0 OR Antiguo is null) AND Estado = 1 AND EstadoMoneda = 1 AND EstadoCiudad = 1 AND EstadoArea = 1";
                var listaFurPagoDB = _dapper.QueryDapper(_query, new { area, ciudad, anio, semana, moneda, estado });
                if (!string.IsNullOrEmpty(listaFurPagoDB) && !listaFurPagoDB.Contains("[]"))
                {
                    listaFurPago = JsonConvert.DeserializeObject<List<ListaFurPagoDTO>>(listaFurPagoDB);
                }
                return listaFurPago;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int obtenerNumeroPagoByFur(int idFur)
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.IdFur == idFur).Max(x => x.NumeroPago);
            }
            catch (Exception e)
            {
                return  0;
            }
        }


        /// <summary>
        ///  Obtiene una lista de algunos campos de FurPago seleccionados por el idFur.
        /// </summary>
        /// <returns></returns>
        public List<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur)
        {
            try
            {
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                List<FurPagoRealizadoDTO> furPagoRealizado = new List<FurPagoRealizadoDTO>();
                var _query = "SELECT Id, IdComprobantePagoPorFur, NombreComprobantePagoPorFur, NumeroPago, IdMoneda, NombreMoneda, NumeroCuenta, NumeroRecibo, IdFormaPago, NombreFormaPago, FechaCobroBanco, PrecioTotalMonedaOrigen, PrecioTotalMonedaDolares, IdCancelado, NombreCancelado FROM fin.V_ObtenerPagosRealizadosPorFur WHERE IdFur = @IdFur AND Estado = 1";
                var furPagoRealizadoDB = _dapper.QueryDapper(_query, new { IdFur = IdFur });
                if (!string.IsNullOrEmpty(furPagoRealizadoDB) && !furPagoRealizadoDB.Contains("[]"))
                {
                    furPagoRealizado = JsonConvert.DeserializeObject<List<FurPagoRealizadoDTO>>(furPagoRealizadoDB);
                }
                return furPagoRealizado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        ///  Genera el Reporte de Estado de Cuenta por Proveedor
        /// </summary>
        /// <returns></returns>
        public List<ReporteEstadoCuentaProveedorDTO> GenerarReporteEstadoCuentaProveedor(string IdsSede, string IdsPlanContable, string IdsProveedor)
        {
            try
            {
                List<ReporteEstadoCuentaProveedorDTO> EstadoCuentaProveedor = new List<ReporteEstadoCuentaProveedorDTO>();
                //var _query = "exec [fin].[SP_GenerarReporteEstadoCuentaProveedor] '" + IdsSede + "' , '" + IdsProveedor + "' , '" + IdsPlanContable + "'";

                var _query = "";
                if (IdsSede.Trim().Equals("") && IdsPlanContable.Trim().Equals("") && IdsProveedor.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor";
                } else if (IdsSede.Trim().Equals("") && IdsPlanContable.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdProveedor in (" + IdsProveedor + ")"; 
                } else if (IdsSede.Trim().Equals("") && IdsProveedor.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdPlanContable in (" + IdsPlanContable + ")";
                } else if ( IdsPlanContable.Trim().Equals("") && IdsProveedor.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdSede in (" + IdsSede + ")";
                } else if (IdsSede.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdProveedor in (" + IdsProveedor + ") and IdPlanContable in ( "+ IdsPlanContable +" )";
                }
                else if (IdsProveedor.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdSede in (" + IdsSede + ") and IdPlanContable in ("+IdsPlanContable+")";
                }
                else if (IdsPlanContable.Trim().Equals(""))
                {
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdSede in (" + IdsSede + ") and IdProveedor in ("+IdsProveedor+")";
                } else
                    _query = "select * from fin.V_ReporteEstadoCuentaProveedor where IdSede in (" + IdsSede + ") and IdProveedor in ("+IdsProveedor+") and IdPlanContable in ("+IdsPlanContable+")";
                {

                }



                var EstadoCuentaProveedorDB = _dapper.QueryDapper(_query, null);
                if (!EstadoCuentaProveedorDB.Contains("[]") && !string.IsNullOrEmpty(EstadoCuentaProveedorDB))
                {
                    EstadoCuentaProveedor = JsonConvert.DeserializeObject<List<ReporteEstadoCuentaProveedorDTO>>(EstadoCuentaProveedorDB);
                }
                return EstadoCuentaProveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
       
    }
}
