using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Movilidad")]
    public class MovilidadOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                RaMovilidadRepositorio _repMovilidad= new RaMovilidadRepositorio();
                return Ok(_repMovilidad.GetBy(x => x.Estado, x => new { x.Id, x.Nombre, x.Telefono, x.IdTipoMovilidad, x.IdCiudad }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MovilidadDTO Movilidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaMovilidadRepositorio _repMovilidad = new RaMovilidadRepositorio();
                if (_repMovilidad.GetBy(x => x.Nombre == Movilidad.Nombre).Count() == 0)
                {
                    RaMovilidadBO raMovilidadBO = new RaMovilidadBO()
                    {
                        Nombre = Movilidad.Nombre,
                        Telefono = Movilidad.Telefono,
                        IdTipoMovilidad = Movilidad.IdTipoMovilidad,
                        IdCiudad = Movilidad.IdCiudad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Movilidad.NombreUsuario,
                        UsuarioModificacion = Movilidad.NombreUsuario
                    };
                    if (!raMovilidadBO.HasErrors)
                    {
                        _repMovilidad.Insert(raMovilidadBO);
                    }
                    else
                    {
                        return BadRequest(raMovilidadBO.ActualesErrores);
                    }
                    return Ok(new { raMovilidadBO.Id, raMovilidadBO.Nombre, raMovilidadBO.Telefono, raMovilidadBO.IdTipoMovilidad, raMovilidadBO.IdCiudad });
                }
                else {
                    return BadRequest("La Movilidad ingresada ya existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MovilidadDTO Movilidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaMovilidadRepositorio _repMovilidad = new RaMovilidadRepositorio();

                if (_repMovilidad.Exist(Movilidad.Id))
                {
                    var raMovilidadBO = _repMovilidad.FirstById(Movilidad.Id);
                    raMovilidadBO.Nombre = Movilidad.Nombre;
                    raMovilidadBO.Telefono = Movilidad.Telefono;
                    raMovilidadBO.IdCiudad = Movilidad.IdCiudad;
                    raMovilidadBO.IdTipoMovilidad = Movilidad.IdTipoMovilidad;
                    raMovilidadBO.Estado = true;
                    raMovilidadBO.FechaModificacion = DateTime.Now;
                    raMovilidadBO.UsuarioModificacion = Movilidad.NombreUsuario;
                    if (!raMovilidadBO.HasErrors)
                    {
                        _repMovilidad.Update(raMovilidadBO);
                    }
                    else
                    {
                        return BadRequest(raMovilidadBO.ActualesErrores);
                    }
                    return Ok(new { raMovilidadBO.Id, raMovilidadBO.Nombre, raMovilidadBO.Telefono, raMovilidadBO.IdTipoMovilidad, raMovilidadBO.IdCiudad });
                }
                else
                {
                    return BadRequest("La Movilidad no existe en la base de datos.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}