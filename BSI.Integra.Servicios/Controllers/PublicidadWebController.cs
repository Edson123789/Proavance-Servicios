    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PublicidadWeb")]
    public class PublicidadWebController : BaseController<PublicidadWebBO, ValidadorPublicidadWebDTO>
    {
        public PublicidadWebController(IIntegraRepository<PublicidadWebBO> repositorio, ILogger<BaseController<PublicidadWebBO, ValidadorPublicidadWebDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerPublicidadWebPanel()
        {
            try
            {
                PublicidadWebRepositorio _repTiposPagos = new PublicidadWebRepositorio();
                var listaTiposPagos = _repTiposPagos.ListarPublicidadWeb();

                return Ok(listaTiposPagos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosPublicidadWeb()
        {
            try
            {
                ChatZopimRepositorio repChatZoopim = new ChatZopimRepositorio();
                CategoriaOrigenRepositorio repCategoria = new CategoriaOrigenRepositorio();
                FormularioSolicitudTextoBotonRepositorio repTextoBotones = new FormularioSolicitudTextoBotonRepositorio();
                CampoContactoRepositorio repCampoContacto = new CampoContactoRepositorio();
                PgeneralRepositorio repPgeneral = new PgeneralRepositorio();
                TipoPublicidadWebRepositorio repTipoPublicidad = new TipoPublicidadWebRepositorio();
                PespecificoRepositorio repPespecifico = new PespecificoRepositorio();

                CombosPublicidadWebDTO combos = new CombosPublicidadWebDTO();

                combos.ChatZoopim= repChatZoopim.ObtenerChatZopinFiltro();
                combos.CategoriaOrigen = repCategoria.ObtenerCategoriaFiltro();
                combos.TextoBotones = repTextoBotones.ObtenerTextoBotonFiltro();
                combos.CampoContactos = repCampoContacto.ObtenerCamposContactoFiltro();
                combos.ProgramasGenerales = repPgeneral.ObtenerProgramasFiltro();
                combos.TipoPublicidad = repTipoPublicidad.ObtenerCamposContactoFiltro();
                combos.CentroCosto = repPespecifico.ObtenerCentroCostoPorPespecifico();
                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]/{IdConjuntoAnuncio}")]
        [HttpGet]
        public ActionResult ObtenerCodigo(int IdConjuntoAnuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FormularioSolicitudRepositorio _repFormulario = new FormularioSolicitudRepositorio();
                ConjuntoAnuncioRepositorio _repConjuntoAnuncio = new ConjuntoAnuncioRepositorio();

                var conjuntoAnuncio = _repConjuntoAnuncio.FirstById(IdConjuntoAnuncio);

                int cantidad = _repFormulario.GetBy(x => x.Codigo.Contains(conjuntoAnuncio.Nombre)).Count();

                string result = "FS-";
                if (cantidad >= 1) result = result + conjuntoAnuncio.Nombre + cantidad;
                else result = result + conjuntoAnuncio.Nombre;

                return Ok(new {Nombre = result});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{IdPublidadWeb}")]
        [HttpGet]
        public IActionResult ObtenerDetallesPublicidadWeb(int IdPublidadWeb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PublicidadWebRepositorio repPublicidadWeb = new PublicidadWebRepositorio();
                PublicidadWebFormularioRepositorio repPublicidadFormulario = new PublicidadWebFormularioRepositorio();

                PublicidadWebProgramaRepositorio repPublicidadWebPrograma = new PublicidadWebProgramaRepositorio();
                PublicidadWebFormularioCampoRepositorio repCampoFormulario = new PublicidadWebFormularioCampoRepositorio();

                DetallePublicidadWebDTO detalle = new DetallePublicidadWebDTO();

                detalle.PublicidadProgramas = repPublicidadWebPrograma.ObtenerProgramasPorPublicidad(IdPublidadWeb);
                detalle.Formularios = repPublicidadFormulario.ObtenerFormulariosPorPublicidad(IdPublidadWeb);

                detalle.FormularioCampos = repCampoFormulario.ObtenerCamposFormularioPorPublicidadFormulario(detalle.Formularios.Id);

                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPEspecifico([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PespecificoRepositorio repPespecifico = new PespecificoRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    var lista = repPespecifico.ObtenerPEspecificoPorCentroCosto(Filtros["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCampaniaNoAsociadas([FromBody] Dictionary<string, string> Filtros)
       {
            try
            {
                PublicidadWebRepositorio repPublicidadWeb = new PublicidadWebRepositorio();
                if (Filtros.Count() > 0 && (Filtros != null && Filtros["Valor"] != null))
                {
                    var lista = repPublicidadWeb.ObtenerCampaniasNoAsociadas(Filtros["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarPublicidadWeb([FromBody] CompuestoPublicidadWebDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PublicidadWebRepositorio repPublicidadWeb = new PublicidadWebRepositorio(contexto);
                PublicidadWebFormularioRepositorio repPublicidadFormulario = new PublicidadWebFormularioRepositorio(contexto);
                PublicidadWebProgramaRepositorio repPublicidadWebPrograma = new PublicidadWebProgramaRepositorio(contexto);
                PublicidadWebFormularioCampoRepositorio repCampoFormulario = new PublicidadWebFormularioCampoRepositorio(contexto);

                PublicidadWebBO publicidadWeb = new PublicidadWebBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    publicidadWeb.IdTipoPublicidadWeb = Json.PublicidadWeb.IdTipoPublicidadWeb;
                    publicidadWeb.IdConjuntoAnuncio = Json.PublicidadWeb.IdConjuntoAnuncio.Value;
                    publicidadWeb.IdCategoriaOrigen = Json.PublicidadWeb.IdCategoriaOrigen.Value;
                    publicidadWeb.Nombre = Json.PublicidadWeb.Nombre;
                    publicidadWeb.Codigo = Json.PublicidadWeb.Codigo;
                    publicidadWeb.Popup = Json.PublicidadWeb.Popup;
                    publicidadWeb.Titulo = Json.PublicidadWeb.Titulo;
                    publicidadWeb.Descripcion = Json.PublicidadWeb.Descripcion;
                    publicidadWeb.Tiempo = Json.PublicidadWeb.Tiempo;
                    publicidadWeb.IdChatZoopim = Json.PublicidadWeb.IdChatZoopim.Value;
                    publicidadWeb.IdPespecifico = Json.PublicidadWeb.IdPespecifico.Value;
                    publicidadWeb.UrlImagen = Json.PublicidadWeb.UrlImagen;
                    publicidadWeb.UrlBrochure = Json.PublicidadWeb.UrlBrochure;
                    publicidadWeb.UrlVideo = Json.PublicidadWeb.UrlVideo;
                    publicidadWeb.EsRegistroAdicional = Json.PublicidadWeb.EsRegistroAdicional;
                    publicidadWeb.UsuarioCreacion = Json.Usuario;
                    publicidadWeb.UsuarioModificacion = Json.Usuario;
                    publicidadWeb.FechaCreacion = DateTime.Now;
                    publicidadWeb.FechaModificacion = DateTime.Now;
                    publicidadWeb.Estado = true;


                    publicidadWeb.PublicidadWebPrograma = new List<PublicidadWebProgramaBO>();

                    foreach (var item in Json.PublicidadProgramas)
                    {
                        PublicidadWebProgramaBO publicidadWebPrograma = new PublicidadWebProgramaBO();
                        publicidadWebPrograma.IdPgeneral = item.IdPgeneral;
                        publicidadWebPrograma.Nombre = item.Nombre;
                        publicidadWebPrograma.OrdenPrograma = item.OrdenPrograma;
                        publicidadWebPrograma.ModificarInformacion = item.ModificarInformacion;
                        publicidadWebPrograma.Duracion = item.Duracion;
                        publicidadWebPrograma.Inicios = item.Inicios;
                        publicidadWebPrograma.Precios = item.Precios;
                        publicidadWebPrograma.UsuarioCreacion = Json.Usuario;
                        publicidadWebPrograma.UsuarioModificacion = Json.Usuario;
                        publicidadWebPrograma.FechaCreacion = DateTime.Now;
                        publicidadWebPrograma.FechaModificacion = DateTime.Now;
                        publicidadWebPrograma.Estado = true;

                        publicidadWeb.PublicidadWebPrograma.Add(publicidadWebPrograma);
                    }

                    PublicidadWebFormularioBO publicidadWebFormulario = new PublicidadWebFormularioBO();
                    publicidadWebFormulario.IdFormularioSolicitudTextoBoton = Json.Formulario.IdFormularioSolicitudTextoBoton;
                    publicidadWebFormulario.Nombre = Json.Formulario.Titulo;
                    publicidadWebFormulario.Titulo = Json.Formulario.Titulo;
                    publicidadWebFormulario.Descripcion = Json.Formulario.Descripcion;
                    publicidadWebFormulario.TextoBoton = Json.Formulario.TextoBoton;
                    publicidadWebFormulario.UsuarioCreacion = Json.Usuario;
                    publicidadWebFormulario.UsuarioModificacion = Json.Usuario;
                    publicidadWebFormulario.FechaCreacion = DateTime.Now;
                    publicidadWebFormulario.FechaModificacion = DateTime.Now;
                    publicidadWebFormulario.Estado = true;

                    publicidadWeb.PublicidadWebFormulario = publicidadWebFormulario;
                    publicidadWeb.PublicidadWebFormulario.PublicidadWebFormularioCampo = new List<PublicidadWebFormularioCampoBO>();
                    foreach (var item in Json.FormularioCampos)
                    {
                        PublicidadWebFormularioCampoBO publicidadWebFormularioCampo = new PublicidadWebFormularioCampoBO();
                        publicidadWebFormularioCampo.IdCampoContacto = item.IdCampoContacto;
                        publicidadWebFormularioCampo.Nombre = item.Nombre;
                        publicidadWebFormularioCampo.Siempre = item.Siempre;
                        publicidadWebFormularioCampo.Inteligente = item.Inteligente;
                        publicidadWebFormularioCampo.Probabilidad = item.Probabilidad;
                        publicidadWebFormularioCampo.Orden = item.Orden;
                        publicidadWebFormularioCampo.UsuarioCreacion = Json.Usuario;
                        publicidadWebFormularioCampo.UsuarioModificacion = Json.Usuario;
                        publicidadWebFormularioCampo.FechaCreacion = DateTime.Now;
                        publicidadWebFormularioCampo.FechaModificacion = DateTime.Now;
                        publicidadWebFormularioCampo.Estado = true;

                        publicidadWeb.PublicidadWebFormulario.PublicidadWebFormularioCampo.Add(publicidadWebFormularioCampo);
                    }
                    repPublicidadWeb.Insert(publicidadWeb);
                    repPublicidadWeb.InsertarPublicidadPortalWeb(publicidadWeb.Id);
                    scope.Complete();
                }

                return Ok(publicidadWeb);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPublicidadWeb([FromBody] CompuestoPublicidadWebDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PublicidadWebRepositorio repPublicidadWeb = new PublicidadWebRepositorio(contexto);
                PublicidadWebFormularioRepositorio repPublicidadFormulario = new PublicidadWebFormularioRepositorio(contexto);
                PublicidadWebProgramaRepositorio repPublicidadWebPrograma = new PublicidadWebProgramaRepositorio(contexto);
                PublicidadWebFormularioCampoRepositorio repCampoFormulario = new PublicidadWebFormularioCampoRepositorio(contexto);

                PublicidadWebBO publicidadWeb = new PublicidadWebBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    //repPublicidadFormulario.EliminacionLogicoPorPublicidadWeb(Json.IdPublicidad, Json.Usuario, Json.Formularios);
                    repPublicidadWebPrograma.EliminacionLogicoPorPublicidadWeb(Json.IdPublicidad, Json.Usuario, Json.PublicidadProgramas);
                    repCampoFormulario.EliminacionLogicoPorPublicidadFormulario(Json.Formulario.Id, Json.Usuario, Json.FormularioCampos);

                    publicidadWeb = repPublicidadWeb.FirstById(Json.PublicidadWeb.Id);

                    publicidadWeb.IdTipoPublicidadWeb = Json.PublicidadWeb.IdTipoPublicidadWeb;
                    publicidadWeb.IdConjuntoAnuncio = Json.PublicidadWeb.IdConjuntoAnuncio.Value;
                    publicidadWeb.IdCategoriaOrigen = Json.PublicidadWeb.IdCategoriaOrigen.Value;
                    publicidadWeb.Nombre = Json.PublicidadWeb.Nombre;
                    publicidadWeb.Codigo = Json.PublicidadWeb.Codigo;
                    publicidadWeb.Popup = Json.PublicidadWeb.Popup;
                    publicidadWeb.Titulo = Json.PublicidadWeb.Titulo;
                    publicidadWeb.Descripcion = Json.PublicidadWeb.Descripcion;
                    publicidadWeb.Tiempo = Json.PublicidadWeb.Tiempo;
                    publicidadWeb.IdChatZoopim = Json.PublicidadWeb.IdChatZoopim.Value;
                    publicidadWeb.IdPespecifico = Json.PublicidadWeb.IdPespecifico.Value;
                    publicidadWeb.UrlImagen = Json.PublicidadWeb.UrlImagen;
                    publicidadWeb.UrlBrochure = Json.PublicidadWeb.UrlBrochure;
                    publicidadWeb.UrlVideo = Json.PublicidadWeb.UrlVideo;
                    publicidadWeb.EsRegistroAdicional = Json.PublicidadWeb.EsRegistroAdicional;
                    publicidadWeb.UsuarioModificacion = Json.Usuario;
                    publicidadWeb.FechaModificacion = DateTime.Now;

                    publicidadWeb.PublicidadWebPrograma = new List<PublicidadWebProgramaBO>();

                    foreach (var item in Json.PublicidadProgramas)
                    {
                        PublicidadWebProgramaBO publicidadWebPrograma;
                        if (repPublicidadWebPrograma.Exist(x => x.IdPgeneral == item.IdPgeneral && x.IdPublicidadWeb == Json.IdPublicidad))
                        {
                            publicidadWebPrograma = repPublicidadWebPrograma.FirstBy(x => x.IdPgeneral == item.IdPgeneral && x.IdPublicidadWeb == Json.IdPublicidad);
                            publicidadWebPrograma.IdPgeneral = item.IdPgeneral;
                            publicidadWebPrograma.Nombre = item.Nombre;
                            publicidadWebPrograma.OrdenPrograma = item.OrdenPrograma;
                            publicidadWebPrograma.ModificarInformacion = item.ModificarInformacion;
                            publicidadWebPrograma.Duracion = item.Duracion;
                            publicidadWebPrograma.Inicios = item.Inicios;
                            publicidadWebPrograma.Precios = item.Precios;
                            publicidadWebPrograma.UsuarioModificacion = Json.Usuario;
                            publicidadWebPrograma.FechaModificacion = DateTime.Now;
                        }
                            else
                        {
                            publicidadWebPrograma = new PublicidadWebProgramaBO();
                            publicidadWebPrograma.IdPgeneral = item.IdPgeneral;
                            publicidadWebPrograma.Nombre = item.Nombre;
                            publicidadWebPrograma.OrdenPrograma = item.OrdenPrograma;
                            publicidadWebPrograma.ModificarInformacion = item.ModificarInformacion;
                            publicidadWebPrograma.Duracion = item.Duracion;
                            publicidadWebPrograma.Inicios = item.Inicios;
                            publicidadWebPrograma.Precios = item.Precios;
                            publicidadWebPrograma.UsuarioCreacion = Json.Usuario;
                            publicidadWebPrograma.UsuarioModificacion = Json.Usuario;
                            publicidadWebPrograma.FechaCreacion = DateTime.Now;
                            publicidadWebPrograma.FechaModificacion = DateTime.Now;
                            publicidadWebPrograma.Estado = true;
                        }
                        publicidadWeb.PublicidadWebPrograma.Add(publicidadWebPrograma);
                    }

                    int IdFormulario = 0;
                    PublicidadWebFormularioBO publicidadWebFormulario;
                    if (repPublicidadFormulario.Exist(x =>  x.IdPublicidadWeb == Json.IdPublicidad))
                    {
                        publicidadWebFormulario = repPublicidadFormulario.FirstBy(x => x.IdPublicidadWeb == Json.IdPublicidad);
                        publicidadWebFormulario.IdFormularioSolicitudTextoBoton = Json.Formulario.IdFormularioSolicitudTextoBoton;
                        publicidadWebFormulario.Nombre = Json.Formulario.Titulo;
                        publicidadWebFormulario.Titulo = Json.Formulario.Titulo;
                        publicidadWebFormulario.Descripcion = Json.Formulario.Descripcion;
                        publicidadWebFormulario.TextoBoton = Json.Formulario.TextoBoton;
                        publicidadWebFormulario.UsuarioModificacion = Json.Usuario;
                        publicidadWebFormulario.FechaModificacion = DateTime.Now;
                        IdFormulario = publicidadWebFormulario.Id;

                        publicidadWebFormulario.PublicidadWebFormularioCampo = new List<PublicidadWebFormularioCampoBO>();
                        foreach (var item in Json.FormularioCampos)
                        {
                            PublicidadWebFormularioCampoBO publicidadWebFormularioCampo;
                            if (repCampoFormulario.Exist(x => x.IdCampoContacto == item.IdCampoContacto && x.IdPublicidadWebFormulario == IdFormulario))
                            {
                                publicidadWebFormularioCampo = repCampoFormulario.FirstBy(x => x.IdCampoContacto == item.IdCampoContacto && x.IdPublicidadWebFormulario == IdFormulario);
                                publicidadWebFormularioCampo.IdCampoContacto = item.IdCampoContacto;
                                publicidadWebFormularioCampo.Nombre = item.Nombre;
                                publicidadWebFormularioCampo.Siempre = item.Siempre;
                                publicidadWebFormularioCampo.Inteligente = item.Inteligente;
                                publicidadWebFormularioCampo.Probabilidad = item.Probabilidad;
                                publicidadWebFormularioCampo.Orden = item.Orden;
                                publicidadWebFormularioCampo.UsuarioModificacion = Json.Usuario;
                                publicidadWebFormularioCampo.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                publicidadWebFormularioCampo = new PublicidadWebFormularioCampoBO();
                                publicidadWebFormularioCampo.IdCampoContacto = item.IdCampoContacto;
                                publicidadWebFormularioCampo.Nombre = item.Nombre;
                                publicidadWebFormularioCampo.Siempre = item.Siempre;
                                publicidadWebFormularioCampo.Inteligente = item.Inteligente;
                                publicidadWebFormularioCampo.Probabilidad = item.Probabilidad;
                                publicidadWebFormularioCampo.Orden = item.Orden;
                                publicidadWebFormularioCampo.UsuarioCreacion = Json.Usuario;
                                publicidadWebFormularioCampo.UsuarioModificacion = Json.Usuario;
                                publicidadWebFormularioCampo.FechaCreacion = DateTime.Now;
                                publicidadWebFormularioCampo.FechaModificacion = DateTime.Now;
                                publicidadWebFormularioCampo.Estado = true;
                            }
                            publicidadWebFormulario.PublicidadWebFormularioCampo.Add(publicidadWebFormularioCampo);
                        }
                        publicidadWeb.PublicidadWebFormulario = publicidadWebFormulario;

                    }
                    repPublicidadWeb.Update(publicidadWeb);
                    repPublicidadWeb.ActualizarPublicidadPortalWeb(publicidadWeb.Id, Json.Usuario);
                    scope.Complete();
                }

                return Ok(publicidadWeb);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdPublicidadWeb}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarPublicidadWeb(int IdPublicidadWeb, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                PublicidadWebRepositorio repPublicidadWeb = new PublicidadWebRepositorio(contexto);
                PublicidadWebFormularioRepositorio repPublicidadFormulario = new PublicidadWebFormularioRepositorio(contexto);
                PublicidadWebProgramaRepositorio repPublicidadWebPrograma = new PublicidadWebProgramaRepositorio(contexto);
                PublicidadWebFormularioCampoRepositorio repCampoFormulario = new PublicidadWebFormularioCampoRepositorio(contexto);

               
                using (TransactionScope scope = new TransactionScope())
                {
                    if (repPublicidadWeb.Exist(IdPublicidadWeb))
                    {
                        repPublicidadWeb.Delete(IdPublicidadWeb, Usuario);
                       
                        var hijosProgramas = repPublicidadWebPrograma.GetBy(x => x.IdPublicidadWeb == IdPublicidadWeb);
                        foreach (var hijo in hijosProgramas)
                        {
                            repPublicidadWebPrograma.Delete(hijo.Id, Usuario);
                        }
                        var hijosFormulario = repPublicidadFormulario.FirstBy(x => x.IdPublicidadWeb == IdPublicidadWeb);
                        repPublicidadWeb.Delete(hijosFormulario.Id, Usuario);
                        
                        var hijosCampos = repCampoFormulario.GetBy(x => x.IdPublicidadWebFormulario == hijosFormulario.Id);
                        foreach (var hijo in hijosCampos)
                        {
                            repCampoFormulario.Delete(hijo.Id, Usuario);
                        }
                        repPublicidadWeb.EliminarPublicidadPortalWeb(IdPublicidadWeb, Usuario);
                    }

                    scope.Complete();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class ValidadorPublicidadWebDTO : AbstractValidator<PublicidadWebBO>
    {
        public static ValidadorPublicidadWebDTO Current = new ValidadorPublicidadWebDTO();
        public ValidadorPublicidadWebDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
        }
    }
}
