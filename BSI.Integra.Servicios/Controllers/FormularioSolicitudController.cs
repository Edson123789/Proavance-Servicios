using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Transactions;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormularioSolicitud")]
    public class FormularioSolicitudController : BaseController<TFormularioSolicitud, ValidadorFormularioSolicitudDTO>
    {
        public FormularioSolicitudController(IIntegraRepository<TFormularioSolicitud> repositorio, ILogger<BaseController<TFormularioSolicitud, ValidadorFormularioSolicitudDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerFormularioSolicitud([FromBody]FiltroCompuestroGrillaDTO paginador)
        {
           
            try
            {
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio();
                var FormularioSolicitud = _repFormularioSolicitud.ObtenerTodo(paginador);

                var Total = FormularioSolicitud.Count() == 0 ? 0 : FormularioSolicitud.FirstOrDefault().Total;

                return Ok(new { data = FormularioSolicitud, Total});
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerFormularioRespuestaFiltro([FromBody]Dictionary<string,string> Filtro)
        {
           
            try
            {
                if (Filtro != null)
                {
                    FormularioRespuestaRepositorio _repFormularioRespuesta = new FormularioRespuestaRepositorio();
                    return Ok(new { data = _repFormularioRespuesta.FormularioRespuestaFiltro(Filtro["valor"].ToString()) });

                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConjuntoAnuncioFiltro([FromBody]Dictionary<string, string> Filtro)
        {
           
            try
            {
                
                if (Filtro != null)
                {
                    FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio();
                    return Ok(new { data = _repFormularioSolicitud.ObtenerConjuntoAnunciosFiltro(Filtro["valor"].ToString()) });
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrosGenerales()
        {
           
            try
            {
                FormularioSolicitudTextoBotonRepositorio _repFormularioSolicitudTextoBoton = new FormularioSolicitudTextoBotonRepositorio();
                CampoContactoRepositorio _repCampoContacto = new CampoContactoRepositorio();
                CategoriaOrigenRepositorio _repCategoriaOrigenRepositorio = new CategoriaOrigenRepositorio();
                FormularioRespuestaRepositorio _repFormularioRespuesta = new FormularioRespuestaRepositorio();
                var data = new
                {
                    textoBoton = _repFormularioSolicitudTextoBoton.ObtenerTextoBotonFiltro(),
                    campoContacto = _repCampoContacto.ObtenerCamposContactoFiltro(),
                    categoriaOrigen = _repCategoriaOrigenRepositorio.ObtenerCategoriaFiltro(),
                    formularioRespuesta = _repFormularioRespuesta.FormularioRespuestaParaFiltro(),
                    campoContactoTodo = _repCampoContacto.ObtenerCamposContacto()
                    
                };
                

                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]/{IdFormulario}")]
        [HttpGet]
        public ActionResult ObtenerCampoFormulario(int IdFormulario)
        {
           
            try
            {
                CampoFormularioRepositorio  _repCampoFormulario = new CampoFormularioRepositorio();

                return Ok(new { data= _repCampoFormulario.ObtenerCampoFormulario(IdFormulario) });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFormularioSolicitud([FromBody]InsertarFormularioSolicitudCampoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(contexto);
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(contexto);

                FormularioSolicitudBO formularioSolicitud = new FormularioSolicitudBO();
                formularioSolicitud.IdFormularioRespuesta = obj.Formulario.IdFormularioRespuesta;
                formularioSolicitud.Nombre = obj.Formulario.Nombre;
                formularioSolicitud.Codigo = obj.Formulario.Codigo;
                formularioSolicitud.Campanha = obj.Formulario.NombreCampania;
                formularioSolicitud.IdConjuntoAnuncio = obj.Formulario.IdCampania;
                formularioSolicitud.Proveedor = obj.Formulario.Proveedor;
                formularioSolicitud.IdFormularioSolicitudTextoBoton = obj.Formulario.IdFormularioSolicitudTextoBoton;
                formularioSolicitud.TipoSegmento = obj.Formulario.TipoSegmento;
                formularioSolicitud.CodigoSegmento = obj.Formulario.CodigoSegmento;
                formularioSolicitud.TipoEvento = obj.Formulario.TipoEvento;
                formularioSolicitud.UrlbotonInvitacionPagina = obj.Formulario.UrlbotonInvitacionPagina;
                formularioSolicitud.Estado = true;
                formularioSolicitud.FechaCreacion = DateTime.Now;
                formularioSolicitud.FechaModificacion = DateTime.Now;
                formularioSolicitud.UsuarioCreacion = obj.Formulario.Usuario;
                formularioSolicitud.UsuarioModificacion = obj.Formulario.Usuario;
                if (!formularioSolicitud.HasErrors)
                    _repFormularioSolicitud.Insert(formularioSolicitud);
                else
                    return BadRequest(formularioSolicitud.GetErrors(null));

                List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();

                foreach (var item in obj.Campo)
                {
                    CampoFormularioBO campo = new CampoFormularioBO()
                    {
                        IdFormularioSolicitud = formularioSolicitud.Id,
                        IdCampoContacto = item.Id.Value,
                        NroVisitas = item.NroVisitas,
                        Codigo = "LPG-PB",
                        Campo = item.Nombre,
                        Siempre = item.Siempre,
                        Inteligente = item.Inteligente,
                        Probabilidad = item.Probabilidad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                };

                    camposnuevos.Add(campo);
                }
                _repCampoFormulario.Insert(camposnuevos);

                return Ok(new { data=formularioSolicitud });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarConjuntoAnuncio([FromBody] CompuestoConjuntoAnuncioDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConjuntoAnuncioRepositorio repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();
                ConjuntoAnuncioBO conjuntoAnuncio = new ConjuntoAnuncioBO();
                var cantidadCampanias = repConjuntoAnuncio.GetBy(w => w.Nombre.Equals(Obj.ConjuntoAnuncio.Nombre));
                if (cantidadCampanias.Count() > 0)
                {
                    throw new ArgumentException("Existen " + cantidadCampanias.Count() + " Capanias con este Nombre");
                }
                using (TransactionScope scope = new TransactionScope())
                {

                    conjuntoAnuncio.Nombre = Obj.ConjuntoAnuncio.Nombre;
                    conjuntoAnuncio.IdCategoriaOrigen = Obj.ConjuntoAnuncio.IdProveedor; ;
                    conjuntoAnuncio.FechaCreacionCampania = DateTime.Now;
                    conjuntoAnuncio.UsuarioCreacion = Obj.Usuario;
                    conjuntoAnuncio.UsuarioModificacion = Obj.Usuario;
                    conjuntoAnuncio.FechaCreacion = DateTime.Now;
                    conjuntoAnuncio.FechaModificacion = DateTime.Now;
                    conjuntoAnuncio.Estado = true;
                    repConjuntoAnuncio.Insert(conjuntoAnuncio);
                    scope.Complete();
                }

                return Ok(new { Id = conjuntoAnuncio.Id ,Codigo = "FS-" + conjuntoAnuncio.Nombre }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFormularioSolicitud([FromBody]InsertarFormularioSolicitudCampoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(contexto);
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(contexto);

                FormularioSolicitudBO formularioSolicitud = new FormularioSolicitudBO(obj.Formulario.Id);

                formularioSolicitud.IdFormularioRespuesta = obj.Formulario.IdFormularioRespuesta;
                formularioSolicitud.Nombre = obj.Formulario.Nombre;
                formularioSolicitud.Codigo = obj.Formulario.Codigo;
                formularioSolicitud.Campanha = obj.Formulario.NombreCampania;
                formularioSolicitud.IdConjuntoAnuncio = obj.Formulario.IdCampania;
                formularioSolicitud.Proveedor = obj.Formulario.Proveedor;
                formularioSolicitud.IdFormularioSolicitudTextoBoton = obj.Formulario.IdFormularioSolicitudTextoBoton;
                formularioSolicitud.TipoSegmento = obj.Formulario.TipoSegmento;
                formularioSolicitud.CodigoSegmento = obj.Formulario.CodigoSegmento;
                formularioSolicitud.TipoEvento = obj.Formulario.TipoEvento;
                formularioSolicitud.UrlbotonInvitacionPagina = obj.Formulario.UrlbotonInvitacionPagina;
                formularioSolicitud.FechaModificacion = DateTime.Now;
                formularioSolicitud.UsuarioModificacion = obj.Formulario.Usuario;

                if (!formularioSolicitud.HasErrors)
                    _repFormularioSolicitud.Update(formularioSolicitud);
                else
                    return BadRequest(formularioSolicitud.GetErrors(null));

                var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == formularioSolicitud.Id, w => new { w.Id }).Select(x=>x.Id);

                _repCampoFormulario.Delete(IdCampos, obj.Formulario.Usuario);

                List<CampoFormularioBO> camposnuevos = new List<CampoFormularioBO>();

                foreach (var item in obj.Campo)
                {
                    CampoFormularioBO campo = new CampoFormularioBO()
                    {
                        IdFormularioSolicitud = formularioSolicitud.Id,
                        IdCampoContacto = item.Id.Value,
                        NroVisitas = item.NroVisitas,
                        Codigo = "LPG-PB",
                        Campo = item.Nombre,
                        Siempre = item.Siempre,
                        Inteligente = item.Inteligente,
                        Probabilidad = item.Probabilidad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                    };

                    camposnuevos.Add(campo);
                }
                _repCampoFormulario.Insert(camposnuevos);
                return Ok(new { data= formularioSolicitud });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]/{IdFormulario}/{Usuario}")]
        [HttpPost]
        public ActionResult EliminarFormularioSolicitud(int IdFormulario,string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FormularioSolicitudRepositorio _repFormularioSolicitud = new FormularioSolicitudRepositorio(contexto);
                CampoFormularioRepositorio _repCampoFormulario = new CampoFormularioRepositorio(contexto);

                _repFormularioSolicitud.Delete(IdFormulario, Usuario);

                var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == IdFormulario, w => new { w.Id }).Select(x => x.Id);

                _repCampoFormulario.Delete(IdCampos, Usuario);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
    public class ValidadorFormularioSolicitudDTO : AbstractValidator<TFormularioSolicitud>
    {
        public static ValidadorFormularioSolicitudDTO Current = new ValidadorFormularioSolicitudDTO();
        public ValidadorFormularioSolicitudDTO()
        {
            RuleFor(objeto => objeto.IdFormularioRespuesta).NotEmpty().WithMessage("IdFormularioRespuesta es Obligatorio")
                                                    .NotNull().WithMessage("IdFormularioRespuesta no permite datos nulos");
        }

    }
}
