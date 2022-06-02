using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
	/// Documentacion
	/// <summary>
	/// 
	/// </summary>
	[Route("api/TrabajoDePares")]
	public class TrabajoDeParesController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;

		public TrabajoDeParesController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
		}


		/// TipoFuncion: GET
		/// Autor: Cesar Santillana
		/// Fecha: 30/06/2021
		/// Version: 1.0
		/// <summary>
		/// Función que trae data para llenar los combos de PEspecifico
		/// </summary>
		/// <returns>Retorna un objeto agrupado</returns>
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerDataCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
				ProveedorRepositorio _repProveedor = new ProveedorRepositorio();				
				var detalles = new
				{
					PEspecifico = _repPEspecifico.ObtenerProgramaEspecifico(),
					Proveedor = _repProveedor.ObtenerProveedorParaFiltro()
				};
				return Ok(detalles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Cesar Santillana
		/// Fecha: 30/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar el reporte de los trabajos de pares
		/// </summary>
		/// <param name="filtroReporte">Filtro para el reporte de los trabajos de pares  </param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarReporteTrabajoDePares([FromBody] TrabajoDeParesFiltroDTO FiltroReporte)
		{
			try 
			{
				TrabajoDeParesRepositorio repoTrabajoDePares = new TrabajoDeParesRepositorio();
				var lista = repoTrabajoDePares.GenerarReporteTrabajoDePares(FiltroReporte);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Miguel Mora
		/// Fecha: 31/12/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar un reporte con los nombre de lso programas especificos por el valor
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerProgramaEspecificoAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
					return Ok(_repPEspecifico.ObtenerProgramaEspecificoAutocomplete(Filtros["valor"].ToString()));
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 05/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza el encargado de revision de trabajo de Pares 
		/// </summary>
		/// <param name="Datos">Datos para acutalizar </param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPersonaCalificacionTrabajoPares([FromBody] TrabajoDeParesActualizacion Datos)
		{
			try
			{
				TrabajoDeParesRepositorio repoTrabajoDePares = new TrabajoDeParesRepositorio();
				var lista = repoTrabajoDePares.ActualizarEncargadoTrabajoPares(Datos.Id, Datos.IdProveedor);
				TrabajoDeParesDTO trabajoPares = new TrabajoDeParesDTO();
				ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
				var nombreProveedor = _repProveedor.ObtenerNombreProveedorById(Datos.IdProveedor);
				trabajoPares.NombreAlumnoResponsableRevision = nombreProveedor;
				trabajoPares.EsDocente = true;
				return Ok(trabajoPares);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: Get
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Retorna la lista de Programas generales y centro costo de los docentes encargados de revisar el trabajo de pares 
		/// </summary>
		/// <param name="IdProveedor">Id del docente</param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerProgramaGeneralCentroCostoDocente([FromBody] TrabajoDeParesActualizacion Datos)
		{
			try
			{
				TrabajoDeParesRepositorio repoTrabajoDePares = new TrabajoDeParesRepositorio();
				var lista = repoTrabajoDePares.ObtenerProgramaTrabajoPares(Datos.IdProveedor);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: Get
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Retorna la lista de Programas generales y centro costo de los docentes encargados de revisar el trabajo de pares 
		/// </summary>
		/// <param name="IdProveedor">Id del docente</param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerAlumnoDocenteTrabajoPares([FromBody] TrabajoDeParesActualizacion datos)
		{
			try
			{
				TrabajoDeParesRepositorio repoTrabajoDePares = new TrabajoDeParesRepositorio();
				var lista = repoTrabajoDePares.ObtenerAlumnoTrabajoPares(datos.IdProveedor, datos.IdPEspecifico);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: GET
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 20/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Funcion que envia correos para la solicitud de envio de certificados fisicos
		/// </summary>
		/// <param name="Datos">Tipo de dato TrabajoDeParesCorreoDTO</param>
		/// <returns>true</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult EnvioCorreoAsignacionTareaDocente([FromBody] TrabajoDeParesCorreoDTO Datos)
		{
			//Plantilla 1398
			IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
			ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
			PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
			MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
			var nombreProveedor= _repProveedor.ObtenerNombreProveedorById(Datos.IdProveedor);
			var idProgramaEspecifico = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(Datos.CodigoMatricula);
			var idPrograma = _repMatriculaCabecera.ObtenerProgramaGeneral(idProgramaEspecifico);
			var nombreProgramaGeneral =_repPgeneral.FirstById(idPrograma);
			TrabajoDeParesCorreoDetalleDTO correo = new TrabajoDeParesCorreoDetalleDTO();
			var correoProveedor = _repProveedor.ObtenerEmail(Datos.IdProveedor);
			var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(Datos.Usuario);
			correo.NombreProveedor = nombreProveedor;
			correo.NombrePrograma = nombreProgramaGeneral.Nombre;
			correo.NombreAlumno = Datos.NombreAlumno;
			correo.Email = correoCoordinadora;

			var idPlantilla = 1398;
			var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
			{
				IdPlantilla = idPlantilla
			};
			_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoAsignacionTareaDocente(correo);
			var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;

			
			List<string> correosPersonalizadosCopiaOculta = new List<string>
				{
					"lpacsi@bsginstitute.com",
				};


			TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
			{
				Sender = correoCoordinadora,
				//Sender = personal.Email,
				Recipient = correoProveedor,
				Subject = emailCalculado.Asunto,
				Message = emailCalculado.CuerpoHTML,
				Cc = "",
				Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct())
			};
			var mailServie = new TMK_MailServiceImpl();

			mailServie.SetData(mailDataPersonalizado);
			mailServie.SendMessageTask();

			return Ok(true);
		}
	}
}
