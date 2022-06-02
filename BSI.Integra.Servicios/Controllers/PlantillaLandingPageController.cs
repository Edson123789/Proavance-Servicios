using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlantillaLandingPage")]
    public class PlantillaLandingPageController : BaseController<TPlantillaLandingPage, ValidadorPlantillaLandingPageDTO>
    {
        public PlantillaLandingPageController(IIntegraRepository<TPlantillaLandingPage> repositorio, ILogger<BaseController<TPlantillaLandingPage, ValidadorPlantillaLandingPageDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPlantillasLandingPage()
        {
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();

                var plantillaLandingPage = _repPlantillaLandingPage.ObtenerTodoGrilla();

                return Ok(new { data = plantillaLandingPage, Total = plantillaLandingPage.Count() });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPlantillaLandingPage([FromBody]PlantillaLandingPageDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                PlantillaLandingPageBO plantillaNueva = new PlantillaLandingPageBO();

                plantillaNueva.Nombre = obj.Nombre;
                plantillaNueva.Cita1Texto = obj.Cita1Texto;
                plantillaNueva.Cita1Color = obj.Cita1Color;
                plantillaNueva.Cita2Texto = obj.Cita2Texto;
                plantillaNueva.Cita2Color = obj.Cita2Color;
                plantillaNueva.Cita3Texto = obj.Cita3Texto;
                plantillaNueva.Cita3Color = obj.Cita3Color;
                plantillaNueva.UrlImagenPrincipal = obj.UrlImagenPrincipal;
                plantillaNueva.PorDefecto = obj.PorDefecto.Value;
                plantillaNueva.Cita4Texto = obj.Cita4Texto;
                plantillaNueva.Cita4Color = obj.Cita4Color;
                plantillaNueva.ColorPopup = obj.ColorPopup;
                plantillaNueva.ColorTitulo = obj.ColorTitulo;
                plantillaNueva.ColorTextoBoton = obj.ColorTextoBoton;
                plantillaNueva.ColorFondoBoton = obj.ColorFondoBoton;
                plantillaNueva.ColorDescripcion = obj.ColorDescripcion;
                plantillaNueva.ColorFondoHeader = obj.ColorFondoHeader;
                plantillaNueva.Cita1Despues = obj.Cita1Despues;
                plantillaNueva.MuestraPrograma = obj.MuestraPrograma.Value;
                plantillaNueva.ColorPlaceHolder = obj.ColorPlaceHolder;
                plantillaNueva.Plantilla2 = true;
                plantillaNueva.TipoPlantilla = 2;
                plantillaNueva.Estado = true;
                plantillaNueva.FechaCreacion = DateTime.Now;
                plantillaNueva.FechaModificacion =DateTime.Now;
                plantillaNueva.UsuarioCreacion = obj.Usuario;
                plantillaNueva.UsuarioModificacion = obj.Usuario;

                if (!plantillaNueva.HasErrors)
                    _repPlantillaLandingPage.Insert(plantillaNueva);
                else
                    return BadRequest(plantillaNueva.GetErrors(null));

                return Ok(new{data= plantillaNueva });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]/{IdPlantilla}/{Usuario}")]
        [HttpPost]
        public ActionResult EliminarPlantillaLandingPage(int IdPlantilla,string Usuario)
        {
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                
                    _repPlantillaLandingPage.Delete(IdPlantilla,Usuario);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarPlantillaLandingPage([FromBody]PlantillaLandingPageDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                PlantillaLandingPageBO plantillaNueva = new PlantillaLandingPageBO(obj.Id);

                plantillaNueva.Nombre = obj.Nombre;
                plantillaNueva.Cita1Texto = obj.Cita1Texto;
                plantillaNueva.Cita1Color = obj.Cita1Color;
                plantillaNueva.Cita2Texto = obj.Cita2Texto;
                plantillaNueva.Cita2Color = obj.Cita2Color;
                plantillaNueva.Cita3Texto = obj.Cita3Texto;
                plantillaNueva.Cita3Color = obj.Cita3Color;
                plantillaNueva.Cita4Texto = obj.Cita4Texto;
                plantillaNueva.Cita4Color = obj.Cita4Color;
                plantillaNueva.UrlImagenPrincipal = obj.UrlImagenPrincipal;
                plantillaNueva.PorDefecto = obj.PorDefecto.Value;                
                plantillaNueva.ColorPopup = obj.ColorPopup;
                plantillaNueva.ColorTitulo = obj.ColorTitulo;
                plantillaNueva.ColorTextoBoton = obj.ColorTextoBoton;
                plantillaNueva.ColorFondoBoton = obj.ColorFondoBoton;
                plantillaNueva.ColorDescripcion = obj.ColorDescripcion;
                plantillaNueva.ColorFondoHeader = obj.ColorFondoHeader;
                plantillaNueva.Cita1Despues = obj.Cita1Despues;
                plantillaNueva.MuestraPrograma = obj.MuestraPrograma.Value;
                plantillaNueva.ColorPlaceHolder = obj.ColorPlaceHolder;
                plantillaNueva.FechaModificacion =DateTime.Now;
                plantillaNueva.UsuarioModificacion = obj.Usuario;

                if (!plantillaNueva.HasErrors)
                    _repPlantillaLandingPage.Update(plantillaNueva);
                else
                    return BadRequest(plantillaNueva.GetErrors(null));

                return Ok(new { data = plantillaNueva });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }



        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                var plantillaLandingPage = _repPlantillaLandingPage.ObtenerTodoGrilla().OrderByDescending(x => x.Id);

                return Ok(plantillaLandingPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlantilla()
        {
            try
            {
                ListaPlantillaRepositorio listaPlantillaRepositorio = new ListaPlantillaRepositorio();

                return Ok(listaPlantillaRepositorio.ObtenerIdNombre());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTitulos()
        {
            try
            {
                TituloRepositorio tituloRepositorio = new TituloRepositorio();

                return Ok(tituloRepositorio.ObtenerTitulosFiltro());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("[Action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerAdicionalesPorId(int IdPlantilla)
        {
            try
            {
                PlantillaLandingPagePgeneralAdicionalRepositorio plantillaAdicionalRepositorio = new PlantillaLandingPagePgeneralAdicionalRepositorio();

                return Ok(plantillaAdicionalRepositorio.ObtenerAdicionales(IdPlantilla));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarConAdicionales([FromBody]PlantillaLandingPageAdicionalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                PlantillaLandingPageBO plantillaNueva = new PlantillaLandingPageBO();
                PlantillaLandingPagePgeneralAdicionalBO plantillaAdicional;

                plantillaNueva.Nombre = Json.plantilla.Nombre;
                plantillaNueva.Cita1Texto = "";
                plantillaNueva.Cita1Color = Json.plantilla.Cita1Color;
                plantillaNueva.Cita2Texto = "";
                plantillaNueva.Cita2Color = Json.plantilla.Cita2Color;
                plantillaNueva.Cita3Texto = "";
                plantillaNueva.Cita3Color = Json.plantilla.Cita3Color;
                //plantillaNueva.PorDefecto = obj.PorDefecto.Value;
                plantillaNueva.Cita4Texto = "";
                plantillaNueva.Cita4Color = Json.plantilla.Cita4Color;
                plantillaNueva.ColorPopup = Json.plantilla.ColorPopup;
                plantillaNueva.ColorTitulo = Json.plantilla.ColorTitulo;
                plantillaNueva.ColorTextoBoton = Json.plantilla.ColorTextoBoton;
                plantillaNueva.ColorFondoBoton = Json.plantilla.ColorFondoBoton;
                plantillaNueva.ColorDescripcion = Json.plantilla.ColorDescripcion;
                plantillaNueva.ColorFondoHeader = Json.plantilla.ColorFondoHeader;
                plantillaNueva.Cita1Despues = "";
                //plantillaNueva.MuestraPrograma = obj.MuestraPrograma.Value;
                plantillaNueva.ColorPlaceHolder = Json.plantilla.ColorPlaceHolder;
                plantillaNueva.Plantilla2 = true;
                plantillaNueva.TipoPlantilla = Json.plantilla.TipoPlantilla??0;

                plantillaNueva.UrlImagenPrincipal = Json.plantilla.UrlImagenPrincipal;
                plantillaNueva.IdListaPlantilla = Json.plantilla.IdListaPlantilla;
                plantillaNueva.FormularioTituloTamanhio = Json.plantilla.FormularioTituloTamanhio;
                plantillaNueva.FormularioTituloFormato = Json.plantilla.FormularioTituloFormato;
                plantillaNueva.FormularioBotonTamanhio = Json.plantilla.FormularioBotonTamanhio;
                plantillaNueva.FormularioBotonFormato = Json.plantilla.FormularioBotonFormato;
                plantillaNueva.FormularioTextoTamanhio = Json.plantilla.FormularioTextoTamanhio;
                plantillaNueva.FormularioTextoFormato = Json.plantilla.FormularioTextoFormato;
                plantillaNueva.TituloTituloTamanhio = Json.plantilla.TituloTituloTamanhio;
                plantillaNueva.TituloTituloFormato = Json.plantilla.TituloTituloFormato;
                plantillaNueva.TituloTextoTamanhio = Json.plantilla.TituloTextoTamanhio;
                plantillaNueva.TituloTextoFormato = Json.plantilla.TituloTextoFormato;
                plantillaNueva.TextoTituloTamanhio = Json.plantilla.TextoTituloTamanhio;
                plantillaNueva.TextoTituloFormato = Json.plantilla.TextoTituloFormato;
                plantillaNueva.TextoTextoTamanhio = Json.plantilla.TextoTextoTamanhio;
                plantillaNueva.TextoTextoFormato = Json.plantilla.TextoTextoFormato;
                plantillaNueva.FormularioBotonPosicion = Json.plantilla.FormularioBotonPosicion;

                plantillaNueva.Estado = true;
                plantillaNueva.FechaCreacion = DateTime.Now;
                plantillaNueva.FechaModificacion = DateTime.Now;
                plantillaNueva.UsuarioCreacion = Json.Usuario;
                plantillaNueva.UsuarioModificacion = Json.Usuario;

                plantillaNueva.PGeneralAdicional = new  List<PlantillaLandingPagePgeneralAdicionalBO>();
                if(Json.listaAdicionales != null)
                {
                    foreach(var adicional in Json.listaAdicionales)
                    {
                        plantillaAdicional = new PlantillaLandingPagePgeneralAdicionalBO();
                        plantillaAdicional.IdTitulo = adicional.IdTitulo;
                        plantillaAdicional.NombreTitulo = adicional.NombreTitulo;
                        plantillaAdicional.IdAdicionalProgramaGeneral = adicional.IdAdicionalProgramaGeneral;
                        plantillaAdicional.ColorTitulo = adicional.ColorTitulo;
                        plantillaAdicional.ColorDescripcion = adicional.ColorDescripcion;

                        plantillaAdicional.Estado = true;
                        plantillaAdicional.FechaCreacion = DateTime.Now;
                        plantillaAdicional.FechaModificacion = DateTime.Now;
                        plantillaAdicional.UsuarioCreacion = Json.Usuario;
                        plantillaAdicional.UsuarioModificacion = Json.Usuario;

                        plantillaNueva.PGeneralAdicional.Add(plantillaAdicional);
                    }
                }

                if (!plantillaNueva.HasErrors)
                    _repPlantillaLandingPage.Insert(plantillaNueva);
                else
                    return BadRequest(plantillaNueva.GetErrors(null));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarConAdicionales([FromBody]PlantillaLandingPageAdicionalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext context = new integraDBContext();
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio(context);
                PlantillaLandingPagePgeneralAdicionalRepositorio _repPlantillaLandingPageAdicional = new PlantillaLandingPagePgeneralAdicionalRepositorio(context);

                PlantillaLandingPageBO plantilla = _repPlantillaLandingPage.FirstById(Json.plantilla.Id);
                PlantillaLandingPagePgeneralAdicionalBO plantillaAdicional;

                plantilla.Nombre = Json.plantilla.Nombre;
                plantilla.Cita1Texto = "";
                plantilla.Cita1Color = Json.plantilla.Cita1Color;
                plantilla.Cita2Texto = "";
                plantilla.Cita2Color = Json.plantilla.Cita2Color;
                plantilla.Cita3Texto = "";
                plantilla.Cita3Color = Json.plantilla.Cita3Color;
                //plantillaNueva.PorDefecto = obj.PorDefecto.Value;
                plantilla.Cita4Texto = "";
                plantilla.Cita4Color = Json.plantilla.Cita4Color;
                plantilla.ColorPopup = Json.plantilla.ColorPopup;
                plantilla.ColorTitulo = Json.plantilla.ColorTitulo;
                plantilla.ColorTextoBoton = Json.plantilla.ColorTextoBoton;
                plantilla.ColorFondoBoton = Json.plantilla.ColorFondoBoton;
                plantilla.ColorDescripcion = Json.plantilla.ColorDescripcion;
                plantilla.ColorFondoHeader = Json.plantilla.ColorFondoHeader;
                plantilla.Cita1Despues = "";
                //plantillaNueva.MuestraPrograma = obj.MuestraPrograma.Value;
                plantilla.ColorPlaceHolder = Json.plantilla.ColorPlaceHolder;
                plantilla.Plantilla2 = true;
                plantilla.TipoPlantilla = Json.plantilla.TipoPlantilla ?? 0;

                plantilla.UrlImagenPrincipal = Json.plantilla.UrlImagenPrincipal;
                plantilla.IdListaPlantilla = Json.plantilla.IdListaPlantilla;
                plantilla.FormularioTituloTamanhio = Json.plantilla.FormularioTituloTamanhio;
                plantilla.FormularioTituloFormato = Json.plantilla.FormularioTituloFormato;
                plantilla.FormularioBotonTamanhio = Json.plantilla.FormularioBotonTamanhio;
                plantilla.FormularioBotonFormato = Json.plantilla.FormularioBotonFormato;
                plantilla.FormularioTextoTamanhio = Json.plantilla.FormularioTextoTamanhio;
                plantilla.FormularioTextoFormato = Json.plantilla.FormularioTextoFormato;
                plantilla.TituloTituloTamanhio = Json.plantilla.TituloTituloTamanhio;
                plantilla.TituloTituloFormato = Json.plantilla.TituloTituloFormato;
                plantilla.TituloTextoTamanhio = Json.plantilla.TituloTextoTamanhio;
                plantilla.TituloTextoFormato = Json.plantilla.TituloTextoFormato;
                plantilla.TextoTituloTamanhio = Json.plantilla.TextoTituloTamanhio;
                plantilla.TextoTituloFormato = Json.plantilla.TextoTituloFormato;
                plantilla.TextoTextoTamanhio = Json.plantilla.TextoTextoTamanhio;
                plantilla.TextoTextoFormato = Json.plantilla.TextoTextoFormato;
                plantilla.FormularioBotonPosicion = Json.plantilla.FormularioBotonPosicion;

                plantilla.Estado = true;
                plantilla.FechaModificacion = DateTime.Now;
                plantilla.UsuarioModificacion = Json.Usuario;

                plantilla.PGeneralAdicional = new List<PlantillaLandingPagePgeneralAdicionalBO>();
                if (Json.listaAdicionales != null)
                {
                    foreach (var adicional in Json.listaAdicionales)
                    {
                        plantillaAdicional = _repPlantillaLandingPageAdicional.FirstById(adicional.Id);
                        plantillaAdicional.IdTitulo = adicional.IdTitulo;
                        plantillaAdicional.NombreTitulo = adicional.NombreTitulo;
                        plantillaAdicional.IdAdicionalProgramaGeneral = adicional.IdAdicionalProgramaGeneral;
                        plantillaAdicional.ColorTitulo = adicional.ColorTitulo;
                        plantillaAdicional.ColorDescripcion = adicional.ColorDescripcion;

                        plantillaAdicional.Estado = true;
                        plantillaAdicional.FechaModificacion = DateTime.Now;
                        plantillaAdicional.UsuarioModificacion = Json.Usuario;

                        plantilla.PGeneralAdicional.Add(plantillaAdicional);
                    }
                }

                if (!plantilla.HasErrors)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repPlantillaLandingPageAdicional.DeleteLogicoPorPrograma(plantilla.Id, Json.Usuario, Json.listaAdicionales);
                        _repPlantillaLandingPage.Update(plantilla);
                        scope.Complete();
                    }
                }

                else
                    return BadRequest(plantilla.GetErrors(null));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody]PlantillaLandingPageEliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaLandingPageRepositorio _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();
                return Ok(_repPlantillaLandingPage.Delete(Json.Id, Json.Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerVistaPreviaPlantilla([FromBody]PlantillaLandingPageAdicionalDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaLandingPageBO plantillaLandingPageBO = new PlantillaLandingPageBO();
                string vistaPrevia = plantillaLandingPageBO.ObtenerVistaPreviaPlantilla2(Json.plantilla, Json.listaAdicionales);
                return Ok(vistaPrevia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
    public class ValidadorPlantillaLandingPageDTO : AbstractValidator<TPlantillaLandingPage>
    {
        public static ValidadorPlantillaLandingPageDTO Current = new ValidadorPlantillaLandingPageDTO();
        public ValidadorPlantillaLandingPageDTO()
        {
            //RuleFor(objeto => objeto.IdFormularioRespuesta).NotEmpty().WithMessage("IdFormularioRespuesta es Obligatorio")
            //                                        .NotNull().WithMessage("IdFormularioRespuesta no permite datos nulos");
        }

    }
}
