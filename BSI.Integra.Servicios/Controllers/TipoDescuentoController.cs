using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoDescuento")]
    public class TipoDescuentoController : BaseController<TTipoDescuento, ValidadorTipoDescuentoDTO>
    {
        public TipoDescuentoController(IIntegraRepository<TTipoDescuento> repositorio, ILogger<BaseController<TTipoDescuento, ValidadorTipoDescuentoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                TipoDescuentoRepositorio tipoDescuentoRepositorio = new TipoDescuentoRepositorio();
                return Ok(tipoDescuentoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboTipoDescuento()
        {
            try
            {
                AgendaTipoUsuarioRepositorio agendaTipoUsuarioRepositorio = new AgendaTipoUsuarioRepositorio();
                FormulaTipoDescuentoRepositorio formulaTipoDescuentoRepositorio = new FormulaTipoDescuentoRepositorio();
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();

                ComboTipoDescuentoDTO combos = new ComboTipoDescuentoDTO();

                combos.FormulaTipoDescuento = formulaTipoDescuentoRepositorio.ObtenerTodoGrid();
                combos.AgendaTipoUsuario = agendaTipoUsuarioRepositorio.ObtenerTipoUsuarioFiltro();
                combos.ProgramaGeneral = _repPgeneral.ObtenerProgramasFiltro();                

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAsesorCoordinadorPorIdTipoDescuento([FromBody]TipoDescuentoDTO objeto)
        {
            try
            {
                TipoDescuentoAsesorCoordinadorPwRepositorio tipoDescuentoAsesorCoordinadorPwRepositorio = new TipoDescuentoAsesorCoordinadorPwRepositorio();
                return Ok(tipoDescuentoAsesorCoordinadorPwRepositorio.ObtenerAsesorCoordinadorPorTipoDescuentoId(objeto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]

        public ActionResult InsertarTipoDescuento([FromBody]CompuestoTipoDescuentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDescuentoRepositorio repTipoDescuento = new TipoDescuentoRepositorio();
                TipoDescuentoBO tipoDescuento = new TipoDescuentoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    tipoDescuento.Id = Json.TipoDescuento.Id;
                    tipoDescuento.Codigo = Json.TipoDescuento.Codigo;
                    tipoDescuento.Descripcion = Json.TipoDescuento.Descripcion;
                    tipoDescuento.Formula = Json.TipoDescuento.Formula;
                    tipoDescuento.PorcentajeGeneral = Json.TipoDescuento.PorcentajeGeneral;
                    tipoDescuento.PorcentajeMatricula = Json.TipoDescuento.PorcentajeMatricula;
                    tipoDescuento.FraccionesMatricula = Json.TipoDescuento.FraccionesMatricula;
                    tipoDescuento.PorcentajeCuotas = Json.TipoDescuento.PorcentajeCuotas;
                    tipoDescuento.CuotasAdicionales = Json.TipoDescuento.CuotasAdicionales;
                    tipoDescuento.UsuarioCreacion = Json.Usuario;
                    tipoDescuento.UsuarioModificacion = Json.Usuario;
                    tipoDescuento.FechaCreacion = DateTime.Now;
                    tipoDescuento.FechaModificacion = DateTime.Now;
                    tipoDescuento.Estado = true;

                    tipoDescuento.TipoDescuentoAsesorCoordinadorPw = new List<TipoDescuentoAsesorCoordinadorPwBO>();

                    foreach (var item in Json.ListaTipos)
                    {
                        TipoDescuentoAsesorCoordinadorPwBO tipoDescuentoAsesorCoordinadorPw = new TipoDescuentoAsesorCoordinadorPwBO();
                        tipoDescuentoAsesorCoordinadorPw.IdAgendaTipoUsuario = item;
                        tipoDescuentoAsesorCoordinadorPw.IdTipoDescuento = Json.TipoDescuento.Id;
                        tipoDescuentoAsesorCoordinadorPw.UsuarioCreacion = Json.Usuario;
                        tipoDescuentoAsesorCoordinadorPw.UsuarioModificacion = Json.Usuario;
                        tipoDescuentoAsesorCoordinadorPw.FechaCreacion = DateTime.Now;
                        tipoDescuentoAsesorCoordinadorPw.FechaModificacion = DateTime.Now;
                        tipoDescuentoAsesorCoordinadorPw.Estado = true;

                        tipoDescuento.TipoDescuentoAsesorCoordinadorPw.Add(tipoDescuentoAsesorCoordinadorPw);
                    }

                    repTipoDescuento.Insert(tipoDescuento);
                    scope.Complete();

                }

                return Ok(tipoDescuento);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarTipoDescuento([FromBody]CompuestoTipoDescuentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                TipoDescuentoRepositorio repTipoDescuento = new TipoDescuentoRepositorio(contexto);
                TipoDescuentoAsesorCoordinadorPwRepositorio repTipoDescuentoAsesorCoordinadorPw = new TipoDescuentoAsesorCoordinadorPwRepositorio(contexto);
                TipoDescuentoBO tipoDescuento = new TipoDescuentoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (repTipoDescuento.Exist(Json.TipoDescuento.Id))
                    {
                        repTipoDescuentoAsesorCoordinadorPw.EliminacionLogicoPorTipoDescuento(Json.TipoDescuento.Id, Json.Usuario, Json.ListaTipos);

                        tipoDescuento = repTipoDescuento.FirstById(Json.TipoDescuento.Id);
                        tipoDescuento.Codigo = Json.TipoDescuento.Codigo;
                        tipoDescuento.Descripcion = Json.TipoDescuento.Descripcion;
                        tipoDescuento.Formula = Json.TipoDescuento.Formula;
                        tipoDescuento.PorcentajeGeneral = Json.TipoDescuento.PorcentajeGeneral;
                        tipoDescuento.FraccionesMatricula = Json.TipoDescuento.FraccionesMatricula;
                        tipoDescuento.PorcentajeMatricula = Json.TipoDescuento.PorcentajeMatricula;
                        tipoDescuento.CuotasAdicionales = Json.TipoDescuento.CuotasAdicionales;
                        tipoDescuento.PorcentajeCuotas = Json.TipoDescuento.PorcentajeCuotas;
                        tipoDescuento.UsuarioModificacion = Json.Usuario;
                        tipoDescuento.FechaModificacion = DateTime.Now;

                        tipoDescuento.TipoDescuentoAsesorCoordinadorPw = new List<TipoDescuentoAsesorCoordinadorPwBO>();
                        foreach (var item in Json.ListaTipos)
                        {
                            TipoDescuentoAsesorCoordinadorPwBO tipoDescuentoAsesorCoordinadorPw = new TipoDescuentoAsesorCoordinadorPwBO();
                            if (repTipoDescuentoAsesorCoordinadorPw.Exist(x => x.IdAgendaTipoUsuario == item && x.IdTipoDescuento == Json.TipoDescuento.Id))
                            {
                                tipoDescuentoAsesorCoordinadorPw = repTipoDescuentoAsesorCoordinadorPw.FirstBy(x => x.IdAgendaTipoUsuario == item && x.IdTipoDescuento == Json.TipoDescuento.Id);
                                tipoDescuentoAsesorCoordinadorPw.IdAgendaTipoUsuario = item;
                                tipoDescuentoAsesorCoordinadorPw.UsuarioModificacion = Json.Usuario;
                                tipoDescuentoAsesorCoordinadorPw.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                tipoDescuentoAsesorCoordinadorPw = new TipoDescuentoAsesorCoordinadorPwBO();
                                tipoDescuentoAsesorCoordinadorPw.IdAgendaTipoUsuario = item;
                                tipoDescuentoAsesorCoordinadorPw.UsuarioCreacion = Json.Usuario;
                                tipoDescuentoAsesorCoordinadorPw.UsuarioModificacion = Json.Usuario;
                                tipoDescuentoAsesorCoordinadorPw.FechaCreacion = DateTime.Now;
                                tipoDescuentoAsesorCoordinadorPw.FechaModificacion = DateTime.Now;
                                tipoDescuentoAsesorCoordinadorPw.Estado = true;
                            }

                            tipoDescuento.TipoDescuentoAsesorCoordinadorPw.Add(tipoDescuentoAsesorCoordinadorPw);
                        }

                        repTipoDescuento.Update(tipoDescuento);
                        scope.Complete();
                    }

                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarTipoDescuento(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDescuentoRepositorio repTipoDescuento = new TipoDescuentoRepositorio();
                TipoDescuentoAsesorCoordinadorPwRepositorio repTipoDescuentoAsesorCoordinadorPw = new TipoDescuentoAsesorCoordinadorPwRepositorio();

                if (repTipoDescuento.Exist(Id))
                {
                    repTipoDescuento.Delete(Id, Usuario);

                    var hijosTipoDescuentoAsesor = repTipoDescuentoAsesorCoordinadorPw.GetBy(x => x.IdTipoDescuento == Id);
                    foreach (var hijo in hijosTipoDescuentoAsesor)
                    {
                        repTipoDescuentoAsesorCoordinadorPw.Delete(hijo.Id, Usuario);
                    }
                                        
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarPrograma([FromBody] TipoDescuentoProgramaDTO Json)
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
                    repDescuento.EliminacionLogicoTipoDescuento(Json.IdTipoDescuento, Json.Usuario, Json.IdPgeneral);
                    foreach (var item in Json.IdPgeneral)
                    {
                        bool flag = false;
                        if (descuentosPromocion.Any(x => x.IdTipoDescuento == Json.IdTipoDescuento))
                        {
                            flag = true;
                        }


                        PgeneralTipoDescuentoBO descuento;
                        if (repDescuento.Exist(x => x.IdTipoDescuento == Json.IdTipoDescuento && x.IdPgeneral == item))
                        {
                            descuento = repDescuento.FirstBy(x => x.IdTipoDescuento == Json.IdTipoDescuento && x.IdPgeneral == item);
                            descuento.IdPgeneral = item;
                            descuento.IdTipoDescuento = Json.IdTipoDescuento;
                            descuento.FlagPromocion = flag;
                            descuento.UsuarioModificacion = Json.Usuario;
                            descuento.FechaModificacion = DateTime.Now;

                            repDescuento.Update(descuento);
                        }
                        else
                        {
                            descuento = new PgeneralTipoDescuentoBO();
                            descuento.IdPgeneral = item;
                            descuento.IdTipoDescuento = Json.IdTipoDescuento;
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

    }

    public class ValidadorTipoDescuentoDTO : AbstractValidator<TTipoDescuento>
    {
        public static ValidadorTipoDescuentoDTO Current = new ValidadorTipoDescuentoDTO();
        public ValidadorTipoDescuentoDTO()
        {
            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("Codigo es Obligatorio")
                                            .Length(1, 100).WithMessage("Codigo debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 100).WithMessage("Descripcion debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
