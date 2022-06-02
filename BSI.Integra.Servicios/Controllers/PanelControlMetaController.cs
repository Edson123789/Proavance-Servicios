using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
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
using static BSI.Integra.Servicios.Controllers.PanelControlMetaController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PanelControlMeta")]
    [ApiController]
    public class PanelControlMetaController : BaseController<TPanelControlMeta, ValidadorPanelControlMetaDTO>
    {
        public PanelControlMetaController(IIntegraRepository<TPanelControlMeta> repositorio, ILogger<BaseController<TPanelControlMeta, ValidadorPanelControlMetaDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();
                return Ok(panelControlMetaRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();

                PanelControlMetaBO panelControlMetaBO = new PanelControlMetaBO();
                panelControlMetaBO.Nombre = Json.Nombre;
                panelControlMetaBO.Meta = Json.Meta;
                panelControlMetaBO.Estado = true;
                panelControlMetaBO.UsuarioCreacion = Json.Usuario;
                panelControlMetaBO.UsuarioModificacion = Json.Usuario;
                panelControlMetaBO.FechaCreacion = DateTime.Now;
                panelControlMetaBO.FechaModificacion = DateTime.Now;

                return Ok(panelControlMetaRepositorio.Insert(panelControlMetaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();

                PanelControlMetaBO panelControlMetaBO = panelControlMetaRepositorio.FirstById(Json.Id);
                panelControlMetaBO.Nombre = Json.Nombre;
                panelControlMetaBO.Meta = Json.Meta;
                panelControlMetaBO.UsuarioModificacion = Json.Usuario;
                panelControlMetaBO.FechaModificacion = DateTime.Now;
                
                return Ok(panelControlMetaRepositorio.Update(panelControlMetaBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(PanelControlMetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PanelControlMetaRepositorio panelControlMetaRepositorio = new PanelControlMetaRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (panelControlMetaRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = panelControlMetaRepositorio.Delete(Json.Id, Json.Usuario);
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

        public class ValidadorPanelControlMetaDTO : AbstractValidator<TPanelControlMeta>
        {
            public static ValidadorPanelControlMetaDTO Current = new ValidadorPanelControlMetaDTO();
            public ValidadorPanelControlMetaDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}
