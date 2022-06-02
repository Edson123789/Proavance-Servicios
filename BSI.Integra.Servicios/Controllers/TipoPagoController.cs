using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoPago")]
    public class TipoPagoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoPagoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTipoPagoPanel()
        {
            try
            {
                TipoPagoRepositorio _repTiposPagos = new TipoPagoRepositorio(_integraDBContext);
                var listaTiposPagos = _repTiposPagos.ListarTiposPagosPanel();

                return Ok(listaTiposPagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdTipoPago}")]
        [HttpGet]
        public IActionResult ObtenerCategoriaPrograma(int IdTipoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoPagoRepositorio _repTiposPagos = new TipoPagoRepositorio(_integraDBContext);
                var listaTiposPagosCategoria = _repTiposPagos.ObtenerCategoriaProgramaPorTipoPago(IdTipoPago);

                return Ok(listaTiposPagosCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosTipoPago()
        {
            try
            {
                CategoriaProgramaRepositorio _repCategoriaPrograma = new CategoriaProgramaRepositorio(_integraDBContext);
                ModoPagoRepositorio _repModoPago = new ModoPagoRepositorio(_integraDBContext);

                CombosTipoPagoDTO combos = new CombosTipoPagoDTO();
                combos.CategoriaPrograma = _repCategoriaPrograma.ObtenerCategoriasNombrePrograma();
                combos.ModoPago = _repModoPago.ListarModosPagosPanel();

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTipoPago([FromBody] DatosTipoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoPagoRepositorio repTipoPago = new TipoPagoRepositorio(_integraDBContext);

                TipoPagoBO tipoPago = new TipoPagoBO();
                tipoPago.Nombre = Json.Nombre;
                tipoPago.Descripcion = Json.Descripcion;
                tipoPago.Cuotas = Json.Cuotas;
                tipoPago.Suscripciones = Json.Suscripciones;
                tipoPago.PorDefecto = Json.PorDefecto;
                tipoPago.UsuarioCreacion = Json.Usuario;
                tipoPago.UsuarioModificacion = Json.Usuario;
                tipoPago.FechaCreacion = DateTime.Now;
                tipoPago.FechaModificacion = DateTime.Now;
                tipoPago.Estado = true;
                
                if (Json.TipoPagoCategoria != null && Json.TipoPagoCategoria.Count > 0)
                {
                    tipoPago.TipoPagoCategoria = new List<TipoPagoCategoriaBO>();
                    foreach (var itemTipoPagoCategoria in Json.TipoPagoCategoria)
                    {
                        TipoPagoCategoriaBO tipoPagoCategoria = new TipoPagoCategoriaBO();
                        tipoPagoCategoria.IdCategoriaPrograma = itemTipoPagoCategoria.IdCategoriaPrograma;
                        tipoPagoCategoria.IdModoPago = Json.TipoPago;
                        tipoPagoCategoria.UsuarioCreacion = Json.Usuario;
                        tipoPagoCategoria.UsuarioModificacion = Json.Usuario;
                        tipoPagoCategoria.FechaCreacion = DateTime.Now;
                        tipoPagoCategoria.FechaModificacion = DateTime.Now;
                        tipoPagoCategoria.Estado = true;
                        tipoPago.TipoPagoCategoria.Add(tipoPagoCategoria);
                    }
                }

                var idTipoPago = repTipoPago.Insert(tipoPago);

                return Ok(tipoPago);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarTipoPago([FromBody] DatosTipoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool result = false;
                TipoPagoRepositorio repTipoPago = new TipoPagoRepositorio(_integraDBContext);
                TipoPagoCategoriaRepositorio repTipoPagoCategoria = new TipoPagoCategoriaRepositorio(_integraDBContext);
                TipoPagoBO tipoPago = new TipoPagoBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    
                    if (repTipoPago.Exist(Json.Id))
                    {
                        var eliminadosTipoCategosrias = repTipoPagoCategoria.EliminacionLogicoPorTipoPagoCategoria(Json.Id, Json.Usuario, Json.TipoPagoCategoria);

                        tipoPago = repTipoPago.FirstById(Json.Id);

                        tipoPago.Nombre = Json.Nombre;
                        tipoPago.Descripcion = Json.Descripcion;
                        tipoPago.Cuotas = Json.Cuotas;
                        tipoPago.Suscripciones = Json.Suscripciones;
                        tipoPago.PorDefecto = Json.PorDefecto;
                        tipoPago.UsuarioModificacion = Json.Usuario;
                        tipoPago.FechaModificacion = DateTime.Now;

                        tipoPago.TipoPagoCategoria = new List<TipoPagoCategoriaBO>();

                        foreach (var item in Json.TipoPagoCategoria)
                        {
                            TipoPagoCategoriaBO tipoPagoCategoria;
                            if (repTipoPagoCategoria.Exist(x => x.IdCategoriaPrograma == item.IdCategoriaPrograma && x.IdTipoPago == Json.Id))
                            {
                                tipoPagoCategoria = repTipoPagoCategoria.FirstBy(x => x.IdCategoriaPrograma == item.IdCategoriaPrograma && x.IdTipoPago == Json.Id);
                                tipoPagoCategoria.IdCategoriaPrograma = item.IdCategoriaPrograma;
                                tipoPagoCategoria.IdModoPago = Json.TipoPago;
                                tipoPagoCategoria.UsuarioModificacion = Json.Usuario;
                                tipoPagoCategoria.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                tipoPagoCategoria = new TipoPagoCategoriaBO();
                                tipoPagoCategoria.IdCategoriaPrograma = item.IdCategoriaPrograma;
                                tipoPagoCategoria.IdModoPago = Json.TipoPago;
                                tipoPagoCategoria.UsuarioCreacion = Json.Usuario;
                                tipoPagoCategoria.UsuarioModificacion = Json.Usuario;
                                tipoPagoCategoria.FechaCreacion = DateTime.Now;
                                tipoPagoCategoria.FechaModificacion = DateTime.Now;
                                tipoPagoCategoria.Estado = true;
                            }

                            tipoPago.TipoPagoCategoria.Add(tipoPagoCategoria);
                        }
                        
                        result = repTipoPago.Update(tipoPago);

                    }
                    scope.Complete();
                    return Ok(tipoPago);
                }
                

                

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarTipoPago(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoPagoRepositorio repTipoPago = new TipoPagoRepositorio(_integraDBContext);
                TipoPagoCategoriaRepositorio repTipoPagoCategoria = new TipoPagoCategoriaRepositorio(_integraDBContext);
                using (TransactionScope scope = new TransactionScope())
                {

                    if (repTipoPago.Exist(Id))
                    {
                        repTipoPago.Delete(Id, Usuario);
                        var hijos = repTipoPagoCategoria.GetBy(x => x.IdTipoPago == Id);
                        foreach (var hijo in hijos)
                        {
                            repTipoPagoCategoria.Delete(hijo.Id, Usuario);
                        }
                    }
                    scope.Complete();
                    
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
