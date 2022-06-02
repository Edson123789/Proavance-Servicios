using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.DTOs;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CategoriaOrigen")]
    public class CategoriaOrigenController : BaseController<TCategoriaOrigen, ValidadorCategoriaOrigenDTO>
    {
        public CategoriaOrigenController(IIntegraRepository<TCategoriaOrigen> repositorio, ILogger<BaseController<TCategoriaOrigen, ValidadorCategoriaOrigenDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerOrigenChat()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nombre = "Chat";
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                return Ok(_repCategoriaOrigen.ObtenerCategoriaOrigenPorNombre(nombre));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                CategoriaOrigenBO categoriaOrigen = new CategoriaOrigenBO();
                return Ok(categoriaOrigen.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCategoriaOrigenLista([FromBody] FiltroPaginadorDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                var lista = _repCategoriaOrigen.ListarCategoriaOrigen(Filtro);
                var registro = lista.FirstOrDefault();
                return Ok(new { Data = lista, Total = registro.Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoDatoFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(contexto);
                var CategoriaOrigen = _repCategoriaOrigen.TipoDatoFiltro();
                return Ok(new { data = CategoriaOrigen });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltrosCategoriaOrigenDTO filtroCategoriaOrigen = new FiltrosCategoriaOrigenDTO
                {
                    filtroTipoDato = _repCategoriaOrigen.TipoDatoFiltro(),
                    filtroProcedenciaformulario = _repCategoriaOrigen.ProcedenciaFormularioFiltro(),
                    filtroProveedorCampania = _repCategoriaOrigen.ProveedorCampaniaIntegraFiltro(),
                    filtroTipoCategoriaOrigen = _repCategoriaOrigen.TipoCategoriaOrigenFiltro(),
                    filtroTipoCategoriaOrigenTodo = _repCategoriaOrigen.TipoCategoriaOrigenFiltroTodo(),
                    filtroTipoInteraccion = _repCategoriaOrigen.TiposInteraccionPorPorcedenciaFiltro()
                };


                return Ok(filtroCategoriaOrigen);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ActualizarCategoriaOrigen([FromBody]CategoriaOrigenDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(contexto);
                SubCategoriaDatoRepositorio _repSubCategoriaDato = new SubCategoriaDatoRepositorio(contexto);

                CategoriaOrigenBO categoriaOrigen = new CategoriaOrigenBO();
                categoriaOrigen = _repCategoriaOrigen.FirstById(Objeto.Id);

                categoriaOrigen.Nombre = Objeto.Nombre;
                categoriaOrigen.Descripcion = Objeto.Descripcion;
                categoriaOrigen.IdTipoDato = Objeto.IdTipoDato;
                categoriaOrigen.IdTipoCategoriaOrigen = Objeto.IdTipoCategoriaOrigen;
                categoriaOrigen.Meta = Objeto.Meta;
                categoriaOrigen.IdProveedorCampaniaIntegra = Objeto.IdProveedorCampaniaIntegra;
                categoriaOrigen.IdFormularioProcedencia = Objeto.IdFormularioProcedencia;
                categoriaOrigen.Considerar = Objeto.Considerar;
                categoriaOrigen.CodigoOrigen = Objeto.CodigoOrigen;
                categoriaOrigen.Estado = true;
                categoriaOrigen.UsuarioModificacion = Objeto.Usuario;
                categoriaOrigen.FechaModificacion = DateTime.Now;
                if (!categoriaOrigen.HasErrors)
                {
                    var CodigosTipoInteraccion = _repCategoriaOrigen.ObtenerTipoInteraccionporTipoFormulario(Objeto.IdFormularioProcedencia.Value);
                    var validacionTipo = _repCategoriaOrigen.GetBy(o => true, x => new { x.Id, x.IdFormularioProcedencia}).Where(x => x.Id == Objeto.Id).FirstOrDefault();
                    if (validacionTipo.IdFormularioProcedencia!=Objeto.IdFormularioProcedencia)
                    {
                        var lista = _repSubCategoriaDato.GetBy(o => true, x => new { x.Id, x.IdCategoriaOrigen, x.IdTipoFormulario }).Where(x => x.IdCategoriaOrigen == Objeto.Id).ToList();

                        foreach (var item in lista)
                        {
                           _repSubCategoriaDato.Delete(item.Id,Objeto.Usuario);                              
                        }

                        foreach (var Codigo in CodigosTipoInteraccion)
                        {
                            try
                            {
                                SubCategoriaDatoBO subCategoriaDato = new SubCategoriaDatoBO()
                                {
                                    IdCategoriaOrigen = Objeto.Id,
                                    IdTipoFormulario = Codigo.IdTipoInteraccion,
                                    Estado = true,
                                    UsuarioCreacion = Objeto.Usuario,
                                    UsuarioModificacion = Objeto.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                };
                                _repSubCategoriaDato.Insert(subCategoriaDato);
                            }
                            catch (Exception Ex)
                            {
                                return BadRequest(Ex.Message);
                            }
                        }
                    }
                    var rpta = _repCategoriaOrigen.Update(categoriaOrigen);

                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(categoriaOrigen.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarCategoriaOrigen([FromBody]CategoriaOrigenDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(contexto);
                SubCategoriaDatoRepositorio _repSubCategoriaDato = new SubCategoriaDatoRepositorio(contexto);
                CategoriaOrigenBO categoriaOrigen = new CategoriaOrigenBO();
                categoriaOrigen.Id = Objeto.Id;
                categoriaOrigen.Nombre = Objeto.Nombre;
                categoriaOrigen.Descripcion = Objeto.Descripcion;
                categoriaOrigen.IdTipoDato = Objeto.IdTipoDato;
                categoriaOrigen.IdTipoCategoriaOrigen = Objeto.IdTipoCategoriaOrigen;
                categoriaOrigen.Meta = Objeto.Meta;
                categoriaOrigen.IdProveedorCampaniaIntegra = Objeto.IdProveedorCampaniaIntegra;
                categoriaOrigen.IdFormularioProcedencia = Objeto.IdFormularioProcedencia;
                categoriaOrigen.Considerar = Objeto.Considerar;
                categoriaOrigen.CodigoOrigen = Objeto.CodigoOrigen;
                categoriaOrigen.Estado = true;
                categoriaOrigen.UsuarioModificacion = Objeto.Usuario;
                categoriaOrigen.UsuarioCreacion = Objeto.Usuario;
                categoriaOrigen.FechaCreacion = DateTime.Now;
                categoriaOrigen.FechaModificacion = DateTime.Now;
                if (!categoriaOrigen.HasErrors)
                {
                    var rpta = _repCategoriaOrigen.Insert(categoriaOrigen);
                    var CodigosTipoInteraccion = _repCategoriaOrigen.ObtenerTipoInteraccionporTipoFormulario(Objeto.IdFormularioProcedencia.Value);
                    var lista = _repCategoriaOrigen.GetBy(o => true, x => new { x.Id, x.FechaCreacion }).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();

                    foreach (var Codigo in CodigosTipoInteraccion)
                    {
                        try
                        {
                            SubCategoriaDatoBO subCategoriaDato = new SubCategoriaDatoBO()
                            {
                                IdCategoriaOrigen = lista.Id,
                                IdTipoFormulario = Codigo.IdTipoInteraccion,
                                Estado = true,
                                UsuarioCreacion = Objeto.Usuario,
                                UsuarioModificacion = Objeto.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };
                            _repSubCategoriaDato.Insert(subCategoriaDato);
                        }
                        catch (Exception Ex)
                        {
                            return BadRequest(Ex.Message);
                        }
                    }
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(categoriaOrigen.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public IActionResult EliminarCategoriaOrigen(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                SubCategoriaDatoRepositorio _repSubCategoriaDato = new SubCategoriaDatoRepositorio();

  
                    if (_repCategoriaOrigen.Exist(Id))
                    {
                        var lista = _repSubCategoriaDato.GetBy(o => true, x => new { x.Id, x.IdCategoriaOrigen, x.IdTipoFormulario }).Where(x => x.IdCategoriaOrigen == Id).ToList();

                        foreach (var item in lista)
                        {
                            _repSubCategoriaDato.Delete(item.Id,Usuario);
                        }
                        _repCategoriaOrigen.Delete(Id, Usuario);
                    }
              
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

    public class ValidadorCategoriaOrigenDTO : AbstractValidator<TCategoriaOrigen>
    {
        public static ValidadorCategoriaOrigenDTO Current = new ValidadorCategoriaOrigenDTO();
        public ValidadorCategoriaOrigenDTO()
        {
        }
    }




}