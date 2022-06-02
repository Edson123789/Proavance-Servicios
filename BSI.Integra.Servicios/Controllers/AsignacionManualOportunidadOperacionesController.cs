using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: AsignacionManualOportunidadOperacionesController
	/// Autor: Jose Villena.
	/// Fecha: 01/03/2021
	/// <summary>
	/// Asignacion Manual Oportunidad Operaciones
	/// </summary>
	[Route("api/AsignacionManualOportunidadOperaciones")]
	public class AsignacionManualOportunidadOperacionesController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PersonalRepositorio repPersonal;
		private readonly OportunidadRepositorio repOportunidad;
		private readonly CentroCostoRepositorio repCentroCosto;
		private readonly ActividadDetalleRepositorio repActividadDetalle;
		private readonly MatriculaCabeceraRepositorio repMatriculaCabecera;
		private readonly IntegraAspNetUsersRepositorio repIntegraAspNetUsers;
		private readonly AsignacionOportunidadRepositorio repAsignacionOportunidad;
		private readonly OportunidadClasificacionOperacionesRepositorio repOportunidadClasificacionOperaciones;
		private readonly OportunidadLogRepositorio repOportunidadLog;


		public AsignacionManualOportunidadOperacionesController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			repPersonal = new PersonalRepositorio(_integraDBContext);
			repOportunidad = new OportunidadRepositorio(_integraDBContext);
			repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
			repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
			repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
			repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
			repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
			repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
			repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Jose Villena.
		/// Fecha: 01/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos
		/// </summary>
		/// <returns>Objetos<returns>
		[Route("[action]/{IdPersonal}")]
		[HttpGet]
		public ActionResult ObtenerCombos(int IdPersonal)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
                EstadoMatriculaRepositorio repEstadoMatricula = new EstadoMatriculaRepositorio();
                MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio();

                var personal = repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal);
				var centroCosto = repCentroCosto.ObtenerCentroCostoParaFiltro();
                var estadomatricula = repEstadoMatricula.ObtenerTodoFiltro();
                var subestadomatricula = repMatriculaCabecera.ObtenerSubEstadoMatricula();

                return Ok(new { Personal = personal, CentroCosto = centroCosto,EstadoMatricula= estadomatricula , Subestadomatricula=subestadomatricula });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Jose Villena.
		/// Fecha: 01/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Oportunidades
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerOportunidades([FromBody] AsignacionManualOportunidadOperacionesFiltroGrillaDTO Obj)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ResultadoFiltroAsignacionOportunidadDTO OportunidadManual = new ResultadoFiltroAsignacionOportunidadDTO();

				if (Obj.Filtro.ListaPersonal.Count() == 0)
				{
					var asistentesCargo = repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(Obj.Filtro.Personal);
					List<int> ListaCoordinadortmp = new List<int>();
					foreach (var item in asistentesCargo)
					{
						ListaCoordinadortmp.Add(item.Id);
					}
					Obj.Filtro.ListaPersonal = ListaCoordinadortmp;

					var codigos = new List<string>();
					if (Obj.Filtro.ListaCodigoMatricula != null) codigos = Obj.Filtro.ListaCodigoMatricula;
					OportunidadManual = repOportunidad.ObtenerPorFiltroPaginaManualOperaciones(Obj.Paginador, Obj.Filtro, Obj.Filter, codigos);
				}
                else
                {
					var codigos = new List<string>();
					if (Obj.Filtro.ListaCodigoMatricula != null) codigos = Obj.Filtro.ListaCodigoMatricula;
					OportunidadManual = repOportunidad.ObtenerPorFiltroPaginaManualOperaciones(Obj.Paginador, Obj.Filtro, Obj.Filter, codigos);
				}
				

				return Ok(OportunidadManual);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 01/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Enlista los codigos de matricula del excel
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerOportunidadesExcel([FromForm] IFormFile ArchivoExcel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				Paginador pag = new Paginador();
				pag.take = 10;
				pag.page = 1;
				pag.pageSize = 10;
				pag.skip =0;
				AsignacionManualOportunidadOperacionesFiltroDTO asignacion = new AsignacionManualOportunidadOperacionesFiltroDTO();
				GridFilters grid = new GridFilters();

				List<string> codigos = new List<string>();
				using (var paquete = new ExcelPackage(ArchivoExcel.OpenReadStream()))
				{

					var worksheet = paquete.Workbook.Worksheets[0];

					var inicio = worksheet.Dimension.Start;
					var final = worksheet.Dimension.End;

					#region Inicializacion Valores
					List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
					listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "matriculas", Columna = 0, FlagObligatorio = true });
					#endregion

					object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
					
					//var OportunidadManual = repOportunidad.ObtenerPorFiltroPaginaManualOperaciones([], [], Obj.Filter, codigos);
					for (int i = inicio.Row; i < final.Row; i++)
					{
						codigos.Add((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "matriculas").Columna] ?? string.Empty).ToString());
					}

				}
				//var OportunidadManual = repOportunidad.ObtenerPorFiltroPaginaManualOperaciones(pag, asignacion,null, codigos);


				return Ok(codigos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Jose Villena.
		/// Fecha: 01/03/2021
		/// Versión: 2.0
		/// <summary>
		/// Asigna Oportunidad Operaciones
		/// modificacion: se agrego la modificacion de los gestores en el cronograma
		/// </summary>
		/// <returns><returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult AsignarOportunidadOperaciones([FromBody]AsignarOportunidadOperacionesFiltroDTO Obj)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					foreach (int idOportunidad in Obj.ListaOportunidades)
					{

						OportunidadBO opo = new OportunidadBO(idOportunidad, Obj.Usuario, _integraDBContext);
						AsignacionOportunidadLogBO asignacionLog = new AsignacionOportunidadLogBO();
						int idant = opo.IdPersonalAsignado;
						int idactCabant = opo.IdActividadCabeceraUltima;
						asignacionLog.FechaLog = DateTime.Now;
						asignacionLog.IdPersonalAnterior = opo.IdPersonalAsignado;
						asignacionLog.IdCentroCostoAnt = opo.IdCentroCosto;
						asignacionLog.IdOportunidad = opo.Id;

						opo.Id = idOportunidad;
						opo.IdPersonalAsignado = Obj.IdPersonal;

						opo.AsignacionOportunidad = repAsignacionOportunidad.ObtenerPorIdOportunidad(idOportunidad);
						AsignacionOportunidadBO asig = new AsignacionOportunidadBO();

						if (opo.AsignacionOportunidad == null)
						{
							//Creamos un nuevo registro en asignacionOportunidad
							asig.FechaAsignacion = DateTime.Now;
							asig.IdAlumno = opo.IdAlumno;
							asig.IdClasificacionPersona = opo.IdClasificacionPersona;
							asig.IdPersonal = opo.IdPersonalAsignado;
							asig.IdCentroCosto = opo.IdCentroCosto.Value;
							asig.IdOportunidad = idOportunidad;
							asig.IdTipoDato = opo.IdTipoDato;
							asig.IdFaseOportunidad = opo.IdFaseOportunidad;
							asig.IdClasificacionPersona = opo.IdClasificacionPersona;
							asig.Estado = true;
							asig.FechaCreacion = DateTime.Now;
							asig.FechaModificacion = DateTime.Now;
							asig.UsuarioCreacion = Obj.Usuario;
							asig.UsuarioModificacion = Obj.Usuario;
							opo.AsignacionOportunidad = asig;
						}
						opo.AsignacionOportunidad.FechaAsignacion = DateTime.Now;
						opo.AsignacionOportunidad.IdPersonal = opo.IdPersonalAsignado == 0 ? opo.AsignacionOportunidad.IdPersonal : opo.IdPersonalAsignado;
						opo.AsignacionOportunidad.IdCentroCosto = opo.IdCentroCosto == 0 ? opo.AsignacionOportunidad.IdCentroCosto : opo.IdCentroCosto.Value;
						opo.AsignacionOportunidad.IdAlumno = opo.IdAlumno == 0 ? opo.AsignacionOportunidad.IdAlumno : opo.IdAlumno;
						opo.AsignacionOportunidad.IdClasificacionPersona = opo.IdClasificacionPersona == 0 ? opo.AsignacionOportunidad.IdClasificacionPersona : opo.IdClasificacionPersona;
						opo.AsignacionOportunidad.IdPersonal = opo.IdPersonalAsignado == 0 ? opo.AsignacionOportunidad.IdPersonal : opo.IdPersonalAsignado;
						opo.AsignacionOportunidad.FechaModificacion = DateTime.Now;
						opo.AsignacionOportunidad.UsuarioModificacion = Obj.Usuario;

						asignacionLog.IdTipoDato = opo.AsignacionOportunidad.IdTipoDato;
						asignacionLog.IdPersonal = opo.AsignacionOportunidad.IdPersonal;
						asignacionLog.IdFaseOportunidad = opo.AsignacionOportunidad.IdFaseOportunidad;
						asignacionLog.IdAlumno = opo.AsignacionOportunidad.IdAlumno;
						asignacionLog.IdClasificacionPersona = opo.AsignacionOportunidad.IdClasificacionPersona;
						asignacionLog.Estado = true;
						asignacionLog.FechaCreacion = DateTime.Now;
						asignacionLog.FechaModificacion = DateTime.Now;
						asignacionLog.UsuarioCreacion = Obj.Usuario;
						asignacionLog.UsuarioModificacion = Obj.Usuario;
						asignacionLog.IdCentroCosto = opo.AsignacionOportunidad.IdCentroCosto;
						asignacionLog.IdAsignacionOportunidad = opo.AsignacionOportunidad.Id;
						opo.AsignacionOportunidad.AsignacionOportunidadLog = asignacionLog;


						//Finalizar Actividad
						opo.ActividadAntigua = new ActividadDetalleBO(opo.IdActividadDetalleUltima, _integraDBContext);
						opo.ActividadAntigua.Comentario = "Asignacion Manual";
						opo.ActividadAntigua.IdOcurrenciaActividad = null;
						opo.ActividadAntigua.IdAlumno = opo.IdAlumno;
						opo.ActividadAntigua.IdClasificacionPersona = opo.IdClasificacionPersona;
						opo.ActividadAntigua.IdOportunidad = opo.Id;
						opo.ActividadAntigua.IdCentralLlamada = 0;
						opo.ActividadAntigua.IdActividadCabecera = opo.IdActividadCabeceraUltima;


						OportunidadDTO oportunidad = new OportunidadDTO();

						oportunidad.Id = opo.Id;
						oportunidad.IdCentroCosto = opo.IdCentroCosto.Value;
						oportunidad.IdPersonalAsignado = opo.IdPersonalAsignado;
						oportunidad.IdTipoDato = opo.IdTipoDato;
						oportunidad.IdFaseOportunidad = opo.IdFaseOportunidad;
						oportunidad.IdOrigen = opo.IdOrigen;
						oportunidad.IdAlumno = opo.IdAlumno;
						oportunidad.UltimoComentario = opo.UltimoComentario;
						oportunidad.IdActividadDetalleUltima = opo.IdActividadDetalleUltima;
						oportunidad.IdActividadCabeceraUltima = opo.IdActividadCabeceraUltima;
						oportunidad.IdEstadoActividadDetalleUltimoEstado = opo.IdEstadoActividadDetalleUltimoEstado;
						oportunidad.UltimaFechaProgramada = opo.UltimaFechaProgramada.ToString();
						oportunidad.IdEstadoOportunidad = opo.IdEstadoOportunidad;
						oportunidad.IdEstadoOcurrenciaUltimo = opo.IdEstadoOcurrenciaUltimo;
						oportunidad.IdFaseOportunidadMaxima = opo.IdFaseOportunidadMaxima;
						oportunidad.IdFaseOportunidadInicial = opo.IdFaseOportunidadInicial;
						oportunidad.IdCategoriaOrigen = opo.IdCategoriaOrigen;
						oportunidad.IdConjuntoAnuncio = opo.IdConjuntoAnuncio;
						oportunidad.IdCampaniaScoring = opo.IdCampaniaScoring;
						oportunidad.IdFaseOportunidadIp = opo.IdFaseOportunidadIp;
						oportunidad.IdFaseOportunidadIc = opo.IdFaseOportunidadIc;
						oportunidad.FechaEnvioFaseOportunidadPf = opo.FechaEnvioFaseOportunidadPf;
						oportunidad.FechaPagoFaseOportunidadPf = opo.FechaPagoFaseOportunidadPf;
						oportunidad.FechaPagoFaseOportunidadIc = opo.FechaPagoFaseOportunidadIc;
						oportunidad.FechaRegistroCampania = opo.FechaRegistroCampania;
						oportunidad.IdFaseOportunidadPortal = opo.IdFaseOportunidadPortal;
						oportunidad.IdFaseOportunidadPf = opo.IdFaseOportunidadPf;
						oportunidad.CodigoPagoIc = opo.CodigoPagoIc;
						oportunidad.FlagVentaCruzada = opo.IdTiempoCapacitacion;
						oportunidad.IdTiempoCapacitacion = opo.IdTiempoCapacitacion;
						oportunidad.IdTiempoCapacitacionValidacion = opo.IdTiempoCapacitacionValidacion;
						oportunidad.IdSubCategoriaDato = opo.IdSubCategoriaDato;
						oportunidad.IdInteraccionFormulario = opo.IdInteraccionFormulario;
						oportunidad.UrlOrigen = opo.UrlOrigen;
						oportunidad.IdClasificacionPersona = opo.IdClasificacionPersona;
						oportunidad.IdPersonalAreaTrabajo = opo.IdPersonalAreaTrabajo;

						opo.FinalizarActividadAsignacionManualOperaciones(false, oportunidad);

						opo.OportunidadLogNueva.IdAsesorAnt = idant;
						opo.UltimaFechaProgramada = DateTime.Now;
						opo.ProgramaActividadAsignacionManualOperaciones();
						opo.ActividadNueva.IdActividadCabecera = idactCabant;
						repOportunidad.Update(opo);
						var oco = repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == idOportunidad);
						var mat = repMatriculaCabecera.FirstById(oco.IdMatriculaCabecera);
						var user = repIntegraAspNetUsers.FirstBy(x => x.PerId == Obj.IdPersonal);

						if (user.UserName.Equals("esanchez1"))
						{
							user.UserName = "esanchez";
						}
						mat.UsuarioCoordinadorAcademico = user.UserName;
						mat.FechaModificacion = DateTime.Now;
						repMatriculaCabecera.Update(mat);
						repMatriculaCabecera.ModificarGestorDeCobranza(Obj.Usuario,mat.UsuarioCoordinadorAcademico,oco.IdMatriculaCabecera);
					}
					scope.Complete();
				}
				foreach (int idOportunidad in Obj.ListaOportunidades)
				{
					OportunidadBO oportunidad = new OportunidadBO();
					OportunidadLogBO oportunidadLog = new OportunidadLogBO();
					OportunidadRepositorio repOportunidad2 = new OportunidadRepositorio();
					OportunidadLogRepositorio repOportunidadLog2 = new OportunidadLogRepositorio();
					ActividadDetalleRepositorio repActividadDetalle2 = new ActividadDetalleRepositorio();
					var actividadDetalle = repActividadDetalle2.GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
					if (actividadDetalle != null)
					{
						oportunidad = repOportunidad2.FirstById(idOportunidad);
						oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
						oportunidadLog = repOportunidadLog2.GetBy(w => w.IdOportunidad == idOportunidad).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
						oportunidadLog.IdActividadDetalle = actividadDetalle.Id;
						repOportunidad2.Update(oportunidad);
						//repOportunidadLog2.Update(oportunidadLog);
					}
					try
					{
						if (Obj.IdPersonal != null)
						{
							AgendaSocket.getInstance().NuevaActividadParaEjecutar(Obj.ListaOportunidades[0], Obj.IdPersonal);
						}
					}
					catch (Exception)
					{
					}

				}

				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message + (ex.InnerException != null ? (" - " + ex.InnerException.Message) : ""));
			}
		}
	}
}
