using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Finanzas/Moneda
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Controladores para obtener los tipos de cambio
    /// </summary>

    [Route("api/TipoCambio")]
    public class TipoCambioController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly int _idMonedaSoles = 20;
        private readonly int _idMonedaCol = 10;
        public TipoCambioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                return Ok(_repTipoCambio.GetBy(x => x.Estado == true, x => new { x.Id, x.SolesDolares, x.DolaresSoles, x.Fecha }).OrderByDescending(x => x.Fecha));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET 
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de periodos
        /// </summary>
        /// <returns>Retorna la lista TipoCambioReporteDTO
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboPeriodo()
        {
            try
            {
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();

                List<PeriodoFiltroDTO> result = new List<PeriodoFiltroDTO> ();
                result = repPeriodo.ObtenerPeriodos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// Tipo Función: POST 
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los tipos de cambio dependiendo a los filtros
        /// </summary>
        /// <param name="filtroTipoCambio">DTO con el id moneda y la fecha</param>
        /// <returns>Retorna la lista TipoCambioReporteDTO
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConFiltro([FromBody] TipoCambioFiltroDTO filtroTipoCambio)
        {
            int idMoneda;
            idMoneda = filtroTipoCambio.IdMoneda;
            DateTime? fecha;
            fecha = filtroTipoCambio.Fecha;
            try
            {
                List <TipoCambioReporteDTO> respuestaGeneral = new List<TipoCambioReporteDTO>();
                TipoCambioRepositorio reporteTipoCambio = new TipoCambioRepositorio();

                respuestaGeneral = reporteTipoCambio.ObtenerTipoCambioFiltro(idMoneda,fecha);
                return Ok(respuestaGeneral);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoCambioDTO TipoCambioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                if (TipoCambioDTO.IdPeriodo != 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var periodo = _repPeriodo.GetBy(x => x.Id == TipoCambioDTO.IdPeriodo, x => new { x.FechaInicialFinanzas, x.FechaFinFinanzas }).ToList().FirstOrDefault();
                        var dias = Enumerable.Range(0, 1 + periodo.FechaFinFinanzas.Subtract(periodo.FechaInicialFinanzas).Days).Select(offset => periodo.FechaInicialFinanzas.AddDays(offset)).ToList();

                        List<TipoCambioBO> listaTipoCambio = new List<TipoCambioBO>();

                        var listaFechasEliminar = _repTipoCambio.GetBy(x=>x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();

                        var listadoEliminarFechaTipoCambioMoneda = listaFechasEliminar.Select(x => x.Id);

                        var listadoEliminarFechaMoneda = _repTipoCambioMoneda.GetBy(x => listadoEliminarFechaTipoCambioMoneda.Any(w => x.IdTipoCambio == w)).ToList();

                        foreach (var item in dias)
                        {
                            if (listaFechasEliminar.Where(x => x.Fecha == item).Count() == 0)//si no existe para esa fecha ese tipo de moneda
                            {
                                TipoCambioBO tipoCambioBO = new TipoCambioBO()
                                {
                                    SolesDolares = TipoCambioDTO.SolesDolares,
                                    DolaresSoles = TipoCambioDTO.DolaresSoles,
                                    Fecha = item,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = TipoCambioDTO.NombreUsuario,
                                    UsuarioModificacion = TipoCambioDTO.NombreUsuario
                                };
                                //para el update en ambas tablas 
                                if (!tipoCambioBO.HasErrors)
                                {
                                    _repTipoCambio.Insert(tipoCambioBO);
                                    TipoCambioMonedaBO tipoCambioMonedaBO = new TipoCambioMonedaBO
                                    {
                                        IdTipoCambio = tipoCambioBO.Id,
                                        IdTipoCambioCol = null,
                                        MonedaAdolar = TipoCambioDTO.SolesDolares,
                                        DolarAmoneda = TipoCambioDTO.DolaresSoles,
                                        IdMoneda = this._idMonedaSoles,
                                        Fecha = item,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = TipoCambioDTO.NombreUsuario,
                                        UsuarioModificacion = TipoCambioDTO.NombreUsuario
                                    };
                                    if (!tipoCambioBO.HasErrors)
                                    {
                                        _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);
                                    }
                                }
                                else
                                {
                                    return BadRequest(tipoCambioBO.ActualesErrores);
                                }
                            }
                            else {//ya existe entonces actualizamos
                                var tipoCambioSoles = listaFechasEliminar.Where(x => x.Fecha == item).FirstOrDefault();
                                tipoCambioSoles.SolesDolares = TipoCambioDTO.SolesDolares;
                                tipoCambioSoles.DolaresSoles = TipoCambioDTO.DolaresSoles;
                                tipoCambioSoles.FechaModificacion = DateTime.Now;
                                tipoCambioSoles.UsuarioModificacion = TipoCambioDTO.NombreUsuario;

                                if (!tipoCambioSoles.HasErrors)
                                {
                                    _repTipoCambio.Update(tipoCambioSoles);

                                    var tipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.IdTipoCambio == tipoCambioSoles.Id && x.IdTipoCambioCol == null).FirstOrDefault();
                                    tipoCambioMoneda.MonedaAdolar = TipoCambioDTO.SolesDolares;
                                    tipoCambioMoneda.DolarAmoneda = TipoCambioDTO.DolaresSoles;
                                    tipoCambioMoneda.FechaModificacion = DateTime.Now;
                                    tipoCambioMoneda.UsuarioModificacion = TipoCambioDTO.NombreUsuario;

                                    if (!tipoCambioMoneda.HasErrors)
                                    {
                                        _repTipoCambioMoneda.Update(tipoCambioMoneda);
                                    }
                                }
                                else
                                {
                                    return BadRequest(tipoCambioSoles.ActualesErrores);
                                }
                            }
                        }
                        scope.Complete();
                        return Ok(listaTipoCambio.Select(x => new { x.Id, x.DolaresSoles, x.SolesDolares, x.Fecha }));
                    }
                }
                else if (TipoCambioDTO.Fecha != null)
                {
                    var tempDiasExistente = _repTipoCambio.GetBy(x => x.Fecha == TipoCambioDTO.Fecha, x => new { x.Id }).ToList();

                    ///Calcula que el registro a insertar no exista
                    if (tempDiasExistente.Count() == 0 || tempDiasExistente == null)
                    {
                        var tipoCambioBO = new TipoCambioBO()
                        {
                            SolesDolares = TipoCambioDTO.SolesDolares,
                            DolaresSoles = TipoCambioDTO.DolaresSoles,
                            Fecha = TipoCambioDTO.Fecha.Value,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = TipoCambioDTO.NombreUsuario,
                            UsuarioModificacion = TipoCambioDTO.NombreUsuario
                        };

                        if (!tipoCambioBO.HasErrors)
                        {
                            _repTipoCambio.Insert(tipoCambioBO);
                            var tipoCambioMonedaBO = new TipoCambioMonedaBO
                            {
                                IdTipoCambio = tipoCambioBO.Id,
                                IdTipoCambioCol = null,
                                MonedaAdolar = TipoCambioDTO.SolesDolares,
                                DolarAmoneda = TipoCambioDTO.DolaresSoles,
                                IdMoneda = this._idMonedaSoles,
                                Fecha = TipoCambioDTO.Fecha.Value,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = TipoCambioDTO.NombreUsuario,
                                UsuarioModificacion = TipoCambioDTO.NombreUsuario
                            };
                            if (!tipoCambioMonedaBO.HasErrors)
                            {
                                _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);
                            }
                            else
                            {
                                return BadRequest(tipoCambioMonedaBO.ActualesErrores);
                            }
                        }
                        else
                        {
                            return BadRequest(tipoCambioBO.ActualesErrores);
                        }
                        return Ok(tipoCambioBO);
                    }
                    else {
                        return BadRequest("Existe un tipo de cambio soles para esa fecha!");
                    }
                }
                else {
                    return BadRequest("Error valores invalidos");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST 
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta los tipo de cambio por periodo o por fecha
        /// </summary>
        /// <returns>Retorna la lista TipoCambioReporteDTO
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarGeneral([FromBody] TipoCambioMonedaDTO tipoCambioMonedaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);

                if (tipoCambioMonedaDTO.IdPeriodo != 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var periodo = _repPeriodo.GetBy(x => x.Id == tipoCambioMonedaDTO.IdPeriodo, x => new { x.FechaInicialFinanzas, x.FechaFinFinanzas }).ToList().FirstOrDefault();
                        var dias = Enumerable.Range(0, 1 + periodo.FechaFinFinanzas.Subtract(periodo.FechaInicialFinanzas).Days).Select(offset => periodo.FechaInicialFinanzas.AddDays(offset)).ToList();

                        List<TipoCambioMonedaBO> listaTipoCambio = new List<TipoCambioMonedaBO>();

                        var listaFechasEliminar = _repTipoCambio.GetBy(x => x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();
                        var listaFechasEliminarCol = _repTipoCambioCol.GetBy(x => x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();

                        var listadoEliminarFechaTipoCambioMoneda = listaFechasEliminar.Select(x => x.Id);

                        var listadoEliminarFechaMoneda = _repTipoCambioMoneda.GetBy(x => listadoEliminarFechaTipoCambioMoneda.Any(w => x.IdTipoCambio == w)).ToList();
                                    
                       foreach (var item in dias)
                        {
                            var tipoCambioMonedaFecha = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == tipoCambioMonedaDTO.IdMoneda && x.Fecha == item).ToList();
                            if (listaFechasEliminar.Where(x => x.Fecha == item).Count() == 0 ||  tipoCambioMonedaFecha.Count() == 0 || tipoCambioMonedaFecha == null)//si no existe para esa fecha ese tipo de moneda
                            {
                                TipoCambioMonedaBO tipoCambioMonedaBO = new TipoCambioMonedaBO()
                                {
                                    MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = item,
                                    IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                    IdTipoCambio = null,
                                    IdTipoCambioCol = null,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };
                                //para el update en ambas tablas 
                                if (!tipoCambioMonedaBO.HasErrors)
                                {
                                    _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);
                                    if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                                    {
                                        TipoCambioBO tipoCambioBO = new TipoCambioBO()
                                        {
                                            SolesDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                            DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda,
                                            Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                            UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                        };

                                        _repTipoCambio.Insert(tipoCambioBO);

                                        tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                        tipoCambioMonedaBO.IdTipoCambio = tipoCambioBO.Id;
                                        tipoCambioMonedaBO.IdTipoCambioCol = null;
                                        tipoCambioMonedaBO.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                        tipoCambioMonedaBO.FechaModificacion = DateTime.Now;

                                        _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                                    }
                                    else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                                    {
                                        TipoCambioColBO tipoCambioColBO = new TipoCambioColBO()
                                        {
                                            PesosDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                            DolaresPesos = tipoCambioMonedaDTO.DolarAmoneda,
                                            Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                            IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                            Estado = true,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                            UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                        };

                                        _repTipoCambioCol.Insert(tipoCambioColBO);

                                        tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                        tipoCambioMonedaBO.IdTipoCambioCol = tipoCambioColBO.Id;
                                        tipoCambioMonedaBO.IdTipoCambio = null;
                                        tipoCambioMonedaBO.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                        tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                                        _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                                    }
                                    
                                }
                                else
                                {
                                    return BadRequest("Un tipo de cambio ya existe configurado para esa moneda y fecha");
                                }
                            }
                            else
                            {//ya existe entonces actualizamos
                                if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                                {
                                    var tipoCambioSoles = listaFechasEliminar.Where(x => x.Fecha == item).FirstOrDefault();
                                    tipoCambioSoles.SolesDolares = tipoCambioMonedaDTO.MonedaAdolar;
                                    tipoCambioSoles.DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda;
                                    tipoCambioSoles.FechaModificacion = DateTime.Now;
                                    tipoCambioSoles.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;

                                    if (!tipoCambioSoles.HasErrors)
                                    {
                                        _repTipoCambio.Update(tipoCambioSoles);

                                        var tipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.IdTipoCambio == tipoCambioSoles.Id && x.IdTipoCambioCol == null).FirstOrDefault();
                                        tipoCambioMoneda.MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar;
                                        tipoCambioMoneda.DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda;
                                        tipoCambioMoneda.FechaModificacion = DateTime.Now;
                                        tipoCambioMoneda.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;

                                        if (!tipoCambioMoneda.HasErrors)
                                        {
                                            _repTipoCambioMoneda.Update(tipoCambioMoneda);
                                        }
                                    }
                                    else
                                    {
                                        return BadRequest(tipoCambioSoles.ActualesErrores);
                                    }
                                }
                                else
                                {
                                    return BadRequest("El tipo de cambio ya existe");
                                }                                
                            }
                        }
                        scope.Complete();
                        return Ok(listaTipoCambio.Select(x => new { x.Id, x.DolarAmoneda, x.MonedaAdolar, x.Fecha }));
                    }
                }
                else if (tipoCambioMonedaDTO.Fecha != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var tempDiasExistente = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == tipoCambioMonedaDTO.IdMoneda && x.Fecha == tipoCambioMonedaDTO.Fecha).ToList();

                    ///Calcula que el registro a insertar no exista
                    if (tempDiasExistente.Count() == 0 || tempDiasExistente == null)
                    {
                        var tipoCambioMonedaBO = new TipoCambioMonedaBO
                        {
                            MonedaAdolar = tipoCambioMonedaDTO.MonedaAdolar,
                            DolarAmoneda = tipoCambioMonedaDTO.DolarAmoneda,
                            Fecha = tipoCambioMonedaDTO.Fecha.Value,
                            IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                            IdTipoCambio = null,
                            IdTipoCambioCol = null,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                            UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                        };
                        if (!tipoCambioMonedaBO.HasErrors)
                        {
                            _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);

                            if (tipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                            {
                                TipoCambioBO tipoCambioBO = new TipoCambioBO()
                                {
                                    SolesDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresSoles = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };

                                _repTipoCambio.Insert(tipoCambioBO);

                                tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                tipoCambioMonedaBO.IdTipoCambio = tipoCambioBO.Id;
                                tipoCambioMonedaBO.IdTipoCambioCol = null;
                                tipoCambioMonedaBO.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;

                                _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                            }
                            else if (tipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                            {
                                TipoCambioColBO tipoCambioColBO = new TipoCambioColBO()
                                {
                                    PesosDolares = tipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresPesos = tipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = tipoCambioMonedaDTO.Fecha.Value,
                                    IdMoneda = tipoCambioMonedaDTO.IdMoneda,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = tipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario
                                };

                                _repTipoCambioCol.Insert(tipoCambioColBO);

                                tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                tipoCambioMonedaBO.IdTipoCambioCol = tipoCambioColBO.Id;
                                tipoCambioMonedaBO.IdTipoCambio = null;
                                tipoCambioMonedaBO.UsuarioModificacion = tipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                                _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                            }
                            
                        }
                            scope.Complete();
                            return Ok(tipoCambioMonedaBO);
                    }
                    else
                    {
                        return BadRequest("Existe un tipo de cambio para esa fecha!");
                    }
                    }
                }
                else
                {
                    return BadRequest("Error valores invalidos");
                }
            }
