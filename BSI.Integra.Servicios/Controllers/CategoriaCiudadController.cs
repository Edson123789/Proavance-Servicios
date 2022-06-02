using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Troncal")]//Referencia a CategoriaCiudad Tabla Principal que maneja servicio Troncales
    public class CategoriaCiudadController : BaseController<TCategoriaCiudad, ValidadorCategoriaCiudadDTO>
    {
        public CategoriaCiudadController(IIntegraRepository<TCategoriaCiudad> repositorio, ILogger<BaseController<TCategoriaCiudad, ValidadorCategoriaCiudadDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

      


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRegistrosTroncal()
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaCiudadRepositorio _repTroncalCiudad = new CategoriaCiudadRepositorio(contexto);
                var RegistroTroncal = _repTroncalCiudad.ObtenerTroncalLista();
                return Ok(new { data = RegistroTroncal, Total = RegistroTroncal.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerLocacionTroncalFiltro()
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaCiudadRepositorio _repTroncalCiudad = new CategoriaCiudadRepositorio(contexto);
                var RegistroLocacionTroncal = _repTroncalCiudad.ObtenerLocacionTroncalFiltro();
                return Ok(new { data = RegistroLocacionTroncal });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult GetAllCategoriaPrograma()
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaProgramaRepositorio _repoCategoriaPrograma = new CategoriaProgramaRepositorio(contexto);
                var categorias = _repoCategoriaPrograma.ObtenerCategoriasPrograma();
                return Ok(new { data = categorias });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }


        [Route("[Action]/{Id}")]
        [HttpPost]
        public ActionResult ActualizarTroncal([FromBody]CategoriaCiudadTroncalDTO Objeto, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaCiudadRepositorio _repTroncalCiudad = new CategoriaCiudadRepositorio(contexto);
                var lista = _repTroncalCiudad.GetBy(o => true, x => new { x.Id, x.IdCategoriaPrograma,x.IdRegionCiudad,x.TroncalCompleto }).Where(x => x.Id == Objeto.Id).FirstOrDefault();
                var rpta = false;
                if (Objeto.TroncalCompleto==lista.TroncalCompleto)
                {
                    if (Objeto.IdCategoriaPrograma == lista.IdCategoriaPrograma&& Objeto.IdRegionCiudad == lista.IdRegionCiudad)
                    {                    
                            throw new System.Exception("Troncal ya Existe");                      
                    }
                    else
                    {


                        CategoriaCiudadBO categoriaCiudad = new CategoriaCiudadBO(Id);

                        categoriaCiudad.Id = Id;
                        categoriaCiudad.IdCategoriaPrograma = Objeto.IdCategoriaPrograma;
                        categoriaCiudad.IdRegionCiudad = Objeto.IdRegionCiudad;
                        categoriaCiudad.TroncalCompleto = Objeto.TroncalCompleto;
                        categoriaCiudad.UsuarioModificacion = Objeto.UsuarioModificacion;
                        categoriaCiudad.FechaModificacion = DateTime.Now;

                        if (!categoriaCiudad.HasErrors)
                        {
                             rpta = _repTroncalCiudad.Update(categoriaCiudad);
                        }
                     

                    }
                }
                if (Objeto.IdCategoriaPrograma == lista.IdCategoriaPrograma && Objeto.IdRegionCiudad == lista.IdRegionCiudad)
                {
                    if (Objeto.TroncalCompleto == lista.TroncalCompleto)
                    {
                        throw new System.Exception("Troncal ya Existe");
                    }
                    else
                    {
                        CategoriaCiudadBO categoriaCiudad = new CategoriaCiudadBO(Id);

                        categoriaCiudad.Id = Id;
                        categoriaCiudad.IdCategoriaPrograma = Objeto.IdCategoriaPrograma;
                        categoriaCiudad.IdRegionCiudad = Objeto.IdRegionCiudad;
                        categoriaCiudad.TroncalCompleto = Objeto.TroncalCompleto;
                        categoriaCiudad.UsuarioModificacion = Objeto.UsuarioModificacion;
                        categoriaCiudad.FechaModificacion = DateTime.Now;

                        if (!categoriaCiudad.HasErrors)
                        {
                             rpta = _repTroncalCiudad.Update(categoriaCiudad);
                        }
                        else
                        {
                            return BadRequest(categoriaCiudad.GetErrors(null));
                        }

                    }
                }

                return Ok(new { data = rpta });


            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]/")]
        [HttpPost]
        public ActionResult InsertarTroncal([FromBody]CategoriaCiudadTroncalDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                CategoriaCiudadRepositorio _repTroncalCiudad = new CategoriaCiudadRepositorio(contexto);

                var validarCiudadCategoria = _repTroncalCiudad.ValidarCiudadCategoria(Objeto.IdCategoriaPrograma, Objeto.IdRegionCiudad);
                var validarTroncal = _repTroncalCiudad.ValidarTroncal(Objeto.TroncalCompleto);
                if (validarTroncal != null)
                {
                    if (Objeto.TroncalCompleto.Equals(validarTroncal.TroncalCompleto))
                        throw new ArgumentException("Troncal ya Existe");
                }
                if (validarCiudadCategoria != null)
                {
                    if (Objeto.IdCategoriaPrograma==validarCiudadCategoria.IdCategoriaPrograma && Objeto.IdRegionCiudad==validarCiudadCategoria.IdRegionCiudad)
                        throw new ArgumentException("Troncal ya Existe");
                }
                CategoriaCiudadBO categoriaCiudad = new CategoriaCiudadBO();

                categoriaCiudad.Id = Objeto.Id;
                categoriaCiudad.IdCategoriaPrograma = Objeto.IdCategoriaPrograma;
                categoriaCiudad.IdRegionCiudad = Objeto.IdRegionCiudad;
                categoriaCiudad.TroncalCompleto = Objeto.TroncalCompleto;
                categoriaCiudad.UsuarioModificacion = Objeto.UsuarioModificacion;
                categoriaCiudad.UsuarioCreacion = Objeto.UsuarioCreacion;
                categoriaCiudad.Estado = true;
                categoriaCiudad.FechaCreacion = DateTime.Now;
                categoriaCiudad.FechaModificacion = DateTime.Now;


                if (!categoriaCiudad.HasErrors)
                {
                    var rpta = _repTroncalCiudad.Update(categoriaCiudad);
                    return Ok(new { data = rpta });
                }
                else
                {
                    return BadRequest(categoriaCiudad.GetErrors(null));
                }



            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

       

    }

    public class ValidadorCategoriaCiudadDTO : AbstractValidator<TCategoriaCiudad>
    {
        public static ValidadorCategoriaCiudadDTO Current = new ValidadorCategoriaCiudadDTO();
        public ValidadorCategoriaCiudadDTO()
        {
            RuleFor(objeto => objeto.IdCategoriaPrograma).NotEmpty().WithMessage("IdCategoria Programa es Obligatorio");
            RuleFor(objeto => objeto.TroncalCompleto).NotEmpty().WithMessage("IdTroncalCompleto es Obligatorio");
            RuleFor(objeto => objeto.IdRegionCiudad).NotEmpty().WithMessage("IdRegionCiudad es Obligatorio");
        }

    }
}
