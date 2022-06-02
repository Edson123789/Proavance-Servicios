using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ExpositorPorArea")]
    public class ExpositorPorAreaController : BaseController<TTipoDato, ValidadoExpositorAreaDTO>
    {
        public ExpositorPorAreaController(IIntegraRepository<TTipoDato> repositorio, ILogger<BaseController<TTipoDato, ValidadoExpositorAreaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboExpositorArea()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                AreaRepositorio _repArea = new AreaRepositorio();
                ComboExpositorAreaDTO comboExpositorArea = new ComboExpositorAreaDTO();

                comboExpositorArea.ExpositorFiltro = _repExpositor.ObtenerExpositoresFiltro();
                comboExpositorArea.AreaFiltro = _repArea.ObtenerAreas();
                return Ok(comboExpositorArea);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorPorAreaRepositorio _repExpositorPorArea = new ExpositorPorAreaRepositorio();
                return Ok(_repExpositorPorArea.ObtenerTodoExpositorPorArea());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ExpositorPorAreaDTO expositorPorArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorPorAreaRepositorio _repExpositorPorArea = new ExpositorPorAreaRepositorio();
                ExpositorPorAreaBO expositorPorAreaoBO = new ExpositorPorAreaBO();
                List<ExpositorPorAreaBO> lista = new List<ExpositorPorAreaBO>(); 

                foreach (var item in expositorPorArea.IdArea)
                {
                    expositorPorAreaoBO = new ExpositorPorAreaBO();
                    expositorPorAreaoBO.IdExpositor = expositorPorArea.IdExpositor;
                    expositorPorAreaoBO.IdArea = item;
                    expositorPorAreaoBO.Estado = true;
                    expositorPorAreaoBO.UsuarioCreacion = expositorPorArea.Usuario;
                    expositorPorAreaoBO.UsuarioModificacion = expositorPorArea.Usuario;
                    expositorPorAreaoBO.FechaCreacion = DateTime.Now;
                    expositorPorAreaoBO.FechaModificacion = DateTime.Now;

                    lista.Add(expositorPorAreaoBO);
                }

                return Ok(_repExpositorPorArea.Insert(lista));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] ExpositorPorAreaListadoDTO expositorPorArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ExpositorPorAreaRepositorio _repExpositorPorArea = new ExpositorPorAreaRepositorio();

                    if (_repExpositorPorArea.Exist(expositorPorArea.Id))
                    {
                        _repExpositorPorArea.Delete(expositorPorArea.Id, expositorPorArea.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class ValidadoExpositorAreaDTO : AbstractValidator<TTipoDato>
    {
        public static ValidadoExpositorAreaDTO Current = new ValidadoExpositorAreaDTO();
        public ValidadoExpositorAreaDTO()
        {
        }
    }
}