#pragma warning disable CA1031 // No capture tipos de excepción generales.
            catch (Exception e)
#pragma warning restore CA1031 // No capture tipos de excepción generales.
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] TipoCambioDTO TipoCambioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                if (TipoCambioDTO.IdPeriodo == -10 && TipoCambioDTO.Fecha != null)
                {
                    TipoCambioDTO.IdPeriodo = _repPeriodo.GetBy(x => x.FechaInicialFinanzas <= TipoCambioDTO.Fecha && x.FechaFinFinanzas >= TipoCambioDTO.Fecha).FirstOrDefault().Id;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        var periodo = _repPeriodo.GetBy(x => x.Id == TipoCambioDTO.IdPeriodo, x => new { x.FechaInicialFinanzas, x.FechaFinFinanzas }).ToList().FirstOrDefault();
                        var dias = Enumerable.Range(0, 1 + periodo.FechaFinFinanzas.Subtract(periodo.FechaInicialFinanzas).Days).Select(offset => periodo.FechaInicialFinanzas.AddDays(offset)).ToList();

                        List<TipoCambioBO> listaTipoCambio = new List<TipoCambioBO>();

                        var listaFechasEliminar = _repTipoCambio.GetBy(x => x.Fecha >= dias.First() && x.Fecha <= dias.Last()).ToList();

                        var listadoEliminarFechaMonedaSoles = listaFechasEliminar.Select(x => x.Id);
                        var listadoEliminarFechaTipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => listadoEliminarFechaMonedaSoles.Any(w => x.IdTipoCambio == w)).ToList();

                        foreach (var item in dias)
                        {
                            //buscamos los dias que cuadran y se modifica los tipos de cambio
                            var tipoCambioSoles = listaFechasEliminar.Where(x => x.Fecha == item).FirstOrDefault();
                            if (tipoCambioSoles != null)
                            {
                                tipoCambioSoles.SolesDolares = TipoCambioDTO.SolesDolares;
                                tipoCambioSoles.DolaresSoles = TipoCambioDTO.DolaresSoles;
                                tipoCambioSoles.FechaModificacion = DateTime.Now;
                                tipoCambioSoles.UsuarioModificacion = TipoCambioDTO.NombreUsuario;

                                if (!tipoCambioSoles.HasErrors)
                                {
                                    _repTipoCambio.Update(tipoCambioSoles);

                                    var tipoCambioMoneda = _repTipoCambioMoneda.GetBy(x => x.IdTipoCambio == tipoCambioSoles.Id && x.IdTipoCambioCol == null).FirstOrDefault();
                                    tipoCambioMoneda.MonedaAdolar = TipoCambioDTO.SolesDolares;
                                    tipoCambioMoneda.DolarAmoneda = TipoCambioDTO.DolaresSoles;
                                    tipoCambioMoneda.FechaModificacion = DateTime.Now;
                                    tipoCambioMoneda.UsuarioModificacion = TipoCambioDTO.NombreUsuario;

                                    if (!tipoCambioMoneda.HasErrors)
                                    {
                                        _repTipoCambioMoneda.Update(tipoCambioMoneda);
                                    }
                                }
                                else
                                {
                                    return BadRequest(tipoCambioSoles.ActualesErrores);
                                }
                            }
                        }
                        scope.Complete();
                        return Ok(listaTipoCambio.Select(x => new { x.Id, x.DolaresSoles, x.SolesDolares, x.Fecha }));
                    }
                }
                else if (TipoCambioDTO.Fecha != null)
                {
                    var tempDiasExistente = _repTipoCambio.GetBy(x => x.Fecha == TipoCambioDTO.Fecha).ToList();

                    if (tempDiasExistente.Count() == 0 || tempDiasExistente == null)
                    {
                        return BadRequest("No existe el registro para la fecha indicada");
                    }
                    else {
                        if (_repTipoCambio.Exist(x => x.Id == tempDiasExistente.FirstOrDefault().Id && x.Fecha == tempDiasExistente.FirstOrDefault().Fecha))//si existe el el id
                        {
                            //Lo obtenemos y actualizamos
                            var tipoCambio = _repTipoCambio.FirstById(tempDiasExistente.FirstOrDefault().Id);
                            tipoCambio.DolaresSoles = TipoCambioDTO.DolaresSoles;
                            tipoCambio.SolesDolares = TipoCambioDTO.SolesDolares;
                            tipoCambio.FechaModificacion = DateTime.Now;
                            tipoCambio.UsuarioModificacion = TipoCambioDTO.NombreUsuario;
                            if (!tipoCambio.HasErrors)
                            {
                                _repTipoCambio.Update(tipoCambio);//TABLA ORIGINAL

                                var tipoCambioMonedaBO = _repTipoCambioMoneda.FirstBy(x => x.IdTipoCambio == tipoCambio.Id && x.IdTipoCambioCol == null);
                                tipoCambioMonedaBO.MonedaAdolar = TipoCambioDTO.SolesDolares;
                                tipoCambioMonedaBO.DolarAmoneda = TipoCambioDTO.DolaresSoles;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                                tipoCambioMonedaBO.UsuarioModificacion = TipoCambioDTO.NombreUsuario;
                                if (!tipoCambioMonedaBO.HasErrors)
                                {
                                    _repTipoCambioMoneda.Update(tipoCambioMonedaBO);//TABLA DEPENDIENTE
                                }
                                else {
                                    return BadRequest(tipoCambioMonedaBO.ActualesErrores);
                                }
                            }
                            else {
                                return BadRequest(tipoCambio.GetErrors(null));
                            }
                        }
                        else {
                            return BadRequest("No se puede modificar la fecha de un tipo de cambios");
                        }
                    }
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Error valores invalidos");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO TipoCambioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repTipoCambio.Exist(TipoCambioDTO.Id))
                    {
                        _repTipoCambio.Delete(TipoCambioDTO.Id, TipoCambioDTO.NombreUsuario);//eliminamos de tipo cambio

                        var tipoCambioMonedaBO = _repTipoCambioMoneda.FirstBy(x => x.IdTipoCambio == TipoCambioDTO.Id);
                        if (tipoCambioMonedaBO != null)
                        {
                            _repTipoCambioMoneda.Delete(tipoCambioMonedaBO.Id, TipoCambioDTO.NombreUsuario);//eliminamos de tipo cambio moneda
                        }
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
