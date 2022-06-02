using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EstadoActividadDetalle")]
    public class EstadoActividadDetalleController : BaseController<TEstadoActividadDetalle, ValidadorEstadoActividadDetalleDTO>
    {
        public EstadoActividadDetalleController(IIntegraRepository<TEstadoActividadDetalle> repositorio, ILogger<BaseController<TEstadoActividadDetalle, ValidadorEstadoActividadDetalleDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerParaFiltro()
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                EstadoActividadDetalleRepositorio _repEstadoActividadDetalle = new EstadoActividadDetalleRepositorio(contexto);
                return Ok(_repEstadoActividadDetalle.ObtenerDetalleActividadFiltroCodigo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    

    public class ValidadorEstadoActividadDetalleDTO : AbstractValidator<TEstadoActividadDetalle>
    {
        public static ValidadorEstadoActividadDetalleDTO Current = new ValidadorEstadoActividadDetalleDTO();
        public ValidadorEstadoActividadDetalleDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 100).WithMessage("Descripcion debe tener 1 caracter minimo y 100 maximo");
            
        }
    }
}
