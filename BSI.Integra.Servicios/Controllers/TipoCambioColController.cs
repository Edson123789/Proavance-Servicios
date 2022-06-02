using System;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoCambioCol")]
    public class TipoCambioColController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public TipoCambioColController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
                return Ok(_repTipoCambioCol.GetBy(x => x.Estado == true, x => new { x.Id, x.PesosDolares, x.DolaresPesos, x.Fecha, x.IdMoneda }).OrderByDescending(x => x.Fecha));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoCambioColDTO tipoCambioCol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
                    TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                    var tipoCambioMonedaFecha = _repTipoCambioCol.GetBy(x => x.IdMoneda == tipoCambioCol.IdMoneda && x.Fecha == tipoCambioCol.Fecha).ToList();


                    if (tipoCambioMonedaFecha.Count() == 0 || tipoCambioMonedaFecha == null)//si no existe registro para esa moneda y fecha
                    {
                        TipoCambioColBO tipoCambioColBO = new TipoCambioColBO()
                        {
                            PesosDolares = tipoCambioCol.PesosDolares,
                            DolaresPesos = tipoCambioCol.DolaresPesos,
                            IdMoneda = tipoCambioCol.IdMoneda,
                            Fecha = tipoCambioCol.Fecha,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = tipoCambioCol.NombreUsuario,
                            UsuarioModificacion = tipoCambioCol.NombreUsuario
                        };
                        if (!tipoCambioColBO.HasErrors)
                        {
                            _repTipoCambioCol.Insert(tipoCambioColBO);//tipo cambio col

                            TipoCambioMonedaBO tipoCambioMonedaBO = new TipoCambioMonedaBO
                            {
                                IdTipoCambioCol = tipoCambioColBO.Id,
                                IdTipoCambio = null,
                                MonedaAdolar = tipoCambioCol.PesosDolares,
                                DolarAmoneda = tipoCambioCol.DolaresPesos,
                                IdMoneda = tipoCambioCol.IdMoneda,
                                Fecha = tipoCambioCol.Fecha,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = tipoCambioCol.NombreUsuario,
                                UsuarioModificacion = tipoCambioCol.NombreUsuario
                            };
                            _repTipoCambioMoneda.Insert(tipoCambioMonedaBO);//tipo cambio moneda
                        }
                        else
                        {
                            return BadRequest(tipoCambioColBO.ActualesErrores);
                        }
                        scope.Complete();
                        return Ok(tipoCambioColBO);
                    }
                    else {
                        return BadRequest("Ya existe un tipo cambio para esa la moneda y esa fecha");
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
        public ActionResult Actualizar([FromBody] TipoCambioColDTO tipoCambioCol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repTipoCambioCol.Exist(tipoCambioCol.Id))
                    {
                        var tipoCambioColBO = _repTipoCambioCol.FirstById(tipoCambioCol.Id);
                        tipoCambioColBO.PesosDolares = tipoCambioCol.PesosDolares;
                        tipoCambioColBO.DolaresPesos = tipoCambioCol.DolaresPesos;
                        //tipoCambioColBO.IdMoneda = tipoCambioCol.IdMoneda;
                        //tipoCambioColBO.Fecha = tipoCambioCol.Fecha;
                        tipoCambioColBO.FechaModificacion = DateTime.Now;
                        tipoCambioColBO.UsuarioModificacion = tipoCambioCol.NombreUsuario;

                        if (!tipoCambioColBO.HasErrors)
                        {
                            _repTipoCambioCol.Update(tipoCambioColBO);//tipo cambio col

                            var tipoCambioMonedaBO = _repTipoCambioMoneda.FirstBy(x => x.IdTipoCambioCol == tipoCambioColBO.Id);
                            if (tipoCambioMonedaBO != null) {
                                tipoCambioMonedaBO.MonedaAdolar = tipoCambioCol.PesosDolares;
                                tipoCambioMonedaBO.DolarAmoneda = tipoCambioCol.DolaresPesos;
                                //tipoCambioMonedaBO.IdMoneda = tipoCambioCol.IdMoneda;
                                //tipoCambioMonedaBO.Fecha = tipoCambioCol.Fecha;
                                tipoCambioMonedaBO.FechaModificacion = DateTime.Now;
                                tipoCambioMonedaBO.UsuarioModificacion = tipoCambioCol.NombreUsuario;
                                _repTipoCambioMoneda.Update(tipoCambioMonedaBO);//tipo cambio moneda
                            }
                            scope.Complete();
                            return Ok(tipoCambioColBO);
                        }
                        else
                        {
                            return BadRequest(tipoCambioColBO.ActualesErrores);
                        }               
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO TipoCambioCol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
                TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repTipoCambioCol.Exist(TipoCambioCol.Id))
                    {
                        _repTipoCambioCol.Delete(TipoCambioCol.Id, TipoCambioCol.NombreUsuario);//eliminamos de tipo cambio col

                        var tipoCambioMonedaBO = _repTipoCambioMoneda.FirstBy(x => x.IdTipoCambioCol == TipoCambioCol.Id);
                        if (tipoCambioMonedaBO != null)
                        {
                            _repTipoCambioMoneda.Delete(tipoCambioMonedaBO.Id, TipoCambioCol.NombreUsuario);//eliminamos de tipo cambio moneda
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
