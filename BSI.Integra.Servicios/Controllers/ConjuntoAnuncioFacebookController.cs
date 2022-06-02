using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Aplicacion.Marketing.BO;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{

    [Route("api/ConjuntoAnuncioFacebook")]
    public class ConjuntoAnuncioFacebookController : BaseController<TConjuntoAnuncioFacebook, ValidadorConjuntoAnuncioFacebookDTO>
    {
        public ConjuntoAnuncioFacebookController(IIntegraRepository<TConjuntoAnuncioFacebook> repositorio, ILogger<BaseController<TConjuntoAnuncioFacebook, ValidadorConjuntoAnuncioFacebookDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerConjuntoAnuncioFB([FromBody] FiltroPaginadorDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioFacebookRepositorio _repConjuntoFB = new ConjuntoAnuncioFacebookRepositorio();
                var lista = _repConjuntoFB.ListarConjuntoAnuncioFB(Filtro);
                var registro = lista.FirstOrDefault();
                return Ok(new { Data = lista, Total = registro.Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConjuntoAnuncioFBPendientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ConjuntoAnuncioFacebookRepositorio _repConjuntoFB = new ConjuntoAnuncioFacebookRepositorio(contexto);
                var ConjuntoFB = _repConjuntoFB.ListarConjuntoAnuncioFBPendiente();
                return Ok(new { Data = ConjuntoFB, Total = ConjuntoFB.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConjuntoAnunciosAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ConjuntoAnuncioFacebookRepositorio _repConjuntoFB = new ConjuntoAnuncioFacebookRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(_repConjuntoFB.ConjutoAnuncioAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



        [Route("[Action]/")]
        [HttpPost]
        public ActionResult ActualizarConjuntoAnuncioFacebook([FromBody]ConjuntoAnuncioFacebookDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio(contexto);
                ConjuntoAnuncioFacebookRepositorio _repConjuntoFB = new ConjuntoAnuncioFacebookRepositorio(contexto);
                ConjuntoAnuncioFacebookBO conjuntoAnuncioFacebook = new ConjuntoAnuncioFacebookBO();
                ConjuntoAnuncioBO conjuntoAnuncio = new ConjuntoAnuncioBO();

                conjuntoAnuncioFacebook = _repConjuntoFB.FirstById(Objeto.Id);
                conjuntoAnuncioFacebook.IdAnuncioFacebook = Objeto.IdAnuncioFacebook;
                conjuntoAnuncioFacebook.EsOtros = Objeto.EsOtros;
                if (Objeto.EsOtros == true)
                {
                    conjuntoAnuncioFacebook.IdConjuntoAnuncio = null;
                }
                else
                {
                    conjuntoAnuncioFacebook.IdConjuntoAnuncio = Objeto.IdConjuntoAnuncio;
                }
                conjuntoAnuncioFacebook.Estado = true;
                conjuntoAnuncioFacebook.UsuarioModificacion = Objeto.Usuario;
                conjuntoAnuncioFacebook.FechaModificacion = DateTime.Now;

                if (Objeto.IdConjuntoAnuncio.HasValue)
                {
                    conjuntoAnuncio = _repConjuntoAnuncio.FirstById(Objeto.IdConjuntoAnuncio.Value);
                    conjuntoAnuncio.IdConjuntoAnuncioFacebook = Objeto.IdAnuncioFacebook;
                    conjuntoAnuncio.Estado = true;
                    conjuntoAnuncio.UsuarioModificacion = Objeto.Usuario;
                    conjuntoAnuncio.FechaModificacion = DateTime.Now;
                    if (!conjuntoAnuncio.HasErrors)
                    {
                        _repConjuntoAnuncio.Update(conjuntoAnuncio);
                    }
                    else
                    {
                        return BadRequest(conjuntoAnuncio.GetErrors(null));
                    }
                }

                if (!conjuntoAnuncioFacebook.HasErrors)
                {
                    var rpta = _repConjuntoFB.Update(conjuntoAnuncioFacebook);
                    return Ok(conjuntoAnuncioFacebook);
                }
                else
                {
                    return BadRequest(conjuntoAnuncioFacebook.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }




    }





















    public class ValidadorConjuntoAnuncioFacebookDTO : AbstractValidator<TConjuntoAnuncioFacebook>
    {
        public static ValidadorConjuntoAnuncioFacebookDTO Current = new ValidadorConjuntoAnuncioFacebookDTO();


        public ValidadorConjuntoAnuncioFacebookDTO()
        {
        }








    }
}