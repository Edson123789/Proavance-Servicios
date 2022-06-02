using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Aerolinea")]
    public class AerolineaOpeController : BaseController<TRaAerolinea, ValidadorRaAerolineaDTO>
    {
        public AerolineaOpeController(IIntegraRepository<TRaAerolinea> repositorio, ILogger<BaseController<TRaAerolinea, ValidadorRaAerolineaDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                RaAerolineaRepositorio _repAerolinea = new RaAerolineaRepositorio();
                return Ok(_repAerolinea.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.LinkBoarding }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] AerolineaDTO Aerolinea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaAerolineaRepositorio _repAerolinea = new RaAerolineaRepositorio();
                if (_repAerolinea.GetBy(x => x.Nombre == Aerolinea.Nombre).Count() == 0)
                {
                    RaAerolineaBO raAerolineaBO = new RaAerolineaBO()
                    {
                        Nombre = Aerolinea.Nombre,
                        LinkBoarding = Aerolinea.LinkBoarding,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Aerolinea.NombreUsuario,
                        UsuarioModificacion = Aerolinea.NombreUsuario
                    };
                    if (!raAerolineaBO.HasErrors)
                    {
                        _repAerolinea.Insert(raAerolineaBO);
                    }
                    else
                    {
                        return BadRequest(raAerolineaBO.ActualesErrores);
                    }
                    return Ok(new { raAerolineaBO.Id, raAerolineaBO.Nombre, raAerolineaBO.LinkBoarding });
                }
                else{
                    return BadRequest("La Aerolínea ingresada ya existe en la base de datos.");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] AerolineaDTO Aerolinea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaAerolineaRepositorio _repAerolinea = new RaAerolineaRepositorio();
                if (_repAerolinea.Exist(Aerolinea.Id))
                {
                    var raAerolineaBO = _repAerolinea.FirstById(Aerolinea.Id);
                    raAerolineaBO.Nombre = Aerolinea.Nombre;
                    raAerolineaBO.LinkBoarding = Aerolinea.LinkBoarding;
                    raAerolineaBO.Estado = true;
                    raAerolineaBO.FechaModificacion = DateTime.Now;
                    raAerolineaBO.UsuarioModificacion = Aerolinea.NombreUsuario;
                    if (!raAerolineaBO.HasErrors)
                    {
                        _repAerolinea.Update(raAerolineaBO);
                    }
                    else
                    {
                        return BadRequest(raAerolineaBO.ActualesErrores);
                    }
                    return Ok(new{ raAerolineaBO.Id , raAerolineaBO.Nombre, raAerolineaBO.LinkBoarding });
                }
                else
                {
                    return BadRequest("La Aerolínea no existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

    public class ValidadorRaAerolineaDTO : AbstractValidator<TRaAerolinea>
    {
        public static ValidadorRaAerolineaDTO Current = new ValidadorRaAerolineaDTO();
        public ValidadorRaAerolineaDTO()
        {
        }
    }
}