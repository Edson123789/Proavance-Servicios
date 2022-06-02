using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoDatoMeta")]
    [ApiController]
    public class TipoDatoMetaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoDatoMetaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                TipoDatoMetaRepositorio tipoDatoMetaRepositorio = new TipoDatoMetaRepositorio(_integraDBContext);
                return Ok(tipoDatoMetaRepositorio.ObtenerTodoGrid());
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
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio(_integraDBContext);
                CombosTipoDatoMetaDTO combos = new CombosTipoDatoMetaDTO
                {
                    FaseOportunidadFiltroCodigo = _repFaseOportunidad.ObtenerFasesOportunidadFiltroCodigo(),
                    TipoDatoFiltro = _repTipoDato.ObtenerFiltro()
                };
                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoDatoMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoMetaRepositorio tipoDatoMetaRepositorio = new TipoDatoMetaRepositorio(_integraDBContext);

                TipoDatoMetaBO tipoDatoMetaBO = new TipoDatoMetaBO();
                tipoDatoMetaBO.IdFaseOportunidadOrigen = Json.IdFaseOportunidadOrigen;
                tipoDatoMetaBO.IdFaseOportunidadDestino = Json.IdFaseOportunidadDestino;
                tipoDatoMetaBO.IdTipoDato = Json.IdTipoDato;
                tipoDatoMetaBO.Meta = Json.Meta;
                tipoDatoMetaBO.MetaGlobal = Json.MetaGlobal;
                tipoDatoMetaBO.Estado = true;
                tipoDatoMetaBO.UsuarioCreacion = Json.Usuario;
                tipoDatoMetaBO.UsuarioModificacion = Json.Usuario;
                tipoDatoMetaBO.FechaCreacion = DateTime.Now;
                tipoDatoMetaBO.FechaModificacion = DateTime.Now;

                return Ok(tipoDatoMetaRepositorio.Insert(tipoDatoMetaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] TipoDatoMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoMetaRepositorio tipoDatoMetaRepositorio = new TipoDatoMetaRepositorio(_integraDBContext);

                TipoDatoMetaBO tipoDatoMetaBO = tipoDatoMetaRepositorio.FirstById(Json.Id);
                tipoDatoMetaBO.IdFaseOportunidadOrigen = Json.IdFaseOportunidadOrigen;
                tipoDatoMetaBO.IdFaseOportunidadDestino = Json.IdFaseOportunidadDestino;
                tipoDatoMetaBO.IdTipoDato = Json.IdTipoDato;
                tipoDatoMetaBO.Meta = Json.Meta;
                tipoDatoMetaBO.MetaGlobal = Json.MetaGlobal;
                tipoDatoMetaBO.UsuarioModificacion = Json.Usuario;
                tipoDatoMetaBO.FechaModificacion = DateTime.Now;

                return Ok(tipoDatoMetaRepositorio.Update(tipoDatoMetaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody]TipoDatoMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDatoMetaRepositorio tipoDatoMetaRepositorio = new TipoDatoMetaRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (tipoDatoMetaRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = tipoDatoMetaRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class ValidadorGrupoFiltroProgramaCriticoDTO : AbstractValidator<GrupoFiltroProgramaCriticoBO>
        {
            public static ValidadorGrupoFiltroProgramaCriticoDTO Current = new ValidadorGrupoFiltroProgramaCriticoDTO();
            public ValidadorGrupoFiltroProgramaCriticoDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");
                RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio");
            }
        }
    }
}
