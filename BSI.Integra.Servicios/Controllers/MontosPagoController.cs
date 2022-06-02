using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MontosPago")]
    public class MontosPagoController : BaseController<TMontoPago, ValidadorMontosPagoDTO>
    {
        public MontosPagoController(IIntegraRepository<TMontoPago> repositorio, ILogger<BaseController<TMontoPago, ValidadorMontosPagoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosMontoPago()
        {
            try
            {
                MontoPagoRepositorio _repModoPago = new MontoPagoRepositorio();
                SubAreaCapacitacionRepositorio repSubArea = new SubAreaCapacitacionRepositorio();
                AreaCapacitacionRepositorio repArea = new AreaCapacitacionRepositorio();
                TipoDescuentoRepositorio repTipoDescuento = new TipoDescuentoRepositorio();
                PaisRepositorio repPais = new PaisRepositorio();
                MonedaRepositorio repMoneda = new MonedaRepositorio();
                CategoriaProgramaRepositorio repCategoria = new CategoriaProgramaRepositorio();
                TipoPagoRepositorio repTipoPago = new TipoPagoRepositorio();
                SuscripcionProgramaGeneralRepositorio repSuscripciones = new SuscripcionProgramaGeneralRepositorio();
                PlataformaPagoRepositorio repPlataformaPago = new PlataformaPagoRepositorio();

                MontoPagoComboDTO combos = new MontoPagoComboDTO();
                combos.SubAreas = repSubArea.ObtenerTodoFiltroAutoSelect();
                combos.Areas = repArea.ObtenerTodoFiltro();
                combos.Descuento = repTipoDescuento.ObtenerTodoFiltro();
                combos.Paises = repPais.ObtenerPaisesCombo();
                combos.Monedas = repMoneda.ObtenerMonedaPais();
                combos.CategoriasProgramas = repCategoria.ObtenerCategoriasNombrePrograma();
                combos.Suscripciones = repSuscripciones.ObtenerSuscripcionesCombos();
                combos.TipoPagos = repTipoPago.TipoPagoFiltro();
                combos.PlataformaPagos = repPlataformaPago.ObtenerPlataformasPagoFiltro();
                return Ok(combos);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdPrograma}/{IdCategoria}")]
        [HttpGet]
        public ActionResult ObtenerDetallesMontoPago(int IdPrograma, int IdCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SuscripcionProgramaGeneralRepositorio repSuscripciones = new SuscripcionProgramaGeneralRepositorio();
                TipoPagoCategoriaRepositorio repTipoPagoCategoria = new TipoPagoCategoriaRepositorio();
                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio();
                MontoPagoDetalleDTO detalleMontoPago = new MontoPagoDetalleDTO();

                detalleMontoPago.MontoPagos = repMontoPago.ObtenerMontoPagoPorPrograma(IdPrograma);
                detalleMontoPago.Suscripciones = repSuscripciones.ObtenerSuscripcionesPorProgramaNombre(IdPrograma);
                detalleMontoPago.TipoCategoria = repTipoPagoCategoria.ObtenerTipoPagoPorCategoria(IdCategoria);

                return Ok(detalleMontoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdMontoPago}")]
        [HttpGet]
        public ActionResult ObtenerSubDetallesPorMontoPago(int IdMontoPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoPlataformaRepositorio repMontoPagoPlataforma = new MontoPagoPlataformaRepositorio();
                MontoPagoSuscripcionRepositorio repMontoPagoSuscripcion = new MontoPagoSuscripcionRepositorio();

                SubDetalleMontoPagoDTO detalleMontoPago = new SubDetalleMontoPagoDTO();
                detalleMontoPago.MontoPagoPlataformas = repMontoPagoPlataforma.ObtenerMontoPagoPlataformaPorFiltro(IdMontoPago);
                detalleMontoPago.MontoPagoSuscripciones = repMontoPagoSuscripcion.ObtenerMontoPagoPlataformaPorFiltro(IdMontoPago);

                return Ok(detalleMontoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProgramasMontoPagoPanel()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralRepositorio repPgeneral = new PgeneralRepositorio();
                var programas = repPgeneral.ListarProgramaGeneralParaMontoPago();
                return Ok(programas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdPrograma}")]
        [HttpGet]
        public ActionResult ObtenerDescuentosPorPrograma(int IdPrograma)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralTipoDescuentoRepositorio repDescuento = new PgeneralTipoDescuentoRepositorio();
                var descuentos = repDescuento.ObtenerDescuentosPorPrograma(IdPrograma);
                return Ok(descuentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdTipoDescuento}")]
        [HttpGet]
        public ActionResult ObtenerProgramaporDescuento(int IdTipoDescuento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PgeneralTipoDescuentoRepositorio repDescuento = new PgeneralTipoDescuentoRepositorio();
                var descuentos = repDescuento.ObtenerProgramaporDescuento(IdTipoDescuento);
                return Ok(descuentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarDescuentos([FromBody] ProgramaTipoDescuentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PgeneralTipoDescuentoRepositorio repDescuento = new PgeneralTipoDescuentoRepositorio(contexto);
                DescuentoPromocionRepositorio repPromocion = new DescuentoPromocionRepositorio(contexto);
               
                using (TransactionScope scope = new TransactionScope())
                {
                    var descuentosPromocion = repPromocion.GetAll().ToList();
                    repDescuento.EliminacionLogicoPorPrograma(Json.IdPgeneral, Json.Usuario, Json.Descuentos);
                    foreach (var item in Json.Descuentos)
                    {
                        bool flag = false;
                        if (descuentosPromocion.Any(x => x.IdTipoDescuento == item))
                        {
                            flag = true;
                        }
                       

                        PgeneralTipoDescuentoBO descuento;
                        if (repDescuento.Exist(x => x.IdTipoDescuento == item && x.IdPgeneral == Json.IdPgeneral))
                        {
                            descuento = repDescuento.FirstBy(x => x.IdTipoDescuento == item && x.IdPgeneral == Json.IdPgeneral);
                            descuento.IdPgeneral = Json.IdPgeneral;
                            descuento.IdTipoDescuento = item;
                            descuento.FlagPromocion = flag;
                            descuento.UsuarioModificacion = Json.Usuario;
                            descuento.FechaModificacion = DateTime.Now;

                            repDescuento.Update(descuento);
                        }
                        else
                        {
                            descuento = new PgeneralTipoDescuentoBO();
                            descuento.IdPgeneral = Json.IdPgeneral;
                            descuento.IdTipoDescuento = item;
                            descuento.FlagPromocion = flag;
                            descuento.UsuarioCreacion = Json.Usuario;
                            descuento.UsuarioModificacion = Json.Usuario;
                            descuento.FechaCreacion = DateTime.Now;
                            descuento.FechaModificacion = DateTime.Now;
                            descuento.Estado = true;
                            repDescuento.Insert(descuento);
                        }
                    }
                    scope.Complete();
                }
                    
               
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarMontoPago([FromBody] CompuestoMontoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio();
                MontoPagoBO montoPago = new MontoPagoBO();
                

                using (TransactionScope scope = new TransactionScope())
                {
                    montoPago.Precio = Json.MontoPago.Precio;
                    montoPago.PrecioLetras = Json.MontoPago.PrecioLetras;
                    montoPago.IdMoneda = Json.MontoPago.IdMoneda;
                    montoPago.Matricula = Json.MontoPago.Matricula;
                    montoPago.Cuotas = Json.MontoPago.Cuotas; 
                    montoPago.NroCuotas = Json.MontoPago.NroCuotas;
                    montoPago.IdTipoDescuento = Json.MontoPago.IdTipoDescuento;
                    montoPago.IdPrograma = Json.MontoPago.IdPrograma;
                    montoPago.IdTipoPago = Json.MontoPago.IdTipoPago;
                    montoPago.IdPais = Json.MontoPago.IdPais;
                    montoPago.Vencimiento = Json.MontoPago.Vencimiento;
                    montoPago.PrimeraCuota = Json.MontoPago.PrimeraCuota;
                    montoPago.CuotaDoble = Json.MontoPago.CuotaDoble;
                    montoPago.Descripcion = Json.MontoPago.Descripcion;
                    montoPago.VisibleWeb = Json.MontoPago.VisibleWeb;
                    montoPago.Paquete = Json.MontoPago.Paquete;
                    montoPago.PorDefecto = Json.MontoPago.PorDefecto;
                    montoPago.MontoDescontado = Json.MontoPago.MontoDescontado;
                    montoPago.UsuarioCreacion = Json.Usuario;
                    montoPago.UsuarioModificacion = Json.Usuario;
                    montoPago.FechaCreacion = DateTime.Now;
                    montoPago.FechaModificacion = DateTime.Now;
                    montoPago.Estado = true;

                    montoPago.MontoPagoPlataforma = new List<MontoPagoPlataformaBO>();
                    montoPago.MontoPagoSuscripcion = new List<MontoPagoSuscripcionBO>();
                    foreach (var item in Json.PlataformasPagos)
                    {
                        MontoPagoPlataformaBO plataforma = new MontoPagoPlataformaBO();
                        plataforma.IdPlataformaPago = item;
                        plataforma.UsuarioCreacion = Json.Usuario;
                        plataforma.UsuarioModificacion = Json.Usuario;
                        plataforma.FechaCreacion = DateTime.Now;
                        plataforma.FechaModificacion = DateTime.Now;
                        plataforma.Estado = true;

                        montoPago.MontoPagoPlataforma.Add(plataforma);
                    }
                    foreach (var item in Json.SuscripcionesPagos)
                    {
                        MontoPagoSuscripcionBO suscripcion = new MontoPagoSuscripcionBO();
                        suscripcion.IdSuscripcionProgramaGeneral = item;
                        suscripcion.UsuarioCreacion = Json.Usuario;
                        suscripcion.UsuarioModificacion = Json.Usuario;
                        suscripcion.FechaCreacion = DateTime.Now;
                        suscripcion.FechaModificacion = DateTime.Now;
                        suscripcion.Estado = true;

                        montoPago.MontoPagoSuscripcion.Add(suscripcion);
                    }
                    repMontoPago.Insert(montoPago);
                    scope.Complete();
                }

                return Ok(montoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarMontoPago([FromBody] CompuestoMontoPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio(contexto);
                MontoPagoPlataformaRepositorio repPlataforma = new MontoPagoPlataformaRepositorio(contexto);
                MontoPagoSuscripcionRepositorio repSuscripcion = new MontoPagoSuscripcionRepositorio(contexto);

                MontoPagoBO montoPago = new MontoPagoBO();


                using (TransactionScope scope = new TransactionScope())
                {
                    repPlataforma.EliminacionLogicoPorMontoPago(Json.IdPgeneral, Json.Usuario, Json.PlataformasPagos);
                    repSuscripcion.EliminacionLogicoPorMontoPago(Json.IdPgeneral, Json.Usuario, Json.SuscripcionesPagos);

                    montoPago = repMontoPago.FirstById(Json.MontoPago.Id);

                    montoPago.Precio = Json.MontoPago.Precio;
                    montoPago.PrecioLetras = Json.MontoPago.PrecioLetras;
                    montoPago.IdMoneda = Json.MontoPago.IdMoneda;
                    montoPago.Matricula = Json.MontoPago.Matricula;
                    montoPago.Cuotas = Json.MontoPago.Cuotas;
                    montoPago.NroCuotas = Json.MontoPago.NroCuotas;
                    montoPago.IdTipoDescuento = Json.MontoPago.IdTipoDescuento;
                    montoPago.IdPrograma = Json.MontoPago.IdPrograma;
                    montoPago.IdTipoPago = Json.MontoPago.IdTipoPago;
                    montoPago.IdPais = Json.MontoPago.IdPais;
                    montoPago.Vencimiento = Json.MontoPago.Vencimiento;
                    montoPago.PrimeraCuota = Json.MontoPago.PrimeraCuota;
                    montoPago.CuotaDoble = Json.MontoPago.CuotaDoble;
                    montoPago.Descripcion = Json.MontoPago.Descripcion;
                    montoPago.VisibleWeb = Json.MontoPago.VisibleWeb;
                    montoPago.Paquete = Json.MontoPago.Paquete;
                    montoPago.PorDefecto = Json.MontoPago.PorDefecto;
                    montoPago.MontoDescontado = Json.MontoPago.MontoDescontado;
                    montoPago.UsuarioModificacion = Json.Usuario;
                    montoPago.FechaModificacion = DateTime.Now;

                    montoPago.MontoPagoPlataforma = new List<MontoPagoPlataformaBO>();
                    montoPago.MontoPagoSuscripcion = new List<MontoPagoSuscripcionBO>();
                    foreach (var item in Json.PlataformasPagos)
                    {
                        MontoPagoPlataformaBO plataforma;
                        if (repPlataforma.Exist(x => x.IdPlataformaPago == item && x.IdMontoPago == Json.MontoPago.Id))
                        {
                            plataforma = repPlataforma.FirstBy(x => x.IdPlataformaPago == item && x.IdMontoPago == Json.MontoPago.Id);
                            plataforma.IdPlataformaPago = item;
                            plataforma.UsuarioModificacion = Json.Usuario;
                            plataforma.FechaModificacion = DateTime.Now;

                        }
                        else
                        {
                            plataforma = new MontoPagoPlataformaBO();
                            plataforma.IdPlataformaPago = item;
                            plataforma.UsuarioCreacion = Json.Usuario;
                            plataforma.UsuarioModificacion = Json.Usuario;
                            plataforma.FechaCreacion = DateTime.Now;
                            plataforma.FechaModificacion = DateTime.Now;
                            plataforma.Estado = true;
                        }
                        montoPago.MontoPagoPlataforma.Add(plataforma);
                    }
                    foreach (var item in Json.SuscripcionesPagos)
                    {
                        MontoPagoSuscripcionBO suscripcion;
                        if (repSuscripcion.Exist(x => x.IdSuscripcionProgramaGeneral == item && x.IdMontoPago == Json.MontoPago.Id))
                        {
                            suscripcion = repSuscripcion.FirstBy(x => x.IdSuscripcionProgramaGeneral == item && x.IdMontoPago == Json.MontoPago.Id);
                            suscripcion.IdSuscripcionProgramaGeneral = item;
                            suscripcion.UsuarioModificacion = Json.Usuario;
                            suscripcion.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            suscripcion = new MontoPagoSuscripcionBO();
                            suscripcion.IdSuscripcionProgramaGeneral = item;
                            suscripcion.UsuarioCreacion = Json.Usuario;
                            suscripcion.UsuarioModificacion = Json.Usuario;
                            suscripcion.FechaCreacion = DateTime.Now;
                            suscripcion.FechaModificacion = DateTime.Now;
                            suscripcion.Estado = true;
                        }
                        montoPago.MontoPagoSuscripcion.Add(suscripcion);
                    }
                    repMontoPago.Update(montoPago);
                    scope.Complete();
                }

                return Ok(montoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarMontoPago(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                MontoPagoRepositorio repMontoPago = new MontoPagoRepositorio(contexto);
                MontoPagoPlataformaRepositorio repPlataforma = new MontoPagoPlataformaRepositorio(contexto);
                MontoPagoSuscripcionRepositorio repSuscripcion = new MontoPagoSuscripcionRepositorio(contexto);

                MontoPagoBO montoPago = new MontoPagoBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (repMontoPago.Exist(Id))
                    {
                        repMontoPago.Delete(Id, Usuario);
                        var hijosPlataforma = repPlataforma.GetBy(x => x.IdMontoPago == Id);
                        foreach (var hijo in hijosPlataforma)
                        {
                            repPlataforma.Delete(hijo.Id, Usuario);
                        }
                        var hijosSuscriptores = repSuscripcion.GetBy(x => x.IdMontoPago == Id);
                        foreach (var hijo in hijosSuscriptores)
                        {
                            repSuscripcion.Delete(hijo.Id, Usuario);
                        }
                    }
                    scope.Complete();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult obtenerPaquetes(int IdCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MontoPagoRepositorio _repMontoPago = new MontoPagoRepositorio();
                var lista = _repMontoPago.ObtenerPaquetesIdCentroCosto(IdCentroCosto);
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
     public class ValidadorMontosPagoDTO : AbstractValidator<TMontoPago>
     {
         public static ValidadorMontosPagoDTO Current = new ValidadorMontosPagoDTO();
         public ValidadorMontosPagoDTO()
            {

                RuleFor(objeto => objeto.Precio).NotEmpty().WithMessage("Precio es Obligatorio");

                RuleFor(objeto => objeto.PrecioLetras).NotEmpty().WithMessage("Precio en Letras es Obligatorio")
                                                .Length(1, 100).WithMessage("Precio en Letras debe tener 1 caracter minimo");

                RuleFor(objeto => objeto.IdMoneda).NotEmpty().WithMessage("Id Moneda es Obligatorio");

                RuleFor(objeto => objeto.Matricula).NotEmpty().WithMessage("Precio de la Matricula es Obligatorio");

                RuleFor(objeto => objeto.Cuotas).NotEmpty().WithMessage("Precio de las Cuotas es Obligatorio");

                RuleFor(objeto => objeto.NroCuotas).NotEmpty().WithMessage("Numero de Cuotas es Obligatorio");

                RuleFor(objeto => objeto.IdTipoDescuento).NotEmpty().WithMessage("Id Tipo Descuento es Obligatorio");

                RuleFor(objeto => objeto.IdPrograma).NotEmpty().WithMessage("Id Programa es Obligatorio");

                RuleFor(objeto => objeto.IdTipoPago).NotEmpty().WithMessage("Id Tipo Pago es Obligatorio");

                RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("IdPais es Obligatorio");

                RuleFor(objeto => objeto.Vencimiento).NotEmpty().WithMessage("Vencimiento es Obligatorio");

                RuleFor(objeto => objeto.PrimeraCuota).NotEmpty().WithMessage("Primera Cuota es Obligatorio");

                RuleFor(objeto => objeto.CuotaDoble).NotEmpty().WithMessage("Cuota Doble es Obligatorio");

                RuleFor(objeto => objeto.Descripcion).NotEmpty().Length(1, 100).WithMessage("Descripcion es Obligatorio");

                RuleFor(objeto => objeto.VisibleWeb).NotEmpty().WithMessage("Visible en WEb es Obligatorio");

                RuleFor(objeto => objeto.Paquete).NotEmpty().WithMessage("Paquete es Obligatorio");

                RuleFor(objeto => objeto.PorDefecto).NotEmpty().WithMessage("Campo por defecto es Obligatorio");

                RuleFor(objeto => objeto.MontoDescontado).NotEmpty().WithMessage("Monto Descontado es Obligatorio");



            }
     
     }
    
}

