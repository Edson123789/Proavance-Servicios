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
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormularioRespuesta")]
    public class FormularioRespuestaController : BaseController<TFormularioRespuesta, ValidadorFormularioRespuestaDTO>
    {
        public FormularioRespuestaController(IIntegraRepository<TFormularioRespuesta> repositorio, ILogger<BaseController<TFormularioRespuesta, ValidadorFormularioRespuestaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }


		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarFormularioRespuesta([FromBody] CompuestoFormularioRespuestaDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				byte[] dataContenido = Convert.FromBase64String(Json.FormularioRespuesta.ContenidoSeccionTelefonos);
				var decodedContenido = Encoding.UTF8.GetString(dataContenido);
				byte[] dataResumenPrograma = Convert.FromBase64String(Json.FormularioRespuesta.ResumenProgramaGeneral);
				var decodedResumen = Encoding.UTF8.GetString(dataResumenPrograma);

				FormularioRespuestaRepositorio repFormularioRespuesta = new FormularioRespuestaRepositorio();
				FormularioRespuestaBO formularioRespuesta = new FormularioRespuestaBO();
				using (TransactionScope scope = new TransactionScope())
				{
					formularioRespuesta.Nombre = Json.FormularioRespuesta.Nombre;
					formularioRespuesta.Codigo = Json.FormularioRespuesta.Codigo;
					formularioRespuesta.IdPgeneral = Json.FormularioRespuesta.IdPgeneral;
					formularioRespuesta.ProgramaGeneral = Json.FormularioRespuesta.ProgramaGeneral;
					formularioRespuesta.ResumenProgramaGeneral = decodedResumen;
					formularioRespuesta.ColorTextoPgeneral = Json.FormularioRespuesta.ColorTextoPgeneral;
					formularioRespuesta.ColorTextoDescripcionPgeneral = Json.FormularioRespuesta.ColorTextoDescripcionPgeneral;
					formularioRespuesta.ColorTextoInvitacionBrochure = Json.FormularioRespuesta.ColorTextoInvitacionBrochure;
					formularioRespuesta.TextoBotonBrochure = Json.FormularioRespuesta.TextoBotonBrochure;
					formularioRespuesta.ColorFondoBotonBrochure = Json.FormularioRespuesta.ColorFondoBotonBrochure;
					formularioRespuesta.ColorTextoBotonBrochure = Json.FormularioRespuesta.ColorTextoBotonBrochure;
					formularioRespuesta.ColorBordeInferiorBotonBrochure = Json.FormularioRespuesta.ColorBordeInferiorBotonBrochure;
					formularioRespuesta.ColorTextoBotonInvitacion = Json.FormularioRespuesta.ColorTextoBotonInvitacion;
					formularioRespuesta.ColorFondoBotonInvitacion = Json.FormularioRespuesta.ColorFondoBotonInvitacion;
					formularioRespuesta.FondoBotonLadoInvitacion = Json.FormularioRespuesta.FondoBotonLadoInvitacion;
					formularioRespuesta.UrlImagenLadoInvitacion = Json.FormularioRespuesta.UrlImagenLadoInvitacion;
					formularioRespuesta.TextoBotonInvitacionPagina = Json.FormularioRespuesta.TextoBotonInvitacionPagina;
					formularioRespuesta.UrlBotonInvitacionPagina = Json.FormularioRespuesta.UrlBotonInvitacionPagina;
					formularioRespuesta.TextoBotonInvitacionArea = Json.FormularioRespuesta.TextoBotonInvitacionArea;
					formularioRespuesta.UrlBotonInvitacionArea = Json.FormularioRespuesta.UrlBotonInvitacionArea;
					formularioRespuesta.ContenidoSeccionTelefonos = decodedContenido;
					formularioRespuesta.IdFormularioRespuestaPlantilla = Json.FormularioRespuesta.IdFormularioRespuestaPlantilla;
					formularioRespuesta.Urlbrochure = Json.FormularioRespuesta.Urlbrochure;
					formularioRespuesta.Urllogotipo = Json.FormularioRespuesta.Urllogotipo;
					formularioRespuesta.TextoInvitacionBrochure = Json.FormularioRespuesta.TextoInvitacionBrochure;
					formularioRespuesta.Estado = true;
					formularioRespuesta.UsuarioCreacion = Json.Usuario;
					formularioRespuesta.UsuarioModificacion = Json.Usuario;
					formularioRespuesta.FechaCreacion = DateTime.Now;
					formularioRespuesta.FechaModificacion = DateTime.Now;
					repFormularioRespuesta.Insert(formularioRespuesta);
					scope.Complete();
				}
				Json.FormularioRespuesta.Id = formularioRespuesta.Id;

				return Ok(Json.FormularioRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarFormularioRespuesta([FromBody] CompuestoFormularioRespuestaDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				byte[] dataContenido = Convert.FromBase64String(Json.FormularioRespuesta.ContenidoSeccionTelefonos);
				var decodedContenido = Encoding.UTF8.GetString(dataContenido);

				byte[] dataResumenPrograma = Convert.FromBase64String(Json.FormularioRespuesta.ResumenProgramaGeneral);
				var decodedResumen = Encoding.UTF8.GetString(dataResumenPrograma);


				FormularioRespuestaRepositorio repFormularioRespuesta = new FormularioRespuestaRepositorio();
				FormularioRespuestaBO formularioRespuesta = new FormularioRespuestaBO();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repFormularioRespuesta.Exist(Json.FormularioRespuesta.Id))
					{
						formularioRespuesta = repFormularioRespuesta.FirstById(Json.FormularioRespuesta.Id);
						formularioRespuesta.Nombre = Json.FormularioRespuesta.Nombre;
						formularioRespuesta.Codigo = Json.FormularioRespuesta.Codigo;
						formularioRespuesta.IdPgeneral = Json.FormularioRespuesta.IdPgeneral;
						formularioRespuesta.ProgramaGeneral = Json.FormularioRespuesta.ProgramaGeneral;
						formularioRespuesta.ResumenProgramaGeneral = decodedResumen;
						formularioRespuesta.ColorTextoPgeneral = Json.FormularioRespuesta.ColorTextoPgeneral;
						formularioRespuesta.ColorTextoDescripcionPgeneral = Json.FormularioRespuesta.ColorTextoDescripcionPgeneral;
						formularioRespuesta.ColorTextoInvitacionBrochure = Json.FormularioRespuesta.ColorTextoInvitacionBrochure;
						formularioRespuesta.TextoBotonBrochure = Json.FormularioRespuesta.TextoBotonBrochure;
						formularioRespuesta.ColorFondoBotonBrochure = Json.FormularioRespuesta.ColorFondoBotonBrochure;
						formularioRespuesta.ColorTextoBotonBrochure = Json.FormularioRespuesta.ColorTextoBotonBrochure;
						formularioRespuesta.ColorBordeInferiorBotonBrochure = Json.FormularioRespuesta.ColorBordeInferiorBotonBrochure;
						formularioRespuesta.ColorTextoBotonInvitacion = Json.FormularioRespuesta.ColorTextoBotonInvitacion;
						formularioRespuesta.ColorFondoBotonInvitacion = Json.FormularioRespuesta.ColorFondoBotonInvitacion;
						formularioRespuesta.FondoBotonLadoInvitacion = Json.FormularioRespuesta.FondoBotonLadoInvitacion;
						formularioRespuesta.UrlImagenLadoInvitacion = Json.FormularioRespuesta.UrlImagenLadoInvitacion;
						formularioRespuesta.TextoBotonInvitacionPagina = Json.FormularioRespuesta.TextoBotonInvitacionPagina;
						formularioRespuesta.UrlBotonInvitacionPagina = Json.FormularioRespuesta.UrlBotonInvitacionPagina;
						formularioRespuesta.TextoBotonInvitacionArea = Json.FormularioRespuesta.TextoBotonInvitacionArea;
						formularioRespuesta.UrlBotonInvitacionArea = Json.FormularioRespuesta.UrlBotonInvitacionArea;
						formularioRespuesta.ContenidoSeccionTelefonos = decodedContenido;
						formularioRespuesta.IdFormularioRespuestaPlantilla = Json.FormularioRespuesta.IdFormularioRespuestaPlantilla;
						formularioRespuesta.Urlbrochure = Json.FormularioRespuesta.Urlbrochure;
						formularioRespuesta.Urllogotipo = Json.FormularioRespuesta.Urllogotipo;
						formularioRespuesta.TextoInvitacionBrochure = Json.FormularioRespuesta.TextoInvitacionBrochure;
						formularioRespuesta.UsuarioModificacion = Json.Usuario;
						formularioRespuesta.FechaModificacion = DateTime.Now;

						repFormularioRespuesta.Update(formularioRespuesta);
					}
					scope.Complete();
					Json.FormularioRespuesta.Id = formularioRespuesta.Id;
				}

				return Ok(Json.FormularioRespuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]/{idFormularioRespuesta}/{Usuario}")]
		[HttpDelete]
		public IActionResult EliminarFormularioRespuesta(int idFormularioRespuesta, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FormularioRespuestaRepositorio repFormularioRespuesta = new FormularioRespuestaRepositorio();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repFormularioRespuesta.Exist(idFormularioRespuesta))
					{
						repFormularioRespuesta.Delete(idFormularioRespuesta, Usuario);
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



		[Route("[action]")]
		[HttpPost]
		public IActionResult ObtenerFormularioRespuesta([FromBody] FiltroPaginadorDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FormularioRespuestaRepositorio _repFormularioRespuesta = new FormularioRespuestaRepositorio();
				var valor = true;
				var lista = _repFormularioRespuesta.ListarFormularioRespuesta(Filtro);
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
		public ActionResult ObtenerPGeneralParaFiltro()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
				FormularioRespuestaPlantillaRepositorio _repFormularioRespuestaPlantilla = new FormularioRespuestaPlantillaRepositorio();
				PgeneralDescripcionRepositorio _repPgeneralDescripcion = new PgeneralDescripcionRepositorio();
				var pGeneral = _repPgeneral.GetBy(w => w.Estado == true, w => new { w.Id, w.Nombre });
				var formularioRespuestaPlantilla = _repFormularioRespuestaPlantilla.GetBy(w => w.Estado == true, w => new { w.Id, w.NombrePlantilla });
				return Ok(new { programaGeneral = pGeneral, formularioRespuestaP = formularioRespuestaPlantilla });
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerDescripcionProgramaGeneral([FromBody]Dictionary<string,string>Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var id = int.Parse(Filtro["id"]);
				PgeneralDescripcionRepositorio _repPgeneralDescripcion = new PgeneralDescripcionRepositorio();
				var pGeneralDescripcion = _repPgeneralDescripcion.GetBy(x => x.IdPgeneral == id, x => new { x.Id, x.IdPgeneral, x.Texto });
				return Ok(pGeneralDescripcion);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerPgeneralPorId([FromBody]Dictionary<string, string> Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
				var id = int.Parse(Filtro["id"]);
				var pGeneral = _repPgeneral.GetBy(x => x.Id == id);
				return Ok(pGeneral);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerPlantillaPorId([FromBody]Dictionary<string,string>Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FormularioRespuestaPlantillaRepositorio _repFormularioRespuestaPlantilla = new FormularioRespuestaPlantillaRepositorio();
				var id = int.Parse(Filtro["id"]);
				var formularioPlantilla = _repFormularioRespuestaPlantilla.GetBy(x => x.Id == id);
				return Ok(formularioPlantilla);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


	}

    public class ValidadorFormularioRespuestaDTO : AbstractValidator<TFormularioRespuesta>
    {
        public static ValidadorFormularioRespuestaDTO Current = new ValidadorFormularioRespuestaDTO();
        public ValidadorFormularioRespuestaDTO()
        {
            RuleFor(objeto => objeto.IdFormularioRespuestaPlantilla).NotEmpty().WithMessage("IdFormularioRespuesta es Obligatorio")
                                                    .NotNull().WithMessage("IdFormularioRespuesta no permite datos nulos");
        }

    }
}
