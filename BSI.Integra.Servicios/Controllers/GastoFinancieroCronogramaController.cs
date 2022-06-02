using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.GastoFinancieroCronogramaController;
using static BSI.Integra.Servicios.Controllers.PanelControlMetaController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GastoFinancieroCronograma")]
    public class GastoFinancieroCronogramaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public GastoFinancieroCronogramaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaEntidadesFinancierasConPrestamo()
        {
            try
            {
                EntidadFinancieraRepositorio _repoEntidadFinanciera = new EntidadFinancieraRepositorio(_integraDBContext);
                return Ok(_repoEntidadFinanciera.ObtenerListaEntidadFinancieraPrestamo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaPrestamos()
        {
            try
            {
                GastoFinancieroCronogramaRepositorio _repoGastoFinancieroCronograma = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                return Ok(_repoGastoFinancieroCronograma.ObtenerListaPrestamosFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio();
                return Ok(gastoFinancieroCronogramaRepositorio.ObtenerGastoFinancieroCronograma());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                EntidadFinancieraRepositorio repEntidadFinanciera = new EntidadFinancieraRepositorio();
                MonedaRepositorio repMoneda = new MonedaRepositorio();

                CombosGastoFinancieroCronogramaDTO comboGastoFinancieroCronograma = new CombosGastoFinancieroCronogramaDTO();

                comboGastoFinancieroCronograma.ListaEntidadFinanciera = repEntidadFinanciera.ObtenerEntidadFinanciera();
                comboGastoFinancieroCronograma.ListaMoneda = repMoneda.ObtenerFiltroMoneda();

                return Ok(comboGastoFinancieroCronograma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCronogramaDetallePorId(int idCronograma)
        {
            try
            {
                GastoFinancieroCronogramaDetalleRepositorio gastoFinancieroCronogramaDetalleRepositorio = new GastoFinancieroCronogramaDetalleRepositorio();
                return Ok(gastoFinancieroCronogramaDetalleRepositorio.ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(idCronograma));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CompuestoGastoFinancieroCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                GastoFinancieroCronogramaBO gastoFinancieroCronogramaBO = new GastoFinancieroCronogramaBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    gastoFinancieroCronogramaBO.Nombre = Json.GastoFinancieroCronograma.Nombre;
                    gastoFinancieroCronogramaBO.IdEntidadFinanciera = Json.GastoFinancieroCronograma.IdEntidadFinanciera;
                    gastoFinancieroCronogramaBO.IdMoneda = Json.GastoFinancieroCronograma.IdMoneda;
                    gastoFinancieroCronogramaBO.CapitalTotal = Json.GastoFinancieroCronograma.CapitalTotal;
                    gastoFinancieroCronogramaBO.InteresTotal = Json.GastoFinancieroCronograma.InteresTotal;
                    gastoFinancieroCronogramaBO.FechaInicio = Json.GastoFinancieroCronograma.FechaInicio;
                    gastoFinancieroCronogramaBO.Estado = true;
                    gastoFinancieroCronogramaBO.UsuarioCreacion = Json.Usuario;
                    gastoFinancieroCronogramaBO.UsuarioModificacion = Json.Usuario;
                    gastoFinancieroCronogramaBO.FechaCreacion = DateTime.Now;
                    gastoFinancieroCronogramaBO.FechaModificacion = DateTime.Now;

                    gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle = new List<GastoFinancieroCronogramaDetalleBO>();

                    foreach (var item in Json.GastoFinancieroCronogramaDetalle)
                    {
                        GastoFinancieroCronogramaDetalleBO gastoFinancieroCronogramaDetalleBO = new GastoFinancieroCronogramaDetalleBO();
                        gastoFinancieroCronogramaDetalleBO.NumeroCuota = item.NumeroCuota;
                        gastoFinancieroCronogramaDetalleBO.CapitalCuota = item.CapitalCuota;
                        gastoFinancieroCronogramaDetalleBO.InteresCuota = item.InteresCuota;
                        gastoFinancieroCronogramaDetalleBO.FechaVencimientoCuota = item.FechaVencimientoCuota;
                        gastoFinancieroCronogramaDetalleBO.UsuarioCreacion = Json.Usuario;
                        gastoFinancieroCronogramaDetalleBO.UsuarioModificacion = Json.Usuario;
                        gastoFinancieroCronogramaDetalleBO.FechaCreacion = DateTime.Now;
                        gastoFinancieroCronogramaDetalleBO.FechaModificacion = DateTime.Now;
                        gastoFinancieroCronogramaDetalleBO.Estado = true;

                        gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle.Add(gastoFinancieroCronogramaDetalleBO);
                    }

                    gastoFinancieroCronogramaRepositorio.Insert(gastoFinancieroCronogramaBO);
                    scope.Complete();
                }
                return Ok(gastoFinancieroCronogramaBO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CompuestoGastoFinancieroCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                GastoFinancieroCronogramaDetalleRepositorio gastoFinancieroCronogramaDetalleRepositorio = new GastoFinancieroCronogramaDetalleRepositorio(_integraDBContext);
                GastoFinancieroCronogramaBO gastoFinancieroCronogramaBO = new GastoFinancieroCronogramaBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (gastoFinancieroCronogramaRepositorio.Exist(Json.GastoFinancieroCronograma.Id))
                    {
                        gastoFinancieroCronogramaDetalleRepositorio.EliminacionLogicoPorCronograma(Json.GastoFinancieroCronograma.Id, Json.Usuario, Json.GastoFinancieroCronogramaDetalle);

                        gastoFinancieroCronogramaBO = gastoFinancieroCronogramaRepositorio.FirstById(Json.GastoFinancieroCronograma.Id);
                        gastoFinancieroCronogramaBO.Nombre = Json.GastoFinancieroCronograma.Nombre;
                        gastoFinancieroCronogramaBO.IdEntidadFinanciera = Json.GastoFinancieroCronograma.IdEntidadFinanciera;
                        gastoFinancieroCronogramaBO.IdMoneda = Json.GastoFinancieroCronograma.IdMoneda;
                        gastoFinancieroCronogramaBO.CapitalTotal = Json.GastoFinancieroCronograma.CapitalTotal;
                        gastoFinancieroCronogramaBO.InteresTotal = Json.GastoFinancieroCronograma.InteresTotal;
                        gastoFinancieroCronogramaBO.FechaInicio = Json.GastoFinancieroCronograma.FechaInicio;
                        gastoFinancieroCronogramaBO.Estado = true;
                        gastoFinancieroCronogramaBO.UsuarioModificacion = Json.Usuario;
                        gastoFinancieroCronogramaBO.FechaModificacion = DateTime.Now;

                        gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle = new List<GastoFinancieroCronogramaDetalleBO>();

                        foreach (var item in Json.GastoFinancieroCronogramaDetalle)
                        {
                            GastoFinancieroCronogramaDetalleBO gastoFinancieroCronogramaDetalleBO;
                            if (gastoFinancieroCronogramaDetalleRepositorio.Exist(x => x.Id == item.Id))
                            {
                                gastoFinancieroCronogramaDetalleBO = gastoFinancieroCronogramaDetalleRepositorio.FirstBy(x => x.Id == item.Id && x.IdGastoFinancieroCronograma == Json.GastoFinancieroCronograma.Id);
                                gastoFinancieroCronogramaDetalleBO.NumeroCuota = item.NumeroCuota;
                                gastoFinancieroCronogramaDetalleBO.CapitalCuota = item.CapitalCuota;
                                gastoFinancieroCronogramaDetalleBO.InteresCuota = item.InteresCuota;
                                gastoFinancieroCronogramaDetalleBO.FechaVencimientoCuota = item.FechaVencimientoCuota;
                                gastoFinancieroCronogramaDetalleBO.UsuarioModificacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.FechaModificacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.Estado = true;

                            }
                            else
                            {
                                gastoFinancieroCronogramaDetalleBO = new GastoFinancieroCronogramaDetalleBO();
                                gastoFinancieroCronogramaDetalleBO.NumeroCuota = item.NumeroCuota;
                                gastoFinancieroCronogramaDetalleBO.CapitalCuota = item.CapitalCuota;
                                gastoFinancieroCronogramaDetalleBO.InteresCuota = item.InteresCuota;
                                gastoFinancieroCronogramaDetalleBO.FechaVencimientoCuota = item.FechaVencimientoCuota;
                                gastoFinancieroCronogramaDetalleBO.UsuarioCreacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.UsuarioModificacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.FechaCreacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.FechaModificacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.Estado = true;
                            }
                            gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle.Add(gastoFinancieroCronogramaDetalleBO);
                        }

                        gastoFinancieroCronogramaRepositorio.Update(gastoFinancieroCronogramaBO);
                        scope.Complete();
                    }
                }
                return Ok(gastoFinancieroCronogramaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                GastoFinancieroCronogramaDetalleRepositorio gastoFinancieroCronogramaDetalleRepositorio = new GastoFinancieroCronogramaDetalleRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (gastoFinancieroCronogramaRepositorio.Exist(Json.Id))
                    {
                        gastoFinancieroCronogramaRepositorio.Delete(Json.Id, Json.NombreUsuario);

                        var hijosGastoFinancieroCronograma = gastoFinancieroCronogramaDetalleRepositorio.GetBy(x => x.IdGastoFinancieroCronograma == Json.Id);
                        foreach (var hijo in hijosGastoFinancieroCronograma)
                        {
                            gastoFinancieroCronogramaDetalleRepositorio.Delete(hijo.Id, Json.NombreUsuario);
                        }
                    }

                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportePrestamos([FromBody] FiltroReportePrestamoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio _repoGastoFinancieroCronograma = new GastoFinancieroCronogramaRepositorio(_integraDBContext);

                var Lista = _repoGastoFinancieroCronograma.ObtenerReportePrestamos(Filtro.IdEntidadFinanciera, Filtro.IdGastoFinancieroCronograma);

                return Ok(Lista);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        public class ValidadorGastoFinancieroCronogramaDTO : AbstractValidator<TGastoFinancieroCronograma>
        {
            public static ValidadorGastoFinancieroCronogramaDTO Current = new ValidadorGastoFinancieroCronogramaDTO();
            public ValidadorGastoFinancieroCronogramaDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}
