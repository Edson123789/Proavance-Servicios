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
    public class FurRepositorio : BaseRepository<TFur, FurBO>
    {
        #region Metodos Base
        public FurRepositorio() : base()
        {
        }
        public FurRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurBO> GetBy(Expression<Func<TFur, bool>> filter)
        {
            IEnumerable<TFur> listado = base.GetBy(filter);
            List<FurBO> listadoBO = new List<FurBO>();
            foreach (var itemEntidad in listado)
            {
                FurBO objetoBO = Mapper.Map<TFur, FurBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurBO FirstById(int id)
        {
            try
            {
                TFur entidad = base.FirstById(id);
                FurBO objetoBO = new FurBO();
                Mapper.Map<TFur, FurBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurBO FirstBy(Expression<Func<TFur, bool>> filter)
        {
            try
            {
                TFur entidad = base.FirstBy(filter);
                FurBO objetoBO = Mapper.Map<TFur, FurBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFur entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurBO> listadoBO)
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

        public bool Update(FurBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFur entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurBO> listadoBO)
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
        private void AsignacionId(TFur entidad, FurBO objetoBO)
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

        private TFur MapeoEntidad(FurBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFur entidad = new TFur();
                entidad = Mapper.Map<FurBO, TFur>(objetoBO,
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

        public List<ProductoFurDTO> ObtenerProductoFur(int IdProveedor)
        {
            try
            {
                List<ProductoFurDTO> productoInformacion = new List<ProductoFurDTO>();
                var _query = "SELECT IdProducto,Producto,IdProveedor,CuentaContable,Cuenta,IdCantidad,Cantidad,IdMoneda,CostoOriginal,CostoDolares,PrecioProducto,IdCondicionTipoPago FROM FIN.V_ObtenerProductosFur where IdProveedor=@IdProveedor";
                var productoFurDB = _dapper.QueryDapper(_query, new { IdProveedor});
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    productoInformacion = JsonConvert.DeserializeObject<List<ProductoFurDTO>>(productoFurDB);
                }
                return productoInformacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ProductoFurDTO ObtenerHistoricoUltimaVersion(int IdProveedor,int IdProducto)
        {
            try
            {
                ProductoFurDTO productoInformacion = new ProductoFurDTO();
                var _query = "SELECT top 1 IdProducto,Producto,IdProveedor,Proveedor,CuentaContable,Cuenta,IdCantidad,Cantidad,IdMoneda,CostoOriginal,CostoDolares,PrecioProducto,IdCondicionTipoPago FROM FIN.V_ObtenerProductosFur where IdProveedor=@IdProveedor and IdProducto=@IdProducto order by IdHistorico desc";
                var productoFurDB = _dapper.FirstOrDefault(_query, new { IdProveedor,IdProducto });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    productoInformacion = JsonConvert.DeserializeObject<ProductoFurDTO>(productoFurDB);
                }
                return productoInformacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FurDTO> ObtenerFursBusquedaCodigo(string codigo)
        {
            try
            {
                List<FurDTO> ListaFurByCodigo = new List<FurDTO>();
                var camposTabla = "SELECT Id,Codigo,IdCentroCosto,CentroCosto,Programa,IdCiudad,IdFurTipoPedido,NumeroSemana,IdProveedor,RazonSocial,IdProducto,Producto,IdProductoPresentacion,ProductoPresentacion,IdMoneda_Proveedor,FechaLimite,Descripcion,NumeroCuenta,Cuenta,Cantidad,IdFaseAprobacion1,FaseAprobacion1,PrecioUnitarioMonedaOrigen,PrecioUnitarioDolares,PrecioTotalMonedaOrigen,PrecioTotalDolares,UsuarioCreacion,UsuarioModificacion,FechaModificacion,Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo,IdCondicionTipoPago,MonedaPagoReal, IdEmpresa ";                
                var _query =camposTabla+ " FROM FIN.V_ObtenerFurFinanzas where Codigo like CONCAT(@Codigo,'%') ORDER BY NumeroSemana,Id";
                var productoFurDB = _dapper.QueryDapper(_query, new { codigo });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    ListaFurByCodigo = JsonConvert.DeserializeObject<List<FurDTO>>(productoFurDB);
                }
                return ListaFurByCodigo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FurDTO> ObtenerFursParaGrid(int IdArea,string Codigo, int IdRol,int IdEstadoFaseAprobacion1,int pageSize,int skip,string UserName, int modo, int? anio, int? semana, int? idCiudad)
        {
            try
                {
				var condiciones = "";
				if(anio != null)
				{
					condiciones += " AND Anio = @anio";
				}
				if(semana != null)
				{
					condiciones += " AND NumeroSemana = @semana";
				}
				if (idCiudad != null)
				{
					condiciones += " AND IdCiudad = @idCiudad";
				}
                var _query="";
                var camposTabla = "Id,Codigo,IdCentroCosto,CentroCosto,Programa,IdCiudad,IdFurTipoPedido,NumeroSemana,IdProveedor,RazonSocial,IdProducto,Producto,IdProductoPresentacion,ProductoPresentacion,IdMoneda_Proveedor,FechaLimite,Descripcion,NumeroCuenta,Cuenta,Cantidad,IdFaseAprobacion1,FaseAprobacion1,PrecioUnitarioMonedaOrigen,PrecioUnitarioDolares,PrecioTotalMonedaOrigen,PrecioTotalDolares,UsuarioCreacion,UsuarioModificacion,FechaModificacion,Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo,IdCondicionTipoPago, MonedaPagoReal,IdEmpresa";
                
                List<FurDTO> FurFinanzas = new List<FurDTO>();
                if (IdEstadoFaseAprobacion1 !=ValorEstatico.IdFurProyectado)
                {
                    if (!Codigo.Equals(""))
                    {
                        if (IdEstadoFaseAprobacion1 == ValorEstatico.IdFurTodosPorEliminar || IdEstadoFaseAprobacion1 == ValorEstatico.IdMisFurPorEliminar)
                        {
                            if (UserName.Equals(""))
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc";
                            }
                            else {
                                _query = "SELECT " +camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and UsuarioCreacion=@UserName and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc";
                            }
                        }
                        else {
                            _query = "SELECT " + camposTabla +
                                " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc ";// ORDER BY NumeroSemana,Id OFFSET  @skip ROWS FETCH NEXT  @pageSize ROWS ONLY
                        }
                    }
                    else {
                        if (IdEstadoFaseAprobacion1 == ValorEstatico.IdFurTodosPorEliminar || IdEstadoFaseAprobacion1 == ValorEstatico.IdMisFurPorEliminar)
                        {
                            if (UserName.Equals(""))
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1'ORDER BY Id desc";
                            }
                            else {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and UsuarioCreacion=@UserName ORDER BY Id desc";
                            }
                        }
                        else {
                            _query = "SELECT " + camposTabla +
                                " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea  ORDER BY Id desc";
                        }
                    }

                }
                else {
                    if (modo == 1)
                    {
                        if (UserName == "sruizope" || UserName == "sruizop")
                        {
                            _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                            " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)))  ORDER BY Id desc";
                        }
                        else
                        {
                            _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                            " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";

                        }

                    }
                    else {
                        _query = "SELECT " + camposTabla +
                            " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 " +condiciones+
                            " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";
                    }
                    if (modo == 10) {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                            " ORDER BY Id desc";
                    }
                    if (modo == 11)
                    {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                             " and FechaLimite< dateadd(month,2, DATEADD(MM, DATEDIFF(MM,0,GETDATE()), 0) ) order by convert(date,FechaLimite) desc";
                    }
                }                    

                var FurDB = _dapper.QueryDapper(_query, new { IdArea, Codigo, IdRol, IdEstadoFaseAprobacion1, pageSize, skip, UserName , modo, anio, semana, idCiudad});
                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    FurFinanzas = JsonConvert.DeserializeObject<List<FurDTO>>(FurDB);
                }

                return FurFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AprobarFurProyectado(FiltroDTO json,string AreaTrabajo,string Ciudad, integraDBContext context) {
            try
            {
                FurRepositorio repFurRep = new FurRepositorio(context);                
                FurBO fur = new FurBO();
                fur = repFurRep.FirstById(json.Id);
                
                int semana = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(fur.FechaLimite.Value, CalendarWeekRule.FirstDay, fur.FechaLimite.Value.DayOfWeek);
                fur.NumeroSemana = (fur.NumeroSemana == null || fur.NumeroSemana==0) ? semana : fur.NumeroSemana;
                string anio = fur.FechaLimite.Value.Year.ToString();
                string codigo = "";
                if (fur.NumeroSemana > 0 && fur.NumeroSemana < 10)
                {
                    codigo = anio.Substring(2) + "-" + Ciudad.Substring(0, 1) + "-" + AreaTrabajo + "-" +"0"+ fur.NumeroSemana + "-";
                }
                else
                {
                    codigo = anio.Substring(2) + "-" + Ciudad.Substring(0, 1) + "-" + AreaTrabajo + "-" + fur.NumeroSemana + "-";
                }
                int correlativo = 0;
                var listCodigos = repFurRep.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }
                fur.Codigo = (fur.Codigo==null || fur.Codigo=="" )?codigo + correlativo: fur.Codigo;
                fur.NumeroFur =fur.NumeroFur==null? fur.Codigo: fur.NumeroFur;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurEstadoPorAprobar;
                fur.UsuarioSolicitud = json.Nombre;
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = json.Nombre;

                repFurRep.Update(fur);

                return true;
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
        public object ObtenerFurPorAprobar(int IdArea, string Codigo, int IdRol,int tipo)
        {
            try
            {
                var _query = "";
                var _whereSQL = "";
                var camposTabla = "Id,Codigo,CentroCosto,Programa,RazonSocial,Producto ,IdMoneda_Proveedor,Descripcion,Cantidad,PrecioUnitarioMonedaOrigen,PrecioUnitarioDolares,PrecioTotalMonedaOrigen,PrecioTotalDolares,Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo, MonedaPagoReal , convert(datetime,FechaLimite) as FechaLimite,FurTipoPedido,UsuarioSolicitud ";
                if (Codigo.Equals("")) {
                    if (tipo == 0)
                    {
                        if (IdRol == ValorEstatico.IdRolJefePlanificacion)
                        {
                            _whereSQL=" FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and (IdPersonalAreaTrabajo=@IdArea or IdPersonalAreaTrabajo=4) and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                        else
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and IdPersonalAreaTrabajo=@IdArea and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                    }
                    else {
                        _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=3 ";
                        _query = "Select " + camposTabla + _whereSQL;
                    }
                }
                else {
                    if (tipo == 0)
                    {
                        if (IdRol == ValorEstatico.IdRolJefePlanificacion)
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and Codigo like CONCAT(@Codigo,'%') and (IdPersonalAreaTrabajo=@IdArea or IdPersonalAreaTrabajo=4 ) and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                        else
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and Codigo like CONCAT(@Codigo,'%') and IdPersonalAreaTrabajo=@IdArea and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                    }
                    else
                    {
                        _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=3  and Codigo like CONCAT(@Codigo,'%')";
                        _query = "Select " + camposTabla + _whereSQL;
                    }
                }
                List<FurPorAprobarDTO> ListaFur = new List<FurPorAprobarDTO>();
                var totalFur = new Dictionary<string, int>();
                _query += " ORDER BY Id desc ";
                var _queryCount ="Select count (Id) as Total "+ _whereSQL;
                var totalElementosDB= _dapper.FirstOrDefault(_queryCount, new { IdArea, Codigo, IdRol});
                var FurDB = _dapper.QueryDapper(_query, new { IdArea, Codigo, IdRol});
                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurPorAprobarDTO>>(FurDB);
                    totalFur= JsonConvert.DeserializeObject<Dictionary<string,int>>(totalElementosDB);
                }
                return new { ListaFur, Total = totalFur.Select(x => x.Value).FirstOrDefault() };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EliminarFur(FiltroDTO FurEliminarDTO, integraDBContext _integraDBContext) {
            FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
            if (repFurRep.Exist(FurEliminarDTO.Id))
            {
                repFurRep.Delete(FurEliminarDTO.Id, FurEliminarDTO.Nombre);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Actualiza el estado de los furs que han sido aprobados pero que no hayan sido ajecutados a 
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool ActualizarFurEstadoAprobadoNoEjecutado(FiltroDTO FurEliminarDTO, integraDBContext _integraDBContext)
        {
            try
            {
                FurRepositorio _repFurRep = new FurRepositorio(_integraDBContext);
                FurBO fur = new FurBO();
                fur = _repFurRep.FirstById(FurEliminarDTO.Id);

                if (fur == null)
                    throw new Exception("No se encontro el registro de 'Fur' que se quiere actualizar");

                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurAprobadoNoEjecutado;
                fur.UsuarioModificacion = FurEliminarDTO.Nombre;
                fur.FechaModificacion = DateTime.Now;

                _repFurRep.Update(fur);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta Furs con estado proyectado, 
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurImportado(FurDTO Json,integraDBContext _integraDBContext) {
            try
            {
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                fur.IdCiudad = Json.IdCiudad;
                fur.IdProducto = Json.IdProducto;
                fur.IdProveedor = Json.IdProveedor;
                fur.Cantidad = Json.Cantidad;
                fur.IdProductoPresentacion = Json.IdProductoPresentacion;
                fur.IdCentroCosto = Json.IdCentroCosto;
                fur.IdMonedaProveedor = Json.IdMoneda_Proveedor;
                fur.NumeroCuenta = Json.NumeroCuenta;
                fur.Cuenta = Json.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = Json.PrecioUnitarioMonedaOrigen;
                fur.PrecioUnitarioDolares = Json.PrecioUnitarioDolares;
                fur.PrecioTotalMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                fur.PrecioTotalDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = DateTime.Parse(Json.FechaLimite);
                fur.EstadoAprobadoObservado = false;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoACredito;
                fur.NumeroSemana = Json.NumeroSemana;
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = Json.UsuarioModificacion;
                fur.Descripcion = "Fur-Proyectado";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = Json.IdMoneda_Proveedor;
                fur.IdMonedaPagoRealizado = Json.IdMoneda_Proveedor;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.Estado = true;
                fur.UsuarioCreacion = Json.UsuarioCreacion;
                fur.UsuarioModificacion = Json.UsuarioModificacion;
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;

                repFurRep.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados y sean aprobados por jefe de Finanzas
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerDatosFur(string codigo)
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo) && x.Cancelado==false && x.OcupadoSolicitud==false && x.IdFurFaseAprobacion1==ValorEstatico.IdFurAprobadoPorJefeFinanzas && x.Antiguo==0, 
                    x => new  { Id = x.Id, Codigo = x.Codigo.ToUpper(),Detalle=x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados, no sean rendidos 
        /// y sean aprobados por jefe de Finanzas, se usa para autocomplete de generacionCajaRec
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerFursAutocompleteREC(string codigo)
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo) && x.Cancelado == false && x.OcupadoRendicion==false && x.OcupadoSolicitud == false && x.IdFurFaseAprobacion1 == ValorEstatico.IdFurAprobadoPorJefeFinanzas && x.Antiguo == 0,
                    x => new { Id = x.Id, Codigo = x.Codigo.ToUpper(), Detalle = x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
        /// <summary>
        /// Obtiene la Lista de Fur (para autocomplete) apropiados para el modulo de 'RendicionRequerimientos'
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<FurDTO> ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera)
        {
            try
            {
                List<FurDTO> furs = new List<FurDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Codigo, Descripcion FROM [fin].[V_ObtenerFursDisponiblesParaRendirCaja] where Codigo like '%" + codigo+"%' AND Estado=1 AND Cancelado = 0 and OcupadoRendicion = 0 AND IdFurFaseAprobacion_1="+ ValorEstatico.IdFurAprobadoPorJefeFinanzas + " AND Antiguo = 0 AND IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;
                var fursDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(fursDB) && !fursDB.Contains("[]"))
                {
                    furs = JsonConvert.DeserializeObject<List<FurDTO>>(fursDB);
                }
                return furs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CambiarEstadoFurSolicitudCajaPR(int? idFur)
        {
            try
            {
                FurBO fur = new FurBO();
                fur = this.FirstById(idFur.Value);
                fur.OcupadoSolicitud = false;
                this.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActualizarEstadoOcupadoFur(int idFur, integraDBContext context)
        {
            try
            {
                FurRepositorio _repFurRep = new FurRepositorio(context);
                FurBO fur = new FurBO();
                fur = _repFurRep.FirstById(idFur);

                if (fur == null)
                    throw new Exception("No se encontro el registro de 'Fur' que se quiere actualizar");

                fur.OcupadoSolicitud = true;

                _repFurRep.Update(fur);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		/// <summary>
		/// Obtiene el valor correlativo para asignarle al siguiente fur a generar
		/// </summary>
		/// <param name="anio"></param>
		/// <param name="semana"></param>
		/// <param name="areaAbrev"></param>
		/// <returns></returns>
		public CorrelativoDTO ObtenerCorrelativo (string anio, string semana, string areaAbrev)
		{
			try
			{
				var res = _dapper.QuerySPFirstOrDefault("fin.SP_FURObtenerCorrelativoNuevo", new { Anio = anio, Semana = semana, Area = areaAbrev});
				return JsonConvert.DeserializeObject<CorrelativoDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene detalle de fur de cronograma de sesiones
		/// </summary>
		/// <param name="idPespecifico"></param>
		/// <param name="grupo"></param>
		/// <returns></returns>
		public List<ProgramaEspecificoFURDTO> ObtenerFurProgramaEspecifico(int idPespecifico, int grupo, bool esDocente)
		{
			try
			{
				string condicionDocente = "";
				if (esDocente)
				{
					condicionDocente = " AND NumeroCuenta IN ('6314090','6321001')";
				}
				var query = "SELECT DISTINCT Id, Codigo, Proveedor, Producto, CentroCosto, Unidades, Descripcion, Ciudad, IdProveedor, IdProducto, IdCentroCosto, IdPersonalAreaTrabajo, IdCiudad FROM " +
					"fin.V_ObtenerFurGeneradosPEspecifico WHERE Estado = 1 AND IdPespecificoPadre = @IdPespecifico AND IdFurFaseAprobacion IN (1,2)"+ condicionDocente;
				var res = _dapper.QueryDapper(query, new { IdPespecifico = idPespecifico});
				if(res == "[]")
				{
					query = "SELECT DISTINCT Id, Codigo, Proveedor, Producto, CentroCosto, Unidades, Descripcion, Ciudad, IdProveedor, IdProducto, IdCentroCosto, IdPersonalAreaTrabajo, IdCiudad FROM " +
					"fin.V_ObtenerFurGeneradosPEspecifico WHERE Estado = 1 AND IdPespecifico = @IdPespecifico AND IdFurFaseAprobacion IN (1,2)"+ condicionDocente;
					res = _dapper.QueryDapper(query, new { IdPespecifico = idPespecifico });
				}
				return JsonConvert.DeserializeObject<List<ProgramaEspecificoFURDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Se obtiene el proveedor por el idFur
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public int? ObtenerProveedor(int idFur)
        {
            try
            {
                var idProveedor = GetBy(x => x.Id == idFur && x.Estado == true).FirstOrDefault().IdProveedor;

                return idProveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Se obtiene algunos datos del fur por el idFur
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public PagoMasivoDatosDTO ObtenerPagosBloquePorFurs (int idFur)
        {
            try
            {
                var query = "SELECT Id, Codigo, Monto, IdMoneda, Moneda, MontoAmortizar FROM " +
                    "fin.V_ObtenerPagosBloquePorFur WHERE Id = @idFur AND Estado = 1";
                var res = _dapper.FirstOrDefault(query, new { idFur });
                return JsonConvert.DeserializeObject<PagoMasivoDatosDTO>(res);
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public  List<FiltroDTO> ObtenerIdFurAprobadoPorEliminar()
        {
            try
            {
                List<FiltroDTO> ListIdFurEliminar = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "select IdFur as Id ,UserEliminacion as Nombre from [fin].[V_FurDatosParaEliminacionAutomatica] where (IdFaseAprobacion=" + ValorEstatico.IdFurProyectado + " and fechaEliminacionProyectado<=convert(date,getdate()) and TienePago=0 and TieneComprobante=0)";

                var fursDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(fursDB) && !fursDB.Contains("[]"))
                {
                    ListIdFurEliminar = JsonConvert.DeserializeObject<List<FiltroDTO>>(fursDB);
                }
                return ListIdFurEliminar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FiltroDTO> ObtenerIdFurAprobadoNoEjecutado()
        {
            try
            {
                List<FiltroDTO> ListIdFurEliminar = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "select IdFur as Id ,UserEliminacion as Nombre from [fin].[V_FurDatosParaEliminacionAutomatica] where (IdFaseAprobacion=" + ValorEstatico.IdFurAprobadoPorJefeFinanzas + " and fechaEliminacionAprobado<=convert(date,getdate()) and TienePago=0 and TieneComprobante=0)";

                var fursDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(fursDB) && !fursDB.Contains("[]"))
                {
                    ListIdFurEliminar = JsonConvert.DeserializeObject<List<FiltroDTO>>(fursDB);
                }
                return ListIdFurEliminar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Inserta Furs automaticos para los sueldos de planillas
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurAutomaticoPlanilla(DatoPlanillaCreacionFurDTO Json,DateTime fechaCreacionFur, integraDBContext _integraDBContext)
        {
            try
            {
                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = ValorEstatico.IdAreaTrabajoGestionPersonas;
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa) {
                    fur.IdCiudad = 4;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    fur.IdCiudad = 14;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonalLima;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                {
                    fur.IdCiudad = 1956;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                {
                    fur.IdCiudad = 2066;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                fur.IdProducto = ValorEstatico.IdProductoPlanillaAdministracion;
                fur.Descripcion = "SUELDOS Y SALARIOS - PROD - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoVentas)
                {
                    fur.IdProducto = ValorEstatico.IdProductoPlanillaVentas;
                    fur.Descripcion = "SUELDOS Y SALARIOS - VTAS - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                }
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoOperaciones)
                {
                    fur.IdProducto = ValorEstatico.IdProductoPlanillaProduccion;
                    fur.Descripcion = "SUELDOS Y SALARIOS - PROD - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                }
                fur.IdProveedor = Json.IdProveedor;

                fur.Cantidad = Json.RemuneracionFija;
                decimal porcentajePensionario = new decimal(0.12);
                decimal asignacionFamiliar = new decimal(93.5);
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    if (Json.TieneAsignacionFamiliar)
                    {
                        fur.Cantidad += asignacionFamiliar;
                    }
                    if (Json.TieneSistemaPensionario && Json.PorcentajeSistemaPensionario>0)
                    {
                        fur.Cantidad -= (Json.PorcentajeSistemaPensionario/100) * fur.Cantidad;
                    }
                    if (Json.TieneSistemaPensionario && Json.PorcentajeSistemaPensionario <= 0)
                    {
                        fur.Cantidad -= porcentajePensionario * fur.Cantidad;
                    }
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                {
                    if (Json.TieneSistemaPensionario)
                    {
                        decimal aporteBolivia = new decimal(0.1271);
                        fur.Cantidad -= aporteBolivia * fur.Cantidad;
                    }
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                {
                    if (Json.TieneSistemaPensionario)
                    {
                        decimal aporteColombia = new decimal(0.08);
                        fur.Cantidad -= aporteColombia * fur.Cantidad;
                    }
                }
                var datosHistorico=ObtenerHistoricoUltimaVersion(ValorEstatico.IdProveedorPersonal,fur.IdProducto.Value);
                fur.IdProductoPresentacion = datosHistorico.IdCantidad;
                fur.IdMonedaProveedor = datosHistorico.IdMoneda;
                fur.NumeroCuenta = datosHistorico.CuentaContable;
                fur.Cuenta = datosHistorico.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = datosHistorico.CostoOriginal;
                fur.PrecioUnitarioDolares = datosHistorico.CostoDolares;
                fur.PrecioTotalMonedaOrigen = fur.PrecioUnitarioMonedaOrigen * fur.Cantidad;
                fur.PrecioTotalDolares = fur.PrecioUnitarioDolares * fur.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = fechaCreacionFur.AddDays(10);
                fur.Descripcion = fechaCreacionFur.AddDays(10).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper() + " " + fechaCreacionFur.AddDays(10).Year + " - " + fur.Descripcion;
                fur.EstadoAprobadoObservado = false;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoGastoInmediato;
                fur.NumeroSemana = fur.obtenerNumeroSemana(fur.FechaLimite.Value);
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = "PLANILLA-AUTOMATIC";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = datosHistorico.IdMoneda;
                fur.IdMonedaPagoRealizado = datosHistorico.IdMoneda;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.UsuarioCreacion = "PLANILLA-AUTOMATIC";
                fur.UsuarioModificacion = "PLANILLA-AUTOMATIC";
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.Estado = true;

                this.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta furs automaticos para las GRatificaciones dos veces al año
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurAutomaticoGratificacion(DatoPlanillaCreacionFurDTO Json, DateTime fechaCreacionFur, integraDBContext _integraDBContext)
        {
            try
            {

                var MesDiferencia = ((fechaCreacionFur.Year - DateTime.Now.Date.Year) * 12) + fechaCreacionFur.Month - DateTime.Now.Date.Month;
                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = ValorEstatico.IdAreaTrabajoGestionPersonas;
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa)
                {
                    fur.IdCiudad = 4;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    fur.IdCiudad = 14;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonalLima;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                {
                    fur.IdCiudad = 1956;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                {
                    fur.IdCiudad = 2066;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                fur.IdProducto = ValorEstatico.IdProductoGratificacionAdministracion;
                fur.Descripcion = "GRATIFICACIONES - ADM - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoVentas)
                {
                    fur.IdProducto = ValorEstatico.IdProductoGratificacionVentas;
                    fur.Descripcion = "GRATIFICACIONES - VTAS - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                }
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoOperaciones)
                {
                    fur.IdProducto = ValorEstatico.IdProductoGratificacionProduccion;
                    fur.Descripcion = "GRATIFICACIONES - PROD  - Remuneraciones  - Cargas de Personal, Directores y Gerentes";
                }
                fur.IdProveedor = Json.IdProveedor;
                decimal porcentajeEssalud = new decimal(0.09);
                decimal asignacionFamiliar = new decimal(93.5);
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima || Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa)
                {
                    Json.NroMesesBeneficio = Json.NroMesesBeneficio + 1+ MesDiferencia;
                    if (Json.NroMesesBeneficio >= 6)
                    {
                        fur.Cantidad = Json.RemuneracionFija;

                        if (Json.TieneAsignacionFamiliar)
                        {
                            fur.Cantidad += asignacionFamiliar;
                        }
                        fur.Cantidad += (porcentajeEssalud * fur.Cantidad);
                    }
                    else if (Json.NroMesesBeneficio < 6)
                    {
                        fur.Cantidad = (Json.RemuneracionFija / 6) * Json.NroMesesBeneficio;
                        if (Json.TieneAsignacionFamiliar)
                        {
                            fur.Cantidad += asignacionFamiliar;
                        }
                        fur.Cantidad += (porcentajeEssalud * fur.Cantidad);

                    }
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota || Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz) {
                    fur.Cantidad = Json.RemuneracionFija;
                }
                var datosHistorico = ObtenerHistoricoUltimaVersion(ValorEstatico.IdProveedorPersonal, fur.IdProducto.Value);
                fur.IdProductoPresentacion = datosHistorico.IdCantidad;
                fur.IdMonedaProveedor = datosHistorico.IdMoneda;
                fur.NumeroCuenta = datosHistorico.CuentaContable;
                fur.Cuenta = datosHistorico.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = datosHistorico.CostoOriginal;
                fur.PrecioUnitarioDolares = datosHistorico.CostoDolares;
                fur.PrecioTotalMonedaOrigen = fur.PrecioUnitarioMonedaOrigen * fur.Cantidad;
                fur.PrecioTotalDolares = fur.PrecioUnitarioDolares * fur.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = fechaCreacionFur.AddDays(10);
                fur.Descripcion = fechaCreacionFur.AddDays(10).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper() + " " + fechaCreacionFur.AddDays(10).Year + " - " + fur.Descripcion;
                fur.EstadoAprobadoObservado = false;
                fur.UsuarioCreacion = "GRATI-AUTOMATIC";
                fur.UsuarioModificacion = "GRATI-AUTOMATIC";
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoGastoInmediato;
                fur.NumeroSemana = fur.obtenerNumeroSemana(fur.FechaLimite.Value);
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = "GRATI-AUTOMATIC";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = datosHistorico.IdMoneda;
                fur.IdMonedaPagoRealizado = datosHistorico.IdMoneda;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.Estado = true;

                this.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta Furs para calcular CTS de personal
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurAutomaticoCTS(DatoPlanillaCreacionFurDTO Json, DateTime fechaCreacionFur, integraDBContext _integraDBContext)
        {
            try
            {

                var MesDiferencia = ((fechaCreacionFur.Year - DateTime.Now.Date.Year) * 12) + fechaCreacionFur.Month - DateTime.Now.Date.Month;

                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = ValorEstatico.IdAreaTrabajoGestionPersonas;
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa)
                {
                    fur.IdCiudad = 4;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    fur.IdCiudad = 14;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonalLima;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                {
                    fur.IdCiudad = 1956;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                    fur.Cantidad = Json.RemuneracionFija / 2;
                }
                fur.IdProducto = ValorEstatico.IdProductoCTSAdministracion;
                fur.Descripcion = "CTS  - ADM - Beneficios Social de los Trabajadores";
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoVentas)
                {
                    fur.IdProducto = ValorEstatico.IdProductoCtsVentas;
                    fur.Descripcion = "CTS  - VTAS - Beneficios Social de los Trabajadores";
                }
                if (Json.AreaTrabajo == ValorEstatico.AreaTrabajoOperaciones)
                {
                    fur.IdProducto = ValorEstatico.IdProductoCtsProduccion;
                    fur.Descripcion = "CTS  - PROD - Beneficios Social de los Trabajadores";
                }
                fur.IdProveedor = Json.IdProveedor;
                Json.NroMesesBeneficio = Json.NroMesesBeneficio + 1 + MesDiferencia;
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa || Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    if (Json.NroMesesBeneficio >= 6)
                    {
                        fur.Cantidad = Json.RemuneracionFija / 2;
                        if (Json.NroMesesBeneficio >= 12)
                        {
                            fur.Cantidad += (1 / 6 * (Json.RemuneracionFija));
                        }
                    }
                    else if (Json.NroMesesBeneficio < 6)
                    {
                        fur.Cantidad = (Json.RemuneracionFija / 365) * (Json.NroDiasBeneficio+30+(30*MesDiferencia));
                    }
                }
                var datosHistorico = ObtenerHistoricoUltimaVersion(ValorEstatico.IdProveedorPersonal, fur.IdProducto.Value);
                fur.IdProductoPresentacion = datosHistorico.IdCantidad;
                fur.IdMonedaProveedor = datosHistorico.IdMoneda;
                fur.NumeroCuenta = datosHistorico.CuentaContable;
                fur.Cuenta = datosHistorico.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = datosHistorico.CostoOriginal;
                fur.PrecioUnitarioDolares = datosHistorico.CostoDolares;
                fur.PrecioTotalMonedaOrigen = fur.PrecioUnitarioMonedaOrigen * fur.Cantidad;
                fur.PrecioTotalDolares = fur.PrecioUnitarioDolares * fur.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = fechaCreacionFur.AddDays(10);
                fur.Descripcion = fechaCreacionFur.AddDays(10).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper()+" "+ fechaCreacionFur.AddDays(10).Year + " - " + fur.Descripcion;
                fur.EstadoAprobadoObservado = false;
                fur.UsuarioCreacion = "CTS-AUTOMATIC";
                fur.UsuarioModificacion = "CTS-AUTOMATIC";
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoGastoInmediato;
                fur.NumeroSemana = fur.obtenerNumeroSemana(fur.FechaLimite.Value);
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = "CTS-AUTOMATIC";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = datosHistorico.IdMoneda;
                fur.IdMonedaPagoRealizado = datosHistorico.IdMoneda;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.Estado = true;

                this.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Inserta Furs Calculando el Bono para los trabajadores 
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurAutomaticoBonos(DatoPlanillaCreacionFurDTO Json, DateTime fechaCreacionFur, integraDBContext _integraDBContext)
        {
            try
            {
                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = ValorEstatico.IdAreaTrabajoGestionPersonas;
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoArequipa)
                {
                    fur.IdCiudad = 4;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoLima)
                {
                    fur.IdCiudad = 14;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonalLima;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoSantaCruz)
                {
                    fur.IdCiudad = 2066;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (Json.IdSedeTrabajo == ValorEstatico.IdSedeTrabajoBogota)
                {
                    fur.IdCiudad = 1956;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                fur.IdProducto = ValorEstatico.IdProductoBonoProductividad;
                fur.Descripcion = "Bono de Productividad al Trabajador";
                fur.IdProveedor = Json.IdProveedor;
                fur.Cantidad = Json.BonoTotal;
                var datosHistorico = ObtenerHistoricoUltimaVersion(ValorEstatico.IdProveedorPersonal, fur.IdProducto.Value);
                fur.IdProductoPresentacion = datosHistorico.IdCantidad;
                fur.IdMonedaProveedor = datosHistorico.IdMoneda;
                fur.NumeroCuenta = datosHistorico.CuentaContable;
                fur.Cuenta = datosHistorico.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = datosHistorico.CostoOriginal;
                fur.PrecioUnitarioDolares = datosHistorico.CostoDolares;
                fur.PrecioTotalMonedaOrigen = fur.PrecioUnitarioMonedaOrigen * fur.Cantidad;
                fur.PrecioTotalDolares = fur.PrecioUnitarioDolares * fur.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = fechaCreacionFur.AddDays(10);
                fur.Descripcion = fechaCreacionFur.AddDays(10).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper() + " " + fechaCreacionFur.AddDays(10).Year + " - " + fur.Descripcion;
                fur.EstadoAprobadoObservado = false;
                fur.UsuarioCreacion = "BONO-AUTOMATIC";
                fur.UsuarioModificacion = "BONO-AUTOMATIC";
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoGastoInmediato;
                fur.NumeroSemana = fur.obtenerNumeroSemana(fur.FechaLimite.Value);
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = "BONO-AUTOMATIC";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = datosHistorico.IdMoneda;
                fur.IdMonedaPagoRealizado = datosHistorico.IdMoneda;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.Estado = true;

                this.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta fur del pago total de Essalud sobre todos los trabajadores
        /// </summary>
        /// <param name="Json"></param>
        /// <param name="fechaCreacionFur"></param>
        /// <param name="_integraDBContext"></param>
        /// <returns></returns>
        public bool InsertarFurAutomaticoEsSalud(int IdCiudad, decimal totalEsSalud ,DateTime fechaCreacionFur, integraDBContext _integraDBContext)
        {
            try
            {
                FurBO fur = new FurBO();

                fur.IdPersonalAreaTrabajo = ValorEstatico.IdAreaTrabajoGestionPersonas;
                if (IdCiudad == 4/*ValorEstatico.IdCiudadArequipa*/)
                {
                    fur.IdCiudad = IdCiudad;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonal;
                }
                if (IdCiudad == 14/*ValorEstatico.IdCiudadLima*/)
                {
                    fur.IdCiudad = IdCiudad;
                    fur.IdCentroCosto = ValorEstatico.IdCentroCostoPersonalLima;
                }
                fur.IdProducto = 224; //ValorEstatico.IdProductoEsSalud;
                fur.Descripcion = "ESSALUD  - ADM - Seguro Social de los Trabajadores";
                fur.IdProveedor = 481 /*398 id prueba*/;//ValorEstatico.IdProveedorSeguroSocialDeSalud;
                fur.Cantidad =totalEsSalud ;
                var datosHistorico = ObtenerHistoricoUltimaVersion(fur.IdProveedor.Value, fur.IdProducto.Value);
                fur.IdProductoPresentacion = datosHistorico.IdCantidad;
                fur.IdMonedaProveedor = datosHistorico.IdMoneda;
                fur.NumeroCuenta = datosHistorico.CuentaContable;
                fur.Cuenta = datosHistorico.Cuenta;
                fur.PrecioUnitarioMonedaOrigen = datosHistorico.CostoOriginal;
                fur.PrecioUnitarioDolares = datosHistorico.CostoDolares;
                fur.PrecioTotalMonedaOrigen = fur.PrecioUnitarioMonedaOrigen * fur.Cantidad;
                fur.PrecioTotalDolares = fur.PrecioUnitarioDolares * fur.Cantidad;
                fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurProyectado;
                fur.FechaLimite = fechaCreacionFur.AddDays(10);
                fur.Descripcion = fechaCreacionFur.AddDays(10).ToString("MMMM", CultureInfo.CreateSpecificCulture("es")).ToUpper() + " " + fechaCreacionFur.AddDays(10).Year + " - " + fur.Descripcion;
                fur.EstadoAprobadoObservado = false;
                fur.UsuarioCreacion = "ESSALUD-AUTOMATIC";
                fur.UsuarioModificacion = "ESSALUD-AUTOMATIC";
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.IdFurTipoPedido = ValorEstatico.IdTipoPedidoGastoInmediato;
                fur.NumeroSemana = fur.obtenerNumeroSemana(fur.FechaLimite.Value);
                fur.Cancelado = false;

                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.UsuarioSolicitud = "ESSALUD-AUTOMATIC";
                fur.Antiguo = 0;
                fur.IdMonedaPagoReal = datosHistorico.IdMoneda;
                fur.IdMonedaPagoRealizado = datosHistorico.IdMoneda;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.Estado = true;

                this.Insert(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ValorFiltroDTO> ObtenerFiltroAñoReporteCuentasContables()
        {
            try
            {
                var _query = "SELECT distinct AñoPago as Valor FROM fin.V_ReportePagosPorCuenta order by AñoPago desc ";
                List<ValorFiltroDTO> lista = new List<ValorFiltroDTO>();

                var res = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ValorFiltroDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ValorFiltroDTO>  ObtenerFiltroEmpresaReporteCuentasContables()
        {
            try
            {
                var _query = "SELECT distinct Empresa as Valor FROM fin.V_ReportePagosPorCuenta order by Empresa desc ";
                List<ValorFiltroDTO> lista = new List<ValorFiltroDTO>();

                var res = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ValorFiltroDTO>>(res);
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerMesProgOriginal()
        {
            try
            {
                var _query = "SELECT distinct Empresa as Valor FROM fin.V_ReportePresupuesto order by Empresa desc ";
                List<FiltroDTO> lista = new List<FiltroDTO>();

                var res = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CodigoFurDTO> ObtenerFursBusquedaCodigoAutoComplete(string valor)
        {
            try
            {
                List<CodigoFurDTO> ListaFurByCodigo = new List<CodigoFurDTO>();
                var _query = "SELECT Id, Codigo FROM FIN.V_ObtenerFurFinanzas where Codigo like CONCAT('%',@valor,'%') ORDER BY NumeroSemana,Id";
                var productoFurDB = _dapper.QueryDapper(_query, new { valor });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    ListaFurByCodigo = JsonConvert.DeserializeObject<List<CodigoFurDTO>>(productoFurDB);
                }
                return ListaFurByCodigo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Furs que estan en proceso de eliminacion, 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<FurAprobadoNoEjecutadoDTO> ObtenerFursNoEjecutados()
        {
            try
            {
                List<FurAprobadoNoEjecutadoDTO> ListaFur = new List<FurAprobadoNoEjecutadoDTO>();
                var camposTabla = "SELECT Id,Codigo,CentroCosto,Programa,Ciudad,TipoPedido,RazonSocial,Producto,ProductoPresentacion,FechaLimite,Descripcion,NumeroCuenta,Cuenta,Cantidad,FaseAprobacion1,PrecioUnitarioMonedaOrigen,PrecioTotalMonedaOrigen,UsuarioSolicitud,MonedaPagoReal,FechaAprobacionJefeFinanzas ";

                var _query = camposTabla + " FROM FIN.V_ObtenerFurNoEjecutados where idfaseAprobacion1 = " + ValorEstatico.IdFurAprobadoNoEjecutado;
                var FurDB = _dapper.QueryDapper(_query, new {  });
                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurAprobadoNoEjecutadoDTO>>(FurDB);
                }
                return ListaFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene todos los furs asociados al IdComprobante
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public List<FurAsociadoComprobanteDTO> ObtenerFurAsociadosPorComprobante(int IdComprobante)
        {
            try
            {
                List<FurAsociadoComprobanteDTO> ListaFur = new List<FurAsociadoComprobanteDTO>();
                var query = "SELECT Id, Codigo, Monto, IdMoneda,MonedaComprobante, MontoAmortizar FROM " +
                    "fin.V_ObtenerFurAsociadosPorComprobante WHERE IdComprobante = @IdComprobante AND EstadoAsociacion = 1 AND" +
                    " (FurCancelado = 0 OR FurCancelado IS NULL)";
                var res = _dapper.QueryDapper(query, new { IdComprobante });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurAsociadoComprobanteDTO>>(res);
                }
                return ListaFur;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene todos los furs para ser asociados a un comprobante
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public List<FurPorAsociarDTO> ObtenerFurParaAsociar(CompuestoFurPorAsociarDTO compuestofur)
        {
            try
            {
                List<FurPorAsociarDTO> ListaFur = new List<FurPorAsociarDTO>();
                var IdProveedor = compuestofur.IdProveedor;
                var EstadoFur = compuestofur.EstadoFur;
                var Codigo = compuestofur.Codigo;
                var query = "SELECT Id, Codigo, MontoFur FROM " +
                    "fin.V_ObtenerFurParaAsociar WHERE (IdProveedor = @IdProveedor OR @IdProveedor IS NULL) AND (EstadoFur = @EstadoFur OR @EstadoFur IS NULL) AND" +
                    " Codigo like Concat(@Codigo,'%') and FaseAprobacion = 5 and CancelarFur= 0 and EstadoFur = 1";
                var res = _dapper.QueryDapper(query, new { IdProveedor, EstadoFur, Codigo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurPorAsociarDTO>>(res);
                }
                return ListaFur;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Fur por Codigo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<CodigoFurProveedorDTO> ObtenerFurAutoComplete(string valor)
        {
            try
            {
                List<CodigoFurProveedorDTO> ListaFur = new List<CodigoFurProveedorDTO>();
                var _query = "SELECT Id, Codigo,IdProveedor,RazonSocial FROM FIN.V_ObtenerFurFinanzas where Codigo like CONCAT('%',@valor,'%') ORDER BY NumeroSemana,Id";
                var productoFurDB = _dapper.QueryDapper(_query, new { valor });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<CodigoFurProveedorDTO>>(productoFurDB);
                }
                return ListaFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CodigoFurProveedorDTO> ObtenerFur()
        {
            try
            {
                List<CodigoFurProveedorDTO> ListaFur = new List<CodigoFurProveedorDTO>();
                var _query = "SELECT Id, Codigo,IdProveedor,RazonSocial FROM FIN.V_ObtenerFurFinanzas ORDER BY NumeroSemana,Id";
                var productoFurDB = _dapper.QueryDapper(_query, new { });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<CodigoFurProveedorDTO>>(productoFurDB);
                }
                return ListaFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
