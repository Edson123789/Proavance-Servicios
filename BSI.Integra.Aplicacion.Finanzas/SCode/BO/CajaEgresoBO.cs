using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CajaEgresoBO : BaseBO
    {
        public int? IdCajaPorRendirCabecera { get; set; }
        public int IdCaja { get; set; }
        public int? IdFur { get; set; }
        public string Descripcion { get; set; }
        public int IdMoneda { get; set; }
        public decimal TotalEfectivo { get; set; }
        public int? IdCajaEgresoAprobado { get; set; }
        public bool EsEnviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdPersonalResponsable { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int? IdComprobantePago { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }

        private CajaEgresoRepositorio _repCajaEgresoRep;
        private FurPagoRepositorio _repFurPagoRep;
        private ComprobantePagoPorFurRepositorio _repCompFurRep;
        private CajaRepositorio _repCajaRep;
        private CuentaCorrienteRepositorio _repCuentaCorrienteRep;
        private FurRepositorio _repFurRep;
        public CajaEgresoBO() {
        }

        public CajaEgresoBO(integraDBContext _integraDBContext)
        {
            _repCajaEgresoRep = new CajaEgresoRepositorio(_integraDBContext);
            _repFurPagoRep= new FurPagoRepositorio(_integraDBContext);
            _repCajaRep = new CajaRepositorio(_integraDBContext);
            _repCuentaCorrienteRep = new CuentaCorrienteRepositorio(_integraDBContext);
            _repFurRep = new FurRepositorio(_integraDBContext);
            _repCompFurRep = new ComprobantePagoPorFurRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Se actualiza la tabla CajaEgreso Aprobado y se coloca el IdCajaEgresoAprobado, Se crea un nuevo pago del REC
        /// generado y se actualiza el estado del fur si ha sido candelado en su totalidad o no. Este procedimiento se realiza para el metodo de Generacion de REC.
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public bool ActualizarAprobacionCajaEgreso(CajaEgresoAprobadoDTO objetoEgresoAprobado, IdCajaEgresoCanceladoDTO idRecCancelado)
        {
            try
            {
                var registroEgreso = _repCajaEgresoRep.FirstById(idRecCancelado.IdRec);

                if (registroEgreso == null)
                    throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");

                registroEgreso.IdPersonalResponsable = objetoEgresoAprobado.IdPersonalResponsable;
                registroEgreso.IdCajaEgresoAprobado = objetoEgresoAprobado.Id;
                registroEgreso.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                registroEgreso.FechaModificacion = DateTime.Now;
                _repCajaEgresoRep.Update(registroEgreso);
                            

                if (registroEgreso.IdFur != null)
                {
                    //Se inserta la asociacion del fur con el comprobante 
                    //ComprobantePagoPorFurBO comprobanteFur = new ComprobantePagoPorFurBO();
                    //comprobanteFur.IdComprobantePago = registroEgreso.IdComprobantePago.Value;
                    //comprobanteFur.IdFur = registroEgreso.IdFur.Value;
                    //comprobanteFur.Monto = registroEgreso.TotalEfectivo;
                    //comprobanteFur.Estado = true;
                    //comprobanteFur.FechaCreacion = DateTime.Now;
                    //comprobanteFur.FechaModificacion = DateTime.Now;
                    //comprobanteFur.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                    //comprobanteFur.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                    //_repCompFurRep.Insert(comprobanteFur);


                    var numPago = registroEgreso.IdFur == null ? 0 : _repFurPagoRep.obtenerNumeroPagoByFur(registroEgreso.IdFur.Value);
                    var idCuentaCorriente = _repCajaRep.obtenerIdCuentaCorriente(registroEgreso.IdCaja);
                    FurPagoBO furPago = new FurPagoBO();

                    var numCuenta = idCuentaCorriente == 0 ? "" : _repCuentaCorrienteRep.ObtenerCuentaCorrienteById(idCuentaCorriente);
                    furPago.IdFur = registroEgreso.IdFur;
                    furPago.NumeroPago = numPago == 0 ? 1 : numPago + 1;
                    furPago.IdComprobantePago= registroEgreso.IdComprobantePago.Value;
                    furPago.IdComprobantePagoPorFur = registroEgreso.IdComprobantePagoPorFur;
                    furPago.IdMoneda = registroEgreso.IdMoneda;
                    furPago.IdCuentaCorriente = idCuentaCorriente;
                   // furPago.NumeroCuenta = idCuentaCorriente == 0 ? "" : numCuenta;
                    furPago.NumeroRecibo = objetoEgresoAprobado.CodigoRec;
                    furPago.IdFormaPago = ValorEstatico.IdFormaPagoEnEfectivo; //Añadir Valor Estatico
                    furPago.FechaCobroBanco = objetoEgresoAprobado.FechaCreacionRegistro;
                    furPago.PrecioTotalMonedaDolares = registroEgreso.IdMoneda == ValorEstatico.IdMonedaDolares ? registroEgreso.TotalEfectivo : 0;
                    furPago.PrecioTotalMonedaOrigen = registroEgreso.IdMoneda != ValorEstatico.IdMonedaDolares ? registroEgreso.TotalEfectivo : 0;
                    furPago.Estado = true;
                    furPago.FechaCreacion = DateTime.Now;
                    furPago.FechaModificacion = DateTime.Now;
                    furPago.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                    furPago.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                    _repFurPagoRep.Insert(furPago);

                    var fur = _repFurRep.FirstById(registroEgreso.IdFur.Value);
                    if (idRecCancelado.FurEsCancelado)
                    {
                        fur.Cancelado = true;
                        fur.OcupadoSolicitud = true;
                        fur.OcupadoRendicion = true;
                       // fur.NumeroComprobante = string.Format("({0}-{1}) ", registroEgreso.Serie, registroEgreso.Numero) + fur.NumeroComprobante;
                    }
                    else
                    {
                        fur.OcupadoSolicitud = false;
                        fur.OcupadoRendicion = false;
                        //fur.NumeroComprobante = string.Format("({0}-{1}) ", registroEgreso.Serie, registroEgreso.Numero) + fur.NumeroComprobante;
                    }
                    _repFurRep.Update(fur);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta Registro de CajaEgreso con su idEgresoAprobado ya validado se actualiza el estado de Furs en caso sean cancelados, 
        /// de lo contrario se cambia a un estado para que puedan seguir siendo usados para pagos.
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public bool InsertarAprobacionCajaEgreso(CajaEgresoAprobadoDTO objetoEgresoAprobado,RegistroEgresoCajaDTO objEgresoCaja)
        {
            try
            {
                CajaEgresoBO registroEgreso = new CajaEgresoBO();
                registroEgreso.IdCaja = objEgresoCaja.IdCaja;
                registroEgreso.IdCajaPorRendirCabecera = objEgresoCaja.IdCajaPorRendirCabecera;
                registroEgreso.IdComprobantePago = objEgresoCaja.IdComprobantePago;
                registroEgreso.IdFur = objEgresoCaja.IdFur == 0 ? null : objEgresoCaja.IdFur;
                registroEgreso.Descripcion = objEgresoCaja.Descripcion;
                registroEgreso.IdMoneda = objEgresoCaja.IdMoneda;
                registroEgreso.TotalEfectivo = objEgresoCaja.TotalEfectivo;
                registroEgreso.IdPersonalSolicitante = objEgresoCaja.IdPersonalSolicitante.Value;
                registroEgreso.IdPersonalResponsable = objEgresoCaja.IdPersonalResponsable;
                registroEgreso.IdCajaEgresoAprobado= objetoEgresoAprobado.Id;
                registroEgreso.EsEnviado = objEgresoCaja.EsEnviado;
                registroEgreso.FechaEnvio = DateTime.Now;
                registroEgreso.Estado = true;
                registroEgreso.FechaCreacion = DateTime.Now;
                registroEgreso.FechaModificacion = DateTime.Now;
                registroEgreso.UsuarioCreacion = objEgresoCaja.UsuarioModificacion;
                registroEgreso.UsuarioModificacion = objEgresoCaja.UsuarioModificacion;



                if (registroEgreso.IdFur != null) {

                    //Se inserta la asociacion del fur con el comprobante 
                    ComprobantePagoPorFurBO comprobanteFur = new ComprobantePagoPorFurBO();
                    comprobanteFur.IdComprobantePago = registroEgreso.IdComprobantePago.Value;
                    comprobanteFur.IdFur = registroEgreso.IdFur.Value;
                    comprobanteFur.Monto = registroEgreso.TotalEfectivo;
                    comprobanteFur.Estado = true;
                    comprobanteFur.FechaCreacion = DateTime.Now;
                    comprobanteFur.FechaModificacion = DateTime.Now;
                    comprobanteFur.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                    comprobanteFur.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                    _repCompFurRep.Insert(comprobanteFur);

                    registroEgreso.IdComprobantePagoPorFur = comprobanteFur.Id;

                    var numPago = objEgresoCaja.IdFur == null ? 0 : _repFurPagoRep.obtenerNumeroPagoByFur(objEgresoCaja.IdFur.Value);
                    var idCuentaCorriente = _repCajaRep.obtenerIdCuentaCorriente(objEgresoCaja.IdCaja);
                    FurPagoBO furPago = new FurPagoBO();

                    var numCuenta = idCuentaCorriente == 0 ? "" : _repCuentaCorrienteRep.ObtenerCuentaCorrienteById(idCuentaCorriente);
                    furPago.IdComprobantePago = registroEgreso.IdComprobantePago.Value;
                    furPago.IdComprobantePagoPorFur = registroEgreso.IdComprobantePagoPorFur;
                    furPago.IdFur = objEgresoCaja.IdFur;
                    furPago.NumeroPago = numPago == 0 ? 1 : numPago + 1;
                    furPago.IdMoneda = objEgresoCaja.IdMoneda;
                    furPago.IdCuentaCorriente = idCuentaCorriente;
                    //furPago.NumeroCuenta = idCuentaCorriente == 0 ? "" : numCuenta;
                    furPago.NumeroRecibo = objetoEgresoAprobado.CodigoRec;
                    furPago.IdFormaPago = ValorEstatico.IdFormaPagoEnEfectivo; //Añadir Valor Estatico
                    furPago.FechaCobroBanco = objetoEgresoAprobado.FechaCreacionRegistro;
                    furPago.PrecioTotalMonedaDolares = objEgresoCaja.IdMoneda == ValorEstatico.IdMonedaDolares ? objEgresoCaja.TotalEfectivo : 0;
                    furPago.PrecioTotalMonedaOrigen = objEgresoCaja.IdMoneda != ValorEstatico.IdMonedaDolares ? objEgresoCaja.TotalEfectivo : 0;

                    furPago.Estado = true;
                    furPago.FechaCreacion = DateTime.Now;
                    furPago.FechaModificacion = DateTime.Now;
                    furPago.UsuarioCreacion = objetoEgresoAprobado.UsuarioModificacion;
                    furPago.UsuarioModificacion = objetoEgresoAprobado.UsuarioModificacion;
                    _repFurPagoRep.Insert(furPago);

                    var fur = _repFurRep.FirstById(objEgresoCaja.IdFur.Value);
                    if (objEgresoCaja.EsCancelado)
                    {
                        fur.Cancelado = true;
                        fur.OcupadoSolicitud = true;
                        fur.OcupadoRendicion = true;
                        //fur.NumeroComprobante = string.Format("({0}-{1}) ", objEgresoCaja.Serie, objEgresoCaja.Numero) + fur.NumeroComprobante;
                    }
                    else
                    {
                        fur.OcupadoSolicitud = false;
                        fur.OcupadoRendicion = false;
                        //fur.NumeroComprobante = string.Format("({0}-{1}) ", objEgresoCaja.Serie, objEgresoCaja.Numero) + fur.NumeroComprobante;
                    }
                    _repFurRep.Update(fur);
                }

                _repCajaEgresoRep.Insert(registroEgreso);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

    
}
