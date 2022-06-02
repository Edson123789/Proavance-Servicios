using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
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
    [Route("api/RegionCiudad")]
    public class RegionCiudadController : BaseController<TRegionCiudad, ValidadorRegionCiudadDTO>
    {
        public RegionCiudadController(IIntegraRepository<TRegionCiudad> repositorio, ILogger<BaseController<TRegionCiudad, ValidadorRegionCiudadDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerTodoFiltro()
		{
			try
			{
				RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
				return Ok(_repRegionCiudad.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.IdCiudad}));
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPaises()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repoPais = new PaisRepositorio();
                var paises = _repoPais.ObtenerPaisesCombo();
                return Json(new { Result = "OK", Records = paises });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCiudades()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repoCiudad = new CiudadRepositorio();
                var Ciudades = _repoCiudad.ObtenerCiudadesFiltro();
                return Json(new { Result = "OK", Records = Ciudades });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarRegionCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                var RegionCiudad = _repRegionCiudad.ObtenerTodoRegionPais();
                return Json(new { Result = "OK", Records = RegionCiudad });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarRegionCiudad([FromBody] RegionCiudadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                RegionCiudadBO NuevaRegionCiudad = new RegionCiudadBO();

                NuevaRegionCiudad.Nombre = ObjetoDTO.Nombre;
                NuevaRegionCiudad.IdCiudad = ObjetoDTO.IdCiudad;
                NuevaRegionCiudad.IdPais = ObjetoDTO.IdPais;
                NuevaRegionCiudad.CodigoBs = ObjetoDTO.CodigoBs;
                NuevaRegionCiudad.DenominacionBs = ObjetoDTO.DenominacionBs;
                NuevaRegionCiudad.NombreCorto = ObjetoDTO.NombreCorto;
                NuevaRegionCiudad.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevaRegionCiudad.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevaRegionCiudad.FechaCreacion = DateTime.Now;
                NuevaRegionCiudad.FechaModificacion = DateTime.Now;
                NuevaRegionCiudad.Estado = true;


                _repRegionCiudad.Insert(NuevaRegionCiudad);

                return Ok(NuevaRegionCiudad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarRegionCiudad([FromBody] RegionCiudadDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                RegionCiudadBO RegionCiudad = _repRegionCiudad.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                RegionCiudad.Nombre = ObjetoDTO.Nombre;
                RegionCiudad.IdCiudad = ObjetoDTO.IdCiudad;
                RegionCiudad.IdPais = ObjetoDTO.IdPais;
                RegionCiudad.CodigoBs = ObjetoDTO.CodigoBs;
                RegionCiudad.DenominacionBs = ObjetoDTO.DenominacionBs;
                RegionCiudad.NombreCorto = ObjetoDTO.NombreCorto;
                RegionCiudad.UsuarioModificacion = ObjetoDTO.Usuario;
                RegionCiudad.FechaModificacion = DateTime.Now;
                RegionCiudad.Estado = true;

                _repRegionCiudad.Update(RegionCiudad);

                return Ok(RegionCiudad);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRegionCiudad([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                RegionCiudadBO RegionCiudad = _repRegionCiudad.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();

                _repRegionCiudad.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorRegionCiudadDTO : AbstractValidator<TRegionCiudad>
    {
        public static ValidadorRegionCiudadDTO Current = new ValidadorRegionCiudadDTO();
        public ValidadorRegionCiudadDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdCiudad).NotEmpty().WithMessage("IdRegion es Obligatorio");

            RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("IdPais es Obligatorio");

            RuleFor(objeto => objeto.CodigoBs).NotEmpty().WithMessage("CodigoBs es Obligatorio");

            RuleFor(objeto => objeto.DenominacionBs).NotEmpty().WithMessage("DenominacionBs es Obligatorio");

            RuleFor(objeto => objeto.NombreCorto).Length(1, 25).WithMessage("NombreCosto debe ser mayor a 1 y a mas");



        }

    }
}
