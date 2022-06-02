using System;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Finanzas/TipoCambio
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Contiene funciones basicas para los tipos de cambio
    /// </summary>
    [Route("api/TipoCambioMoneda")]
    public class TipoCambioMonedaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly int _idMonedaSoles = 20;
        private readonly int _idMonedaCol = 10;
        public TipoCambioMonedaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                //return Ok(_repTipoCambioMoneda.GetBy(x => x.Estado == true, x => new { x.Id, x.IdMoneda, x.DolarAmoneda, x.MonedaAdolar, x.Fecha, x.FechaCreacion }).OrderByDescending( x => x.Fecha));
                return Ok(_repTipoCambioMoneda.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoCambioMonedaDTO TipoCambioMonedaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
                using (TransactionScope scope = new TransactionScope())
                {
                    var tipoCambioMonedaFecha = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == TipoCambioMonedaDTO.IdMoneda && x.Fecha == TipoCambioMonedaDTO.Fecha).ToList();
                    if (tipoCambioMonedaFecha.Count() == 0 || tipoCambioMonedaFecha == null)//si no existe registro para esa moneda y fecha
                    {
                        var tipoCambioMonedaBO = new TipoCambioMonedaBO
                        {
                            MonedaAdolar = TipoCambioMonedaDTO.MonedaAdolar,
                            DolarAmoneda = TipoCambioMonedaDTO.DolarAmoneda,
                            Fecha = TipoCambioMonedaDTO.Fecha.Value,
                            IdMoneda = TipoCambioMonedaDTO.IdMoneda,
                            IdTipoCambio = null,
                            IdTipoCambioCol = null,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = TipoCambioMonedaDTO.NombreUsuario,
                            UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario
                        };
                        if (!tipoCambioMonedaBO.HasErrors)
                        {
                            _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);

                            if (TipoCambioMonedaDTO.IdMoneda == _idMonedaSoles)
                            {
                                TipoCambioBO tipoCambioBO = new TipoCambioBO()
                                {
                                    SolesDolares = TipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresSoles = TipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = TipoCambioMonedaDTO.Fecha.Value,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = TipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario
                                };

                                _repTipoCambio.Insert(tipoCambioBO);

                                tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                tipoCambioMonedaBO.IdTipoCambio = tipoCambioBO.Id;
                                tipoCambioMonedaBO.IdTipoCambioCol = null;
                                tipoCambioMonedaBO.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;

                                _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                            }
                            else if (TipoCambioMonedaDTO.IdMoneda == _idMonedaCol)
                            {
                                TipoCambioColBO tipoCambioColBO = new TipoCambioColBO()
                                {
                                    PesosDolares = TipoCambioMonedaDTO.MonedaAdolar,
                                    DolaresPesos = TipoCambioMonedaDTO.DolarAmoneda,
                                    Fecha = TipoCambioMonedaDTO.Fecha.Value,
                                    IdMoneda = TipoCambioMonedaDTO.IdMoneda,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = TipoCambioMonedaDTO.NombreUsuario,
                                    UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario
                                };

                                _repTipoCambioCol.Insert(tipoCambioColBO);

                                tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == tipoCambioMonedaBO.Id).FirstOrDefault();

                                tipoCambioMonedaBO.IdTipoCambioCol = tipoCambioColBO.Id;
                                tipoCambioMonedaBO.IdTipoCambio = null;
                                tipoCambioMonedaBO.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                                _repTipoCambioMoneda.Update(tipoCambioMonedaBO);
                            }
                        }
                        scope.Complete();
                        return Ok(tipoCambioMonedaBO);
                    }
                    else {
                        return BadRequest("Un tipo de cambio ya existe configurado para esa moneda y fecha");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] TipoCambioMonedaDTO TipoCambioMonedaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);

                if (_repTipoCambioMoneda.Exist(TipoCambioMonedaDTO.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var tipoCambioMonedaBO = _repTipoCambioMoneda.FirstById(TipoCambioMonedaDTO.Id);
                        tipoCambioMonedaBO.MonedaAdolar = TipoCambioMonedaDTO.MonedaAdolar;
                        tipoCambioMonedaBO.DolarAmoneda = TipoCambioMonedaDTO.DolarAmoneda;
                        tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                        tipoCambioMonedaBO.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;

                        if (!tipoCambioMonedaBO.HasErrors)
                        {
                            _repTipoCambioMoneda.Update(tipoCambioMonedaBO);


                            if (tipoCambioMonedaBO.IdTipoCambio == null && tipoCambioMonedaBO.IdTipoCambioCol == null)
                            {
                                //Es un registro de tabla misma no viene ni de T_TipoCambio, ni T_TipoCambioCol
                                //no hacemos nada
                            }
                            else if (tipoCambioMonedaBO.IdTipoCambio != null && tipoCambioMonedaBO.IdTipoCambioCol == null)
                            {
                                //viene de tipo cambio
                                //actualizacion
                                var tipoCambioBO = _repTipoCambio.GetBy(x => x.Id == tipoCambioMonedaBO.IdTipoCambio).FirstOrDefault();
                                tipoCambioBO.SolesDolares = TipoCambioMonedaDTO.MonedaAdolar;
                                tipoCambioBO.DolaresSoles = TipoCambioMonedaDTO.DolarAmoneda;
                                tipoCambioBO.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioBO.FechaModificacion = DateTime.Now;
                                _repTipoCambio.Update(tipoCambioBO);
                            }
                            else if (tipoCambioMonedaBO.IdTipoCambio == null && tipoCambioMonedaBO.IdTipoCambioCol != null)
                            {
                                //viene de tipo cambio col
                                //actualizacion
                                var tipoCambioColBO = _repTipoCambioCol.GetBy(x => x.Id == tipoCambioMonedaBO.IdTipoCambioCol).FirstOrDefault();
                                tipoCambioColBO.PesosDolares = TipoCambioMonedaDTO.MonedaAdolar;
                                tipoCambioColBO.DolaresPesos = TipoCambioMonedaDTO.DolarAmoneda;
                                tipoCambioColBO.UsuarioModificacion = TipoCambioMonedaDTO.NombreUsuario;
                                tipoCambioColBO.FechaModificacion = DateTime.Now;
                                _repTipoCambioCol.Update(tipoCambioColBO);
                            }
                        }
                        else
                        {
                            return BadRequest(tipoCambioMonedaBO.ActualesErrores);
                        }
                        scope.Complete();
                        return Ok(tipoCambioMonedaBO);
                    }
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO TipoCambioMonedaEliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);

                if (_repTipoCambioMoneda.Exist(TipoCambioMonedaEliminarDTO.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var tipoCambioMonedaBO = _repTipoCambioMoneda.GetBy(x => x.Id == TipoCambioMonedaEliminarDTO.Id).FirstOrDefault();
                        _repTipoCambioMoneda.Delete(TipoCambioMonedaEliminarDTO.Id, TipoCambioMonedaEliminarDTO.NombreUsuario);

                        if (tipoCambioMonedaBO.IdTipoCambio == null && tipoCambioMonedaBO.IdTipoCambioCol == null)
                        {
                            //Es un registro de tabla misma no viene ni de T_TipoCambio, ni T_TipoCambioCol
                            //no hacemos nada
                        }
                        else if (tipoCambioMonedaBO.IdTipoCambio != null && tipoCambioMonedaBO.IdTipoCambioCol == null)
                        {
                            //viene de tipo cambio
                            //eliminamos de tipo cambio
                            var listadoTipoCambio = _repTipoCambio.GetBy(x => x.Id == tipoCambioMonedaBO.IdTipoCambio, x => new { x.Id }).ToList();
                            _repTipoCambio.Delete(listadoTipoCambio.Select(x => x.Id), TipoCambioMonedaEliminarDTO.NombreUsuario);
                        }
                        else if (tipoCambioMonedaBO.IdTipoCambio == null && tipoCambioMonedaBO.IdTipoCambioCol != null)
                        {
                            //viene de tipo cambio col
                            //eliminamos de tipo cambio moneda
                            var listadoTipoCambioCol = _repTipoCambioCol.GetBy(x => x.Id == tipoCambioMonedaBO.IdTipoCambioCol, x => new { x.Id }).ToList();
                            _repTipoCambioCol.Delete(listadoTipoCambioCol.Select(x => x.Id), TipoCambioMonedaEliminarDTO.NombreUsuario);
                        }
                        scope.Complete();
                        return Ok(true);
                    }
                }
                else
                {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
