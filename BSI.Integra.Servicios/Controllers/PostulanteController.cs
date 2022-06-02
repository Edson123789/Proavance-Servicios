using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using System.Linq;
using System.Web.Helpers;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using RestSharp;
using BSI.Integra.Servicios.DTOs;
using Newtonsoft.Json;
using Nancy.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Google.Api.Ads.Common.Util;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using CsvHelper;
using System.IO;
using System.Transactions;
using System.Data.Entity.ModelConfiguration.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using AngleSharp.Common;
using NLog.Targets.Wrappers;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/Postulante")]
	[ApiController]
	public class PostulanteController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly ProveedorRepositorio _repProveedor;
		private readonly PersonalRepositorio _repPersonal;
		private readonly EstadoEtapaProcesoSeleccionRepositorio _repEstadoEtapaProcesoSeleccion;
		private readonly ProcesoSeleccionEtapaRepositorio _repProcesoSeleccionEtapa;
		private readonly ConvocatoriaPersonalRepositorio _repConvocatoriaPersonal;
		private readonly PostulanteNivelPotencialRepositorio _repPostulanteNivelPotencial;
		private readonly EtapaProcesoSeleccionCalificadoRepositorio _reEtapaCalificacion;
		private readonly RespuestaPreguntaRepositorio _repRespuestaPregunta;
		private readonly ExamenRealizadoRespuestaEvaluadorRepositorio _repExamenRealizadoRespuestaEvaluador;
		public PostulanteController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repProveedor = new ProveedorRepositorio(_integraDBContext);
			_repPersonal = new PersonalRepositorio(_integraDBContext);
			_repEstadoEtapaProcesoSeleccion = new EstadoEtapaProcesoSeleccionRepositorio(_integraDBContext);
			_repProcesoSeleccionEtapa = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
			_repConvocatoriaPersonal = new ConvocatoriaPersonalRepositorio(_integraDBContext);
			_repPostulanteNivelPotencial = new PostulanteNivelPotencialRepositorio(_integraDBContext);
			_reEtapaCalificacion = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
			_repRespuestaPregunta = new RespuestaPreguntaRepositorio(_integraDBContext);
			_repExamenRealizadoRespuestaEvaluador = new ExamenRealizadoRespuestaEvaluadorRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCombosPostulante()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoDocumentoPersonalRepositorio _repTipoDocumento = new TipoDocumentoPersonalRepositorio(_integraDBContext);
				ProcesoSeleccionRepositorio _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
				CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
				PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);
				SexoRepositorio _repSexo = new SexoRepositorio(_integraDBContext);
				CentroEstudioRepositorio _repCentroEstudio = new CentroEstudioRepositorio(_integraDBContext);
				TipoEstudioRepositorio _repTipoEstudio = new TipoEstudioRepositorio(_integraDBContext);
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				AreaFormacionRepositorio _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
				EmpresaRepositorio _repEmpresa = new EmpresaRepositorio(_integraDBContext);
				CargoRepositorio _repCargo = new CargoRepositorio(_integraDBContext);
				AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
				IndustriaRepositorio _repIndustria = new IndustriaRepositorio(_integraDBContext);
				MonedaRepositorio _repMoneda = new MonedaRepositorio(_integraDBContext);

				var documentos = _repTipoDocumento.GetFiltroIdNombre();
				var procesoSeleccion = _repProcesoSeleccion.ObtenerProcesoSeleccion();
				var procesoSeleccionTotal= _repProcesoSeleccion.ObtenerProcesoSeleccionTotal();
				var pais = _repPais.ObtenerListaPais();
				var ciudad = _repCiudad.ObtenerCiudadFiltro();
				var estadoProcesoSeleccion = _repProcesoSeleccion.ObtenerEstadoProcesoSeleccion();
				var plantilla = _repPlantilla.ObtenerTodoFiltroGP();
				var plantillaWhatsApp = _repPlantilla.ObtenerTodoFiltroGPWhatsApp();
				var proveedor = _repProveedor.ObtenerProveedoresConvocatoriaPersonal().Select(x => new FiltroBasicoDTO { Id = x.IdProveedor, Nombre = x.RazonSocial }).ToList();
				var personalGP = _repPersonal.getDatosPersonalGestionPersonas().Select(x => new FiltroBasicoDTO { Id = x.Id, Nombre = x.NombreCompleto }).ToList();
				var etapas = _repProcesoSeleccionEtapa.GetBy(x => x.Estado == true).Select(x => new { IdEtapa = x.Id, Nombre = x.Nombre, Id = x.IdProcesoSeleccion }).ToList();
				var estadoEtapas = _repEstadoEtapaProcesoSeleccion.GetBy(x => x.Estado == true).Select(x => new FiltroBasicoDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
				var convocatoria = _repConvocatoriaPersonal.GetBy(x => x.Estado == true).Select(x => new { IdConvocatoria = x.Id, Nombre = x.Codigo + " - " + x.Nombre, Id = x.IdProcesoSeleccion }).ToList();
				var nivelPotencial = _repPostulanteNivelPotencial.GetBy(x => x.Estado == true).Select(x => new FiltroBasicoDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
				var FactorDesaprobatorio = _repRespuestaPregunta.GetBy(x => x.Estado == true && x.IdPregunta == 761).Select(x => new { IdRespuestaDesaprobatoria = x.Id, Nombre = x.EnunciadoRespuesta, Id = 2 }).ToList();
				var sexo = _repSexo.GetFiltroIdNombre();
				var centroEstudio = _repCentroEstudio.ObtenerListaParaFiltro();
				var tipoEstudio = _repTipoEstudio.ObtenerListaParaFiltro();
				var estadoEstudio = _repPostulante.ObtenerListaEstadoEstudioParaFiltro();
				var areaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro();
				var empresa = _repEmpresa.ObtenerTodoEmpresasFiltro();
				var cargo = _repCargo.ObtenerCargoFiltro();
				var areaTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro();
				var industria = _repIndustria.ObtenerIndustriaFiltro();
				var moneda = _repMoneda.ObtenerFiltroMoneda();

				return Ok(new
				{
					ProcesoSeleccion = procesoSeleccion,
					ProcesoSeleccionTotal = procesoSeleccionTotal,
					Documento = documentos,
					Pais = pais,
					Ciudad = ciudad,
					Sexo = sexo,
					CentroEstudio = centroEstudio,
					ListaEmpresa = empresa,
					Cargo = cargo,
					Industria = industria,
					Moneda = moneda,
					AreaTrabajo = areaTrabajo,
					Nivel = tipoEstudio,
					EstadoEstudio = estadoEstudio,
					AreaFormacion = areaFormacion,
					EstadoProcesoSeleccion = estadoProcesoSeleccion,
					Plantilla = plantilla,
					PlantillaWhatsApp = plantillaWhatsApp
				,
					listaProveedor = proveedor,
					listaPersonal = personalGP,
					listaEtapas = etapas,
					listaEstadoEtapas = estadoEtapas,
					listaCodigoConvocatoria = convocatoria,
					listaNivelPotencial = nivelPotencial,
					listaRespuestaDesaprobatoria = FactorDesaprobatorio
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPostulanteInscritos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var Postulante = _repPostulante.ObtenerPostulantesInscritos();

				foreach (var item in Postulante)
				{
					if (item.IdRespuestas != null)
					{
						var x = item.IdRespuestas.Split(",");
						foreach (var idRespuesta in x)
						{
							if (!idRespuesta.Equals(""))
							{
								if (item.ListaRespuestaDesaprobatoria == null)
								{
									item.ListaRespuestaDesaprobatoria = new List<int>();
								}
								int y = Int32.Parse(idRespuesta);
								item.ListaRespuestaDesaprobatoria.Add(Int32.Parse(idRespuesta));
							}
						}
					}
				}

				return Ok(Postulante);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPostulante([FromBody] PostulanteFormularioV2DTO Postulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
				PostulanteBO postulante = new PostulanteBO();
				PersonaBO persona = new PersonaBO(_integraDBContext);
				ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
				var existe = _repPostulante.FirstBy(x => x.Email.Equals(Postulante.InformacionPostulante.Email.Trim()));
				var personal = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Postulante.Usuario);
				var IdExamenAsignadoEvaluadorCriterioDesaprobatorio = 0;
				ExamenAsignadoEvaluadorBO examenAsignadoEvaluadorCriterioDesaprobatorioBO = new ExamenAsignadoEvaluadorBO();

				PostulanteLogRepositorio _repPostulanteLog = new PostulanteLogRepositorio(_integraDBContext);
				PostulanteLogBO postulanteLog = new PostulanteLogBO();
				TipoDocumentoRepositorio _repTipoDocumento = new TipoDocumentoRepositorio(_integraDBContext);
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
				CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);

				if (existe != null)
				{
					var mensaje = "No se realizo el registro ya que el postulante ya existe";
					return Ok(mensaje);
				}
				else
				{
					using (TransactionScope scope = new TransactionScope())
					{
						postulante.Nombre = Postulante.InformacionPostulante.Nombre;
						postulante.ApellidoPaterno = Postulante.InformacionPostulante.ApellidoPaterno;
						postulante.ApellidoMaterno = Postulante.InformacionPostulante.ApellidoMaterno;
						postulante.NroDocumento = Postulante.InformacionPostulante.NroDocumento;
						postulante.Celular = Postulante.InformacionPostulante.Celular;
						postulante.Email = Postulante.InformacionPostulante.Email;
						postulante.IdTipoDocumento = Postulante.InformacionPostulante.IdTipoDocumento;
						postulante.IdPais = Postulante.InformacionPostulante.IdPais;
						postulante.IdCiudad = Postulante.InformacionPostulante.IdCiudad;
						postulante.Estado = true;
						postulante.UsuarioCreacion = Postulante.Usuario;
						postulante.UsuarioModificacion = Postulante.Usuario;
						postulante.FechaCreacion = DateTime.Now;
						postulante.FechaModificacion = DateTime.Now;
						_repPostulante.Insert(postulante);

						postulanteLog.IdPostulante = postulante.Id;
						postulanteLog.Estado = true;
						postulanteLog.UsuarioCreacion = Postulante.Usuario;
						postulanteLog.UsuarioModificacion = Postulante.Usuario;
						postulanteLog.FechaCreacion = DateTime.Now;
						postulanteLog.FechaModificacion = DateTime.Now;
						postulanteLog.Clave = "Nombre";
						postulanteLog.Valor = postulante.Nombre;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						postulanteLog.Clave = "ApellidoPaterno";
						postulanteLog.Valor = postulante.ApellidoPaterno;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						postulanteLog.Clave = "ApellidoMaterno";
						postulanteLog.Valor = postulante.ApellidoMaterno;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						postulanteLog.Clave = "NroDocumento";
						postulanteLog.Valor = postulante.NroDocumento;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						postulanteLog.Clave = "Celular";
						postulanteLog.Valor = postulante.Celular;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						postulanteLog.Clave = "Email";
						postulanteLog.Valor = postulante.Email;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
						if (postulante.IdTipoDocumento != null)
						{
							postulanteLog.Clave = "IdTipoDocumento";
							postulanteLog.Valor = _repTipoDocumento.FirstById((int)postulante.IdTipoDocumento).Descripcion;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						if (postulante.IdPais != null)
						{
							postulanteLog.Clave = "IdPais";
							postulanteLog.Valor = _repPais.FirstById((int)postulante.IdPais).NombrePais;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						if (postulante.IdCiudad != null)
						{
							postulanteLog.Clave = "IdCiudad";
							postulanteLog.Valor = _repCiudad.FirstById((int)postulante.IdCiudad).Nombre;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}


						int? idCreacionCorrecta = persona.InsertarPersona(postulante.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Postulante, Postulante.Usuario);
						if (idCreacionCorrecta == null)
						{
							throw new Exception("No se creo clasificacion persona");
						}
						if (Postulante.InformacionPostulante.IdProcesoSeleccion.HasValue)
						{
							PostulanteProcesoSeleccionBO postulanteProcesoSeleccion = new PostulanteProcesoSeleccionBO
							{
								IdPostulante = postulante.Id,
								IdProcesoSeleccion = Postulante.InformacionPostulante.IdProcesoSeleccion.Value,
								FechaRegistro = DateTime.Now,
								Estado = true,
								UsuarioCreacion = Postulante.Usuario,
								UsuarioModificacion = Postulante.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								IdEstadoProcesoSeleccion = Postulante.InformacionPostulante.IdEstadoProcesoSeleccion,
								IdPostulanteNivelPotencial = Postulante.InformacionPostulante.IdPostulanteNivelPotencial,
								IdProveedor = Postulante.InformacionPostulante.IdProveedor,
								IdPersonalOperadorProceso = Postulante.InformacionPostulante.IdPersonal_OperadorProceso,
								IdConvocatoriaPersonal = Postulante.InformacionPostulante.IdConvocatoriaPersonal
							};
							var res = _repPostulanteProcesoSeleccion.Insert(postulanteProcesoSeleccion);
							if (res)
							{
								var postulacion = _repExamenAsignado.FirstBy(x => x.IdPostulante == postulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulanteProcesoSeleccion.IdProcesoSeleccion);
								if (postulacion == null)
								{
									//var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(postulanteProcesoSeleccion.IdProcesoSeleccion);
									var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
									var examenEvaluadorPorProceso = _repExamenAsignadoEvaluador.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
									foreach (var item in examenPorProceso)
									{
										ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
										examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
										examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
										examenAsignado.IdExamen = item.IdExamen;
										examenAsignado.EstadoExamen = false;
										examenAsignado.Estado = true;
										examenAsignado.UsuarioCreacion = Postulante.Usuario;
										examenAsignado.UsuarioModificacion = Postulante.Usuario;
										examenAsignado.FechaCreacion = DateTime.Now;
										examenAsignado.FechaModificacion = DateTime.Now;
										_repExamenAsignado.Insert(examenAsignado);
									}
									foreach (var item in examenEvaluadorPorProceso)
									{
										ExamenAsignadoEvaluadorBO examenAsignadoEvaluador = new ExamenAsignadoEvaluadorBO();
										examenAsignadoEvaluador.IdPersonal = personal.Id;
										examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
										examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
										examenAsignadoEvaluador.IdExamen = item.IdExamen;
										examenAsignadoEvaluador.EstadoExamen = false;
										examenAsignadoEvaluador.Estado = true;
										examenAsignadoEvaluador.UsuarioCreacion = Postulante.Usuario;
										examenAsignadoEvaluador.UsuarioModificacion = Postulante.Usuario;
										examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
										examenAsignadoEvaluador.FechaModificacion = DateTime.Now;

										if (examenAsignadoEvaluador.IdExamen == 111 && Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria != null)
										{
											examenAsignadoEvaluador.EstadoExamen = true;
										}
										_repExamenAsignadoEvaluador.Insert(examenAsignadoEvaluador);

										if (examenAsignadoEvaluador.IdExamen == 111)
										{
											IdExamenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador.Id;
											examenAsignadoEvaluadorCriterioDesaprobatorioBO = examenAsignadoEvaluador;
										}
									}

									if (IdExamenAsignadoEvaluadorCriterioDesaprobatorio > 0 && Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria != null)
									{

										foreach (var item in Postulante.InformacionPostulante.ListaRespuestaDesaprobatoria)
										{
											ExamenRealizadoRespuestaEvaluadorBO evaluadorExamen = new ExamenRealizadoRespuestaEvaluadorBO();
											evaluadorExamen.IdExamenAsignadoEvaluador = IdExamenAsignadoEvaluadorCriterioDesaprobatorio;
											evaluadorExamen.IdPregunta = 761; // Id de Pregunta de Examen de Criterio de Desaprobacion
											evaluadorExamen.IdRespuestaPregunta = item.IdRespuestaDesaprobatoria;
											evaluadorExamen.TextoRespuesta = null;
											evaluadorExamen.Estado = true;
											evaluadorExamen.UsuarioCreacion = Postulante.Usuario;
											evaluadorExamen.UsuarioModificacion = Postulante.Usuario;
											evaluadorExamen.FechaCreacion = DateTime.Now;
											evaluadorExamen.FechaModificacion = DateTime.Now;

											_repExamenRealizadoRespuestaEvaluador.Insert(evaluadorExamen);
										}
									}
								}

								//Se asignan todas las etapas del proceso al postulante
								var EtapasProceso = _repProcesoSeleccionEtapa.GetBy(x => x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion && x.Estado == true);
								foreach (var item in EtapasProceso)
								{
									EtapaProcesoSeleccionCalificadoBO etapaCalificacion = new EtapaProcesoSeleccionCalificadoBO();
									etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
									etapaCalificacion.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
									if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item.Id)
									{
										etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
										etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion;
										etapaCalificacion.EsEtapaActual = true;
									}
									else
									{
										etapaCalificacion.EsEtapaAprobada = false;
										etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
										etapaCalificacion.EsEtapaActual = false;
									}
									etapaCalificacion.EsContactado = false;
									etapaCalificacion.Estado = true;
									etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
									etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
									etapaCalificacion.FechaCreacion = DateTime.Now;
									etapaCalificacion.FechaModificacion = DateTime.Now;
									_reEtapaCalificacion.Insert(etapaCalificacion);
								}
							}
							else
							{
								return BadRequest(ModelState);
							}
						}


						scope.Complete();
						return Ok(true);
					}
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPostulante([FromBody] PostulanteFormularioV2DTO Postulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

				PostulanteLogRepositorio _repPostulanteLog = new PostulanteLogRepositorio(_integraDBContext);
				PostulanteLogBO postulanteLog = new PostulanteLogBO();
				TipoDocumentoRepositorio _repTipoDocumento = new TipoDocumentoRepositorio(_integraDBContext);
				PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
				CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
				SexoRepositorio _repSexo = new SexoRepositorio(_integraDBContext);
				PersonaRepositorio _repPersona = new PersonaRepositorio(_integraDBContext);
				ClasificacionPersonaRepositorio _repClasificacionPersona = new ClasificacionPersonaRepositorio(_integraDBContext);
				ConvocatoriaPersonalRepositorio _repConvocatoriaPersonal = new ConvocatoriaPersonalRepositorio(_integraDBContext);
				EstadoEtapaProcesoSeleccionRepositorio _repEstadoProcesoSeleccion = new EstadoEtapaProcesoSeleccionRepositorio(_integraDBContext);
				EtapaProcesoSeleccionCalificadoRepositorio _repEtapaProceso = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);


				if (_repPostulante.Exist(Postulante.InformacionPostulante.Id))
				{

					var personal = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Postulante.Usuario);
					var postulante = _repPostulante.FirstById(Postulante.InformacionPostulante.Id);

					if (postulante.Email != Postulante.InformacionPostulante.Email)
					{
						var cantidadPersona = _repPersona.GetBy(x => x.Email1 == Postulante.InformacionPostulante.Email).ToList();

						if (cantidadPersona.Count > 0)
						{
							return BadRequest("Ya existe un registro con este correo");
						}
						else
						{
							var clasificacionPersona = _repClasificacionPersona.FirstBy(x => x.IdTablaOriginal == Postulante.InformacionPostulante.Id && x.IdTipoPersona == 5);
							var persona = _repPersona.FirstBy(x => x.Id == clasificacionPersona.IdPersona);
							persona.Email1 = Postulante.InformacionPostulante.Email;
							persona.UsuarioModificacion = Postulante.Usuario;
							persona.FechaModificacion = DateTime.Now;
							_repPersona.Update(persona);

						}
					}
					// Registro de historial Log
					postulanteLog.IdPostulante = postulante.Id;
					postulanteLog.Estado = true;
					postulanteLog.UsuarioCreacion = Postulante.Usuario;
					postulanteLog.UsuarioModificacion = Postulante.Usuario;
					postulanteLog.FechaCreacion = DateTime.Now;
					postulanteLog.FechaModificacion = DateTime.Now;
					if (postulante.Nombre != Postulante.InformacionPostulante.Nombre)
					{
						postulanteLog.Clave = "Nombre";
						postulanteLog.Valor = Postulante.InformacionPostulante.Nombre;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.ApellidoPaterno != Postulante.InformacionPostulante.ApellidoPaterno)
					{
						postulanteLog.Clave = "ApellidoPaterno";
						postulanteLog.Valor = Postulante.InformacionPostulante.ApellidoPaterno;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.ApellidoMaterno != Postulante.InformacionPostulante.ApellidoMaterno)
					{
						postulanteLog.Clave = "ApellidoMaterno";
						postulanteLog.Valor = Postulante.InformacionPostulante.ApellidoMaterno;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.NroDocumento != Postulante.InformacionPostulante.NroDocumento)
					{
						postulanteLog.Clave = "NroDocumento";
						postulanteLog.Valor = Postulante.InformacionPostulante.NroDocumento;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.Celular != Postulante.InformacionPostulante.Celular)
					{
						postulanteLog.Clave = "Celular";
						postulanteLog.Valor = Postulante.InformacionPostulante.Celular;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.Email != Postulante.InformacionPostulante.Email)
					{
						postulanteLog.Clave = "Email";
						postulanteLog.Valor = Postulante.InformacionPostulante.Email;
						_repPostulanteLog.Insert(postulanteLog);
						postulanteLog.Id = 0;
					}
					if (postulante.IdTipoDocumento != Postulante.InformacionPostulante.IdTipoDocumento)
					{
						if (Postulante.InformacionPostulante.IdTipoDocumento != null)
						{
							postulanteLog.Clave = "IdTipoDocumento";
							postulanteLog.Valor = _repTipoDocumento.FirstById((int)Postulante.InformacionPostulante.IdTipoDocumento).Descripcion;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "IdTipoDocumento";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					if (postulante.IdPais != Postulante.InformacionPostulante.IdPais)
					{
						if (Postulante.InformacionPostulante.IdPais != null)
						{
							postulanteLog.Clave = "IdPais";
							postulanteLog.Valor = _repPais.FirstById((int)Postulante.InformacionPostulante.IdPais).NombrePais;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "IdPais";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					if (postulante.IdCiudad != Postulante.InformacionPostulante.IdCiudad)
					{
						if (Postulante.InformacionPostulante.IdCiudad != null)
						{
							postulanteLog.Clave = "IdCiudad";
							postulanteLog.Valor = _repCiudad.FirstById((int)Postulante.InformacionPostulante.IdCiudad).Nombre;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "IdCiudad";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					if (postulante.IdSexo != Postulante.InformacionPostulante.IdSexo)
					{
						if (Postulante.InformacionPostulante.IdSexo != null)
						{
							postulanteLog.Clave = "IdSexo";
							postulanteLog.Valor = _repSexo.FirstById((int)Postulante.InformacionPostulante.IdSexo).Nombre;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "IdSexo";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}

					var Fecha = postulante.FechaNacimiento.ToString();
					var temp1 = Fecha.Split(" ");
					var FechaBD = Postulante.InformacionPostulante.FechaNacimiento.ToString();
					var temp2 = FechaBD.Split(" ");
					if (temp1[0] != temp2[0])
					{
						if (Postulante.InformacionPostulante.FechaNacimiento != null)
						{
							postulanteLog.Clave = "FechaNacimiento";
							postulanteLog.Valor = ((DateTime)Postulante.InformacionPostulante.FechaNacimiento).ToString("dd/MM/yyyy");
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "FechaNacimiento";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					var hijos = postulante.CantidadHijo;
					if (hijos == null)
					{
						hijos = 0;
					}
					if (hijos != Postulante.InformacionPostulante.CantidadHijo)
					{
						if (Postulante.InformacionPostulante.CantidadHijo != null)
						{
							postulanteLog.Clave = "CantidadHijo";
							postulanteLog.Valor = Postulante.InformacionPostulante.CantidadHijo.ToString();
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "CantidadHijo";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					if (postulante.UrlPerfilFacebook != Postulante.InformacionPostulante.UrlPerfilFacebook)
					{
						if (Postulante.InformacionPostulante.UrlPerfilFacebook != null)
						{
							postulanteLog.Clave = "UrlPerfilFacebook";
							postulanteLog.Valor = Postulante.InformacionPostulante.UrlPerfilFacebook;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "UrlPerfilFacebook";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					if (postulante.UrlPerfilLinkedin != Postulante.InformacionPostulante.UrlPerfilLinkedin)
					{
						if (Postulante.InformacionPostulante.UrlPerfilLinkedin != null)
						{
							postulanteLog.Clave = "UrlPerfilLinkedin";
							postulanteLog.Valor = Postulante.InformacionPostulante.UrlPerfilLinkedin;
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
						else
						{
							postulanteLog.Clave = "UrlPerfilLinkedin";
							postulanteLog.Valor = "";
							_repPostulanteLog.Insert(postulanteLog);
							postulanteLog.Id = 0;
						}
					}
					// Fin
					postulante.Nombre = Postulante.InformacionPostulante.Nombre;
					postulante.ApellidoPaterno = Postulante.InformacionPostulante.ApellidoPaterno;
					postulante.ApellidoMaterno = Postulante.InformacionPostulante.ApellidoMaterno;
					postulante.IdTipoDocumento = Postulante.InformacionPostulante.IdTipoDocumento;
					postulante.NroDocumento = Postulante.InformacionPostulante.NroDocumento;
					postulante.IdPais = Postulante.InformacionPostulante.IdPais;
					postulante.IdCiudad = Postulante.InformacionPostulante.IdCiudad;
					postulante.Celular = Postulante.InformacionPostulante.Celular;
					postulante.Email = Postulante.InformacionPostulante.Email;
					postulante.FechaNacimiento = Postulante.InformacionPostulante.FechaNacimiento;
					postulante.IdSexo = Postulante.InformacionPostulante.IdSexo;
					postulante.CantidadHijo = Postulante.InformacionPostulante.CantidadHijo;
					postulante.UrlPerfilFacebook = Postulante.InformacionPostulante.UrlPerfilFacebook;
					postulante.UrlPerfilLinkedin = Postulante.InformacionPostulante.UrlPerfilLinkedin;
					postulante.UsuarioModificacion = Postulante.Usuario;
					postulante.FechaModificacion = DateTime.Now;
					postulante.EsProcesoAnterior = null;
					_repPostulante.Update(postulante);

					if (Postulante.InformacionPostulante.IdProcesoSeleccion.HasValue)
					{
						var procesos = _repPostulanteProcesoSeleccion.ObtenerProcesoSeleccionInscrito(postulante.Id);
						foreach (var item in procesos)
						{
							if (item.IdProcesoSeleccion != Postulante.InformacionPostulante.IdProcesoSeleccion.Value)
							{
								var pps = _repPostulanteProcesoSeleccion.FirstBy(x => x.IdPostulante == postulante.Id && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
								_repPostulanteProcesoSeleccion.EliminarProcesoSeleccionAsociado(postulante.Id, item.IdProcesoSeleccion);
								_repPostulanteProcesoSeleccion.Delete(pps.Id, Postulante.Usuario);
							}
						}
						var ppss = _repPostulanteProcesoSeleccion.FirstBy(x => x.IdPostulante == postulante.Id && x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion.Value);
						if (ppss == null)
						{

							if (Postulante.InformacionPostulante.IdConvocatoriaPersonal != null)
							{
								postulanteLog.Clave = "CodigoConvocatoria";
								postulanteLog.Valor = _repConvocatoriaPersonal.FirstById((int)Postulante.InformacionPostulante.IdConvocatoriaPersonal).Nombre;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "CodigoConvocatoria";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdEstadoProcesoSeleccion != null)
							{
								postulanteLog.Clave = "EstadoProcesoSeleccion";
								postulanteLog.Valor = _repPostulante.ObtenerEtapaProceso((int)Postulante.InformacionPostulante.IdEstadoProcesoSeleccion).Nombre;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "EstadoProcesoSeleccion";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa != null)
							{
								postulanteLog.Clave = "EtapaProcesoSeleccion";
								postulanteLog.Valor = _repProcesoSeleccionEtapa.FirstById((int)Postulante.InformacionPostulante.IdProcesoSeleccionEtapa).Nombre;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "EtapaProcesoSeleccion";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion != null)
							{
								postulanteLog.Clave = "EstadoEtapaProcesoSeleccion";
								postulanteLog.Valor = _repEstadoEtapaProcesoSeleccion.FirstById((int)Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion).Nombre;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "EstadoEtapaProcesoSeleccion";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdPostulanteNivelPotencial != null)
							{
								postulanteLog.Clave = "PotencialProcesoSeleccion";
								postulanteLog.Valor = _repPostulanteNivelPotencial.FirstById((int)Postulante.InformacionPostulante.IdPostulanteNivelPotencial).Nombre;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "PotencialProcesoSeleccion";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdProveedor != null)
							{
								postulanteLog.Clave = "ProveedorProcesoSeleccion";
								postulanteLog.Valor = _repProveedor.FirstById((int)Postulante.InformacionPostulante.IdProveedor).RazonSocial;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "ProveedorProcesoSeleccion";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							if (Postulante.InformacionPostulante.IdPersonal_OperadorProceso != null)
							{
								postulanteLog.Clave = "Operador";
								var pers = _repPersonal.FirstById((int)Postulante.InformacionPostulante.IdPersonal_OperadorProceso);
								postulanteLog.Valor = pers.Nombres + " " + pers.Apellidos;
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}
							else
							{
								postulanteLog.Clave = "Operador";
								postulanteLog.Valor = "";
								_repPostulanteLog.Insert(postulanteLog);
								postulanteLog.Id = 0;
							}

							PostulanteProcesoSeleccionBO postulanteProcesoSeleccion = new PostulanteProcesoSeleccionBO
							{
								IdPostulante = postulante.Id,
								IdProcesoSeleccion = Postulante.InformacionPostulante.IdProcesoSeleccion.Value,
								FechaRegistro = DateTime.Now,
								Estado = true,
								UsuarioCreacion = Postulante.Usuario,
								UsuarioModificacion = Postulante.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								IdEstadoProcesoSeleccion = Postulante.InformacionPostulante.IdEstadoProcesoSeleccion,
								IdPostulanteNivelPotencial = Postulante.InformacionPostulante.IdPostulanteNivelPotencial,
								IdProveedor = Postulante.InformacionPostulante.IdProveedor,
								IdPersonalOperadorProceso = Postulante.InformacionPostulante.IdPersonal_OperadorProceso,
								IdConvocatoriaPersonal = Postulante.InformacionPostulante.IdConvocatoriaPersonal
							};
							var res = _repPostulanteProcesoSeleccion.Insert(postulanteProcesoSeleccion);
							if (res)
							{
								var postulacion = _repExamenAsignado.FirstBy(x => x.IdPostulante == postulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulanteProcesoSeleccion.IdProcesoSeleccion);
								if (postulacion == null)
								{
									//var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(postulanteProcesoSeleccion.IdProcesoSeleccion);
									//foreach (var item in examenPorProceso)
									//{
									//	ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
									//	examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
									//	examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
									//	examenAsignado.IdExamen = item.IdExamen;
									//	examenAsignado.EstadoExamen = false;
									//	examenAsignado.Estado = true;
									//	examenAsignado.UsuarioCreacion = Postulante.Usuario;
									//	examenAsignado.UsuarioModificacion = Postulante.Usuario;
									//	examenAsignado.FechaCreacion = DateTime.Now;
									//	examenAsignado.FechaModificacion = DateTime.Now;
									//	_repExamenAsignado.Insert(examenAsignado);
									//}
									var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
									var examenEvaluadorPorProceso = _repExamenAsignadoEvaluador.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
									foreach (var item in examenPorProceso)
									{
										ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
										examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
										examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
										examenAsignado.IdExamen = item.IdExamen;
										examenAsignado.EstadoExamen = false;
										examenAsignado.Estado = true;
										examenAsignado.UsuarioCreacion = Postulante.Usuario;
										examenAsignado.UsuarioModificacion = Postulante.Usuario;
										examenAsignado.FechaCreacion = DateTime.Now;
										examenAsignado.FechaModificacion = DateTime.Now;
										_repExamenAsignado.Insert(examenAsignado);
									}
									foreach (var item in examenEvaluadorPorProceso)
									{
										ExamenAsignadoEvaluadorBO examenAsignadoEvaluador = new ExamenAsignadoEvaluadorBO();
										examenAsignadoEvaluador.IdPersonal = personal.Id;
										examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
										examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
										examenAsignadoEvaluador.IdExamen = item.IdExamen;
										examenAsignadoEvaluador.EstadoExamen = false;
										examenAsignadoEvaluador.Estado = true;
										examenAsignadoEvaluador.UsuarioCreacion = Postulante.Usuario;
										examenAsignadoEvaluador.UsuarioModificacion = Postulante.Usuario;
										examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
										examenAsignadoEvaluador.FechaModificacion = DateTime.Now;
										_repExamenAsignadoEvaluador.Insert(examenAsignadoEvaluador);
									}
								}
							}
							else
							{
								return BadRequest(ModelState);
							}
							var EtapasProceso = _repProcesoSeleccionEtapa.GetBy(x => x.Estado == true && x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion).ToList();
							var etapaCalificada = _reEtapaCalificacion.ObtenerEtapaCalificadaProcesoSeleccion(Postulante.InformacionPostulante.Id, Postulante.InformacionPostulante.IdProcesoSeleccion.Value).ToList();
							foreach (var item in etapaCalificada)
							{
								_reEtapaCalificacion.Delete(item.IdEtapaProcesoSeleccionCalificado, Postulante.Usuario);
							}
							foreach (var item in EtapasProceso)
							{
								EtapaProcesoSeleccionCalificadoBO etapaCalificacion = new EtapaProcesoSeleccionCalificadoBO();
								etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
								etapaCalificacion.IdPostulante = Postulante.InformacionPostulante.Id;
								if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item.Id)
								{
									etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
									etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion;
									etapaCalificacion.EsEtapaActual = true;
								}
								else
								{
									etapaCalificacion.EsEtapaAprobada = false;
									etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
									etapaCalificacion.EsEtapaActual = false;
								}
								etapaCalificacion.EsContactado = false;
								etapaCalificacion.Estado = true;
								etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
								etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
								etapaCalificacion.FechaCreacion = DateTime.Now;
								etapaCalificacion.FechaModificacion = DateTime.Now;
								_reEtapaCalificacion.Insert(etapaCalificacion);
							}
						}
						else
						{
							if (ppss.IdConvocatoriaPersonal != Postulante.InformacionPostulante.IdConvocatoriaPersonal)
							{
								if (Postulante.InformacionPostulante.IdConvocatoriaPersonal != null)
								{
									postulanteLog.Clave = "CodigoConvocatoria";
									postulanteLog.Valor = _repConvocatoriaPersonal.FirstById((int)Postulante.InformacionPostulante.IdConvocatoriaPersonal).Nombre;
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
								else
								{
									postulanteLog.Clave = "CodigoConvocatoria";
									postulanteLog.Valor = "";
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
							}

							if (ppss.IdEstadoProcesoSeleccion != Postulante.InformacionPostulante.IdEstadoProcesoSeleccion)
							{
								if (Postulante.InformacionPostulante.IdEstadoProcesoSeleccion != null)
								{
									postulanteLog.Clave = "EstadoProcesoSeleccion";
									postulanteLog.Valor = _repPostulante.ObtenerEtapaProceso((int)Postulante.InformacionPostulante.IdEstadoProcesoSeleccion).Nombre;
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
								else
								{
									postulanteLog.Clave = "EstadoProcesoSeleccion";
									postulanteLog.Valor = "";
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
							}

							if (ppss.IdPostulanteNivelPotencial != Postulante.InformacionPostulante.IdPostulanteNivelPotencial)
							{
								if (Postulante.InformacionPostulante.IdPostulanteNivelPotencial != null)
								{
									postulanteLog.Clave = "PotencialProcesoSeleccion";
									postulanteLog.Valor = _repPostulanteNivelPotencial.FirstById((int)Postulante.InformacionPostulante.IdPostulanteNivelPotencial).Nombre;
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
								else
								{
									postulanteLog.Clave = "PotencialProcesoSeleccion";
									postulanteLog.Valor = "";
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
							}

							if (ppss.IdProveedor != Postulante.InformacionPostulante.IdProveedor)
							{
								if (Postulante.InformacionPostulante.IdProveedor != null)
								{
									postulanteLog.Clave = "ProveedorProcesoSeleccion";
									postulanteLog.Valor = _repProveedor.FirstById((int)Postulante.InformacionPostulante.IdProveedor).RazonSocial;
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
								else
								{
									postulanteLog.Clave = "ProveedorProcesoSeleccion";
									postulanteLog.Valor = "";
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
							}

							if (ppss.IdPersonalOperadorProceso != Postulante.InformacionPostulante.IdPersonal_OperadorProceso)
							{
								if (Postulante.InformacionPostulante.IdPersonal_OperadorProceso != null)
								{
									postulanteLog.Clave = "Operador";
									var pers = _repPersonal.FirstById((int)Postulante.InformacionPostulante.IdPersonal_OperadorProceso);
									postulanteLog.Valor = pers.Nombres + " " + pers.Apellidos;
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
								else
								{
									postulanteLog.Clave = "Operador";
									postulanteLog.Valor = "";
									_repPostulanteLog.Insert(postulanteLog);
									postulanteLog.Id = 0;
								}
							}

							ppss.IdEstadoProcesoSeleccion = Postulante.InformacionPostulante.IdEstadoProcesoSeleccion;
							//Agregadi oir Britsel
							ppss.IdPostulanteNivelPotencial = Postulante.InformacionPostulante.IdPostulanteNivelPotencial;
							ppss.IdProveedor = Postulante.InformacionPostulante.IdProveedor;
							ppss.IdPersonalOperadorProceso = Postulante.InformacionPostulante.IdPersonal_OperadorProceso;
							ppss.IdConvocatoriaPersonal = Postulante.InformacionPostulante.IdConvocatoriaPersonal;

							ppss.UsuarioModificacion = Postulante.Usuario;
							ppss.FechaModificacion = DateTime.Now;
							_repPostulanteProcesoSeleccion.Update(ppss);

							var EtapasProceso = _repProcesoSeleccionEtapa.GetBy(x => x.Estado == true && x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion).Select(x => x.Id).ToList();
							var etapaCalificada = _reEtapaCalificacion.ObtenerEtapaCalificadaProcesoSeleccion(Postulante.InformacionPostulante.Id, Postulante.InformacionPostulante.IdProcesoSeleccion.Value).ToList();
							var listaEtapasCalificadas = etapaCalificada.Select(x => x.IdProcesoSeleccionEtapa).ToList();
							var flag1 = true;
							var flag2 = true;
							if (etapaCalificada.Count == 0 || etapaCalificada == null)
							{
								foreach (var item in EtapasProceso)
								{
									EtapaProcesoSeleccionCalificadoBO etapaCalificacion = new EtapaProcesoSeleccionCalificadoBO();
									etapaCalificacion.IdProcesoSeleccionEtapa = item;
									etapaCalificacion.IdPostulante = Postulante.InformacionPostulante.Id;
									if (flag1)
									{
										if (etapaCalificacion.IdProcesoSeleccionEtapa != Postulante.InformacionPostulante.IdProcesoSeleccionEtapa)
										{
											if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa != null)
											{
												postulanteLog.Clave = "EtapaProcesoSeleccion";
												postulanteLog.Valor = _repProcesoSeleccionEtapa.FirstById((int)Postulante.InformacionPostulante.IdProcesoSeleccionEtapa).Nombre;
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											else
											{
												postulanteLog.Clave = "EtapaProcesoSeleccion";
												postulanteLog.Valor = "";
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											flag1 = false;
										}

									}
									if (flag2)
									{
										if (etapaCalificacion.IdEstadoEtapaProcesoSeleccion != Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion)
										{
											if (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion != null)
											{
												postulanteLog.Clave = "EstadoEtapaProcesoSeleccion";
												postulanteLog.Valor = _repEstadoEtapaProcesoSeleccion.FirstById((int)Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion).Nombre;
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											else
											{
												postulanteLog.Clave = "EstadoEtapaProcesoSeleccion";
												postulanteLog.Valor = "";
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											flag2 = false;
										}

									}
									if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item)
									{
										etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
										etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion;
										etapaCalificacion.EsEtapaActual = true;
									}
									else
									{
										etapaCalificacion.EsEtapaAprobada = false;
										etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
										etapaCalificacion.EsEtapaActual = false;
									}
									etapaCalificacion.EsContactado = false;
									etapaCalificacion.Estado = true;
									etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
									etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
									etapaCalificacion.FechaCreacion = DateTime.Now;
									etapaCalificacion.FechaModificacion = DateTime.Now;
									_reEtapaCalificacion.Insert(etapaCalificacion);
								}
							}
							else
							{
								foreach (var item in etapaCalificada)
								{
									if (!EtapasProceso.Contains(item.IdProcesoSeleccionEtapa))
									{
										_reEtapaCalificacion.Delete(item.IdEtapaProcesoSeleccionCalificado, Postulante.Usuario);
									}
								}

								foreach (var item in EtapasProceso)
								{
									//si ya existe el registro en EtapaCalificada
									if (listaEtapasCalificadas.Contains(item))
									{
										EtapaProcesoSeleccionCalificadoBO etapaCalificacion = _reEtapaCalificacion.FirstBy(x => x.IdPostulante == Postulante.InformacionPostulante.Id && x.IdProcesoSeleccionEtapa == item);

										if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item)
										{

											if (etapaCalificacion.IdEstadoEtapaProcesoSeleccion != Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion)
											{
												postulanteLog.Clave = "EstadoEtapaProcesoSeleccion";
												postulanteLog.Valor = _repEstadoEtapaProcesoSeleccion.FirstById((int)Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion).Nombre;
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
											etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion; ;
											if (etapaCalificacion.EsEtapaActual != true)
											{
												postulanteLog.Clave = "EtapaProcesoSeleccion";
												postulanteLog.Valor = _repProcesoSeleccionEtapa.FirstById((int)Postulante.InformacionPostulante.IdProcesoSeleccionEtapa).Nombre;
												_repPostulanteLog.Insert(postulanteLog);
												postulanteLog.Id = 0;
											}
											etapaCalificacion.EsEtapaActual = true;

										}
										else
										{
											etapaCalificacion.EsEtapaActual = false;
										}
										etapaCalificacion.EsContactado = false;
										etapaCalificacion.Estado = true;
										etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
										etapaCalificacion.FechaModificacion = DateTime.Now;
										_reEtapaCalificacion.Update(etapaCalificacion);
									}
									else
									{//Si no existe el registro de esa etapa se crea uno nuevo

										EtapaProcesoSeleccionCalificadoBO etapaCalificacion = new EtapaProcesoSeleccionCalificadoBO();
										etapaCalificacion.IdProcesoSeleccionEtapa = item;
										etapaCalificacion.IdPostulante = Postulante.InformacionPostulante.Id;
										if (Postulante.InformacionPostulante.IdProcesoSeleccionEtapa == item)
										{
											etapaCalificacion.EsEtapaAprobada = Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
											etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == null || Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.InformacionPostulante.IdEstadoEtapaProcesoSeleccion;
											etapaCalificacion.EsEtapaActual = true;
										}
										else
										{
											etapaCalificacion.EsEtapaAprobada = false;
											etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
											etapaCalificacion.EsEtapaActual = false;
										}
										etapaCalificacion.EsContactado = false;
										etapaCalificacion.Estado = true;
										etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
										etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
										etapaCalificacion.FechaCreacion = DateTime.Now;
										etapaCalificacion.FechaModificacion = DateTime.Now;
										_reEtapaCalificacion.Insert(etapaCalificacion);
									}
								}
							}


						}
					}
				}
				else
				{
					return BadRequest("El postulante no existe o ya fue eliminado");
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPostulante([FromBody] EliminarDTO Postulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);

				if (_repPostulante.Exist(Postulante.Id))
				{
					var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.ObtenerProcesoSeleccionInscrito(Postulante.Id);
					if (postulanteProcesoSeleccion.Count > 0)
					{
						foreach (var item in postulanteProcesoSeleccion)
						{
							var tokenPostulanteProcesoSeleccion = _repTokenPostulanteProcesoSeleccion.ObtenerTokenPorPostulanteProcesoSeleccion(item.Id);
							if (tokenPostulanteProcesoSeleccion.Count > 0)
							{
								foreach (var item2 in tokenPostulanteProcesoSeleccion)
								{
									_repTokenPostulanteProcesoSeleccion.Delete(item2.Id, Postulante.NombreUsuario);
								}
							}
							_repPostulanteProcesoSeleccion.Delete(item.Id, Postulante.NombreUsuario);
						}

					}
					_repPostulante.Delete(Postulante.Id, Postulante.NombreUsuario);
				}
				else
				{
					return BadRequest("El postulante no existe o ya fue eliminado");
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerProcesoSeleccionPorPostulante([FromBody] IdDTO Postulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteProcesoSeleccionRepositorio _repPostulante = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				var procesosInscritos = _repPostulante.ObtenerProcesoSeleccionInscrito(Postulante.Id);
				return Ok(procesosInscritos);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult IngresarProcesoSeleccionEvaluaciones([FromBody] PostulanteProcesoSeleccionDTO PostulanteProcesoSeleccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionBO postulanteProcesoSeleccion = new PostulanteProcesoSeleccionBO()
				{
					IdPostulante = PostulanteProcesoSeleccion.IdPostulante,
					IdProcesoSeleccion = PostulanteProcesoSeleccion.IdProcesoSeleccion,
					FechaRegistro = DateTime.Now,
					Estado = true,
					UsuarioCreacion = PostulanteProcesoSeleccion.Usuario,
					UsuarioModificacion = PostulanteProcesoSeleccion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};

				var res = _repPostulanteProcesoSeleccion.Insert(postulanteProcesoSeleccion);

				if (res)
				{
					var postulacion = _repExamenAsignado.FirstBy(x => x.IdPostulante == PostulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == PostulanteProcesoSeleccion.IdProcesoSeleccion);
					if (postulacion == null)
					{
						var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(PostulanteProcesoSeleccion.IdProcesoSeleccion);
						foreach (var item in examenPorProceso)
						{
							ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
							examenAsignado.IdPostulante = PostulanteProcesoSeleccion.IdPostulante;
							examenAsignado.IdProcesoSeleccion = PostulanteProcesoSeleccion.IdProcesoSeleccion;
							examenAsignado.IdExamen = item.IdExamen;
							examenAsignado.EstadoExamen = false;
							examenAsignado.Estado = true;
							examenAsignado.UsuarioCreacion = PostulanteProcesoSeleccion.Usuario;
							examenAsignado.UsuarioModificacion = PostulanteProcesoSeleccion.Usuario;
							examenAsignado.FechaCreacion = DateTime.Now;
							examenAsignado.FechaModificacion = DateTime.Now;
							_repExamenAsignado.Insert(examenAsignado);
						}
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarProcesoSeleccionEvaluaciones([FromBody] PostulanteProcesoSeleccionDTO PostulanteProcesoSeleccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				var postulacionAnterior = _repPostulanteProcesoSeleccion.ObtenerProcesoSeleccionInscritoPorId(PostulanteProcesoSeleccion.Id);
				var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.FirstById(PostulanteProcesoSeleccion.Id);

				postulanteProcesoSeleccion.IdProcesoSeleccion = PostulanteProcesoSeleccion.IdProcesoSeleccion;
				postulanteProcesoSeleccion.UsuarioModificacion = PostulanteProcesoSeleccion.Usuario;
				postulanteProcesoSeleccion.FechaModificacion = DateTime.Now;

				var res = _repPostulanteProcesoSeleccion.Update(postulanteProcesoSeleccion);

				if (res)
				{
					var postulacion = _repExamenAsignado.GetBy(x => x.IdPostulante == PostulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulacionAnterior.IdProcesoSeleccion).ToList();
					foreach (var item in postulacion)
					{
						_repExamenAsignado.Delete(item.Id, PostulanteProcesoSeleccion.Usuario);
					}
					var postulacionNueva = _repExamenAsignado.FirstBy(x => x.IdPostulante == PostulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == PostulanteProcesoSeleccion.IdProcesoSeleccion);
					if (postulacionNueva == null)
					{
						var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccion(PostulanteProcesoSeleccion.IdProcesoSeleccion);
						foreach (var item in examenPorProceso)
						{
							ExamenAsignadoBO examenAsignado = new ExamenAsignadoBO();
							examenAsignado.IdPostulante = PostulanteProcesoSeleccion.IdPostulante;
							examenAsignado.IdProcesoSeleccion = PostulanteProcesoSeleccion.IdProcesoSeleccion;
							examenAsignado.IdExamen = item.IdExamen;
							examenAsignado.EstadoExamen = true;
							examenAsignado.Estado = true;
							examenAsignado.UsuarioCreacion = PostulanteProcesoSeleccion.Usuario;
							examenAsignado.UsuarioModificacion = PostulanteProcesoSeleccion.Usuario;
							examenAsignado.FechaCreacion = DateTime.Now;
							examenAsignado.FechaModificacion = DateTime.Now;
							_repExamenAsignado.Insert(examenAsignado);
						}
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarProcesoSeleccionEvaluaciones([FromBody] PostulanteProcesoSeleccionDTO PostulanteProcesoSeleccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				var postulacionAnterior = _repPostulanteProcesoSeleccion.ObtenerProcesoSeleccionInscritoPorId(PostulanteProcesoSeleccion.Id);
				var res = _repPostulanteProcesoSeleccion.Delete(PostulanteProcesoSeleccion.Id, PostulanteProcesoSeleccion.Usuario);
				if (res)
				{
					var postulacion = _repExamenAsignado.GetBy(x => x.IdPostulante == PostulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulacionAnterior.IdProcesoSeleccion).ToList();
					foreach (var item in postulacion)
					{
						_repExamenAsignado.Delete(item.Id, PostulanteProcesoSeleccion.Usuario);
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarToken([FromBody] PostulanteProcesoSeleccionIdDTO PostulanteProcesoSeleccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
				ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
				TokenPostulanteProcesoSeleccionBO tokenPostulanteProceso = new TokenPostulanteProcesoSeleccionBO();
				var token = tokenPostulanteProceso.GenerarClave(8);
				var tokenHash = Crypto.HashPassword(token);
				var tokenPostulante = _repTokenPostulanteProcesoSeleccion.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(PostulanteProcesoSeleccion.Id);
				if (tokenPostulante != null)
				{
					var tokenPostulanteProcesoSeleccion = _repTokenPostulanteProcesoSeleccion.FirstById(tokenPostulante.Id);
					tokenPostulanteProcesoSeleccion.Activo = false;
					tokenPostulanteProcesoSeleccion.UsuarioModificacion = PostulanteProcesoSeleccion.Usuario;
					tokenPostulanteProcesoSeleccion.FechaModificacion = DateTime.Now;
					_repTokenPostulanteProcesoSeleccion.Update(tokenPostulanteProcesoSeleccion);
				}
				TokenPostulanteProcesoSeleccionBO tokenNueva = new TokenPostulanteProcesoSeleccionBO()
				{
					IdPostulanteProcesoSeleccion = PostulanteProcesoSeleccion.Id,
					Token = token,
					TokenHash = tokenHash,
					GuidAccess = Guid.NewGuid(),
					Activo = true,
					Estado = true,
					UsuarioCreacion = PostulanteProcesoSeleccion.Usuario,
					UsuarioModificacion = PostulanteProcesoSeleccion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};

				_repTokenPostulanteProcesoSeleccion.Insert(tokenNueva);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EnviarMensajeEmailPostulante([FromBody] PostulanteProcesoSeleccionIdDTO PostulanteProcesoSeleccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

				var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.ObtenerPostulanteProcesoSeleccion(PostulanteProcesoSeleccion.Id);
				var emailPersonal = _repAspNetUsers.ObtenerEmailPorNombreUsuario(PostulanteProcesoSeleccion.Usuario);

				if (postulanteProcesoSeleccion.Token == null)
				{
					TokenPostulanteProcesoSeleccionBO tokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionBO();
					var token = tokenPostulanteProcesoSeleccion.GenerarClave(8);
					var tokenHash = Crypto.HashPassword(token);
					var tokenPostulante = _repTokenPostulanteProcesoSeleccion.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(PostulanteProcesoSeleccion.Id);

					TokenPostulanteProcesoSeleccionBO tokenNueva = new TokenPostulanteProcesoSeleccionBO()
					{
						IdPostulanteProcesoSeleccion = PostulanteProcesoSeleccion.Id,
						Token = token,
						TokenHash = tokenHash,
						GuidAccess = Guid.NewGuid(),
						Activo = true,
						Estado = true,
						UsuarioCreacion = PostulanteProcesoSeleccion.Usuario,
						UsuarioModificacion = PostulanteProcesoSeleccion.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now,
						FechaEnvioAccesos = DateTime.Now
					};

					_repTokenPostulanteProcesoSeleccion.Insert(tokenNueva);
					postulanteProcesoSeleccion.Token = token;
					postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
				}
				var mensaje = "<p style=‘text - align: justify;’><strong style=‘text - align: justify; font - family: Calibri, sans - serif; font - size: 14.6667px;’>Estimado(a) {T_Postulante.Nombre}</strong> <span style=‘text - align: justify;’>&nbsp;</span> <br /><br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’>Me es grato saludarle y a la vez le hago llegar los datos para acceder al proceso de selecci&oacute;n al que usted se inscribio:</span></p><p style=‘text - align: center;’><strong style=‘font - family: Calibri, sans - serif; font - size: 14.6667px; text - align: center;’>{T_ProcesoSeleccion.Nombre}</strong></p><p style=‘margin - left: 30px; text - align: justify;’><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Usuario:</strong> {T_Postulante.Dni} </span> <br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Contrase&ntilde;a:</strong> {T_Postulante.Clave} </span> <br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Link: </strong>{Link.UrlProcesoSeleccion} </span></p><p style=‘text - align: justify;’><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’>Recuerde que los accesos enviados caducan dentro de 72 hrs o al primer inicio de sesi&oacute;n, es importante continuar el proceso sin salir de la p&aacute;gina ya que al salir estos accesos no tendr&aacute;n validez. En caso ya no pueda acceder al proceso de selecci&oacute;n favor de responder el correo solicitandolos.</span></p><p style=‘text - align: justify;’>Saludos cordiales.</p>";
				var asunto = "{T_Postulante.Nombre}, acceda al proceso de selección - BSG INSTITUTE";
				var url = "https://bsginstitute.com/procesoseleccion/acceso?guid=" + postulanteProcesoSeleccion.GuidAccess;

				mensaje = mensaje.Replace("{T_Postulante.Nombre}", postulanteProcesoSeleccion.Postulante);
				mensaje = mensaje.Replace("{T_ProcesoSeleccion.Nombre}", postulanteProcesoSeleccion.ProcesoSeleccion);
				mensaje = mensaje.Replace("{T_Postulante.Dni}", postulanteProcesoSeleccion.Dni);
				mensaje = mensaje.Replace("{T_Postulante.Clave}", postulanteProcesoSeleccion.Token);
				mensaje = mensaje.Replace("{Link.UrlProcesoSeleccion}", url);

				asunto = asunto.Replace("{T_Postulante.Nombre}", postulanteProcesoSeleccion.Postulante);

				TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
				{
					Sender = emailPersonal,
					Recipient = postulanteProcesoSeleccion.Email,
					Subject = asunto,
					Message = mensaje,
					AttachedFiles = null,
					Bcc = emailPersonal,
					RemitenteC = PostulanteProcesoSeleccion.Usuario
				};
				var mailServie = new TMK_MailServiceImpl();

				mailServie.SetData(mailDataPersonalizado);
				mailServie.SendMessageTask();

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		private void EnvioAutomaticoPlantilla(WhatsAppResultadoPostulanteDTO MensajePostulante)
		{

			bool banderaLogin = false;
			string _tokenComunicacion = string.Empty;

			WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
			{
				Id = 0,
				WaTo = MensajePostulante.Celular,
				WaType = "hsm",
				WaTypeMensaje = 8,
				WaRecipientType = "hsm",
				WaBody = MensajePostulante.Plantilla,
				datosPlantillaWhatsApp = new System.Collections.Generic.List<datoPlantillaWhatsApp>()
			};

			try
			{
				ServicePointManager.ServerCertificateValidationCallback =
				delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{
					return true;
				};

				WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
				WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

				var _credencialesHost = _repCredenciales.ObtenerCredencialHost(MensajePostulante.IdCodigoPais);
				//personal debe tener accesos
				var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(MensajePostulante.IdPersonal, MensajePostulante.IdCodigoPais);

				string urlToPost = _credencialesHost.UrlWhatsApp;

				string resultado = string.Empty, _waType = string.Empty;

				//TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

				if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
				{
					string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

					var userLogin = _repTokenUsuario.CredencialUsuarioLogin(MensajePostulante.IdPersonal);

					var client = new RestClient(urlToPostUsuario);
					var request = new RestSharp.RestRequest(Method.POST);
					request.AddHeader("cache-control", "no-cache");
					request.AddHeader("Content-Length", "");
					request.AddHeader("Accept-Encoding", "gzip, deflate");
					request.AddHeader("Host", _credencialesHost.IpHost);
					request.AddHeader("Cache-Control", "no-cache");
					request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
					request.AddHeader("Content-Type", "application/json");

				}
				else
				{
					_tokenComunicacion = tokenValida.UserAuthToken;
					banderaLogin = true;
				}

				if (banderaLogin)
				{
					switch (DTO.WaType.ToLower())
					{
						case "text":
							urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
							_waType = "text";

							MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

							_mensajeTexto.to = DTO.WaTo;
							_mensajeTexto.type = DTO.WaType;
							_mensajeTexto.recipient_type = DTO.WaRecipientType;
							_mensajeTexto.text = new text();

							_mensajeTexto.text.body = DTO.WaBody;

							using (WebClient client = new WebClient())
							{
								//client.Encoding = Encoding.UTF8;
								var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
								var serializer = new JavaScriptSerializer();

								var serializedResult = serializer.Serialize(_mensajeTexto);
								string myParameters = serializedResult;
								client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
								client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
								client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
								client.Headers[HttpRequestHeader.ContentType] = "application/json";
								resultado = client.UploadString(urlToPost, myParameters);
							}

							break;
						case "hsm":
							urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
							_waType = "hsm";

							MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

							_mensajePlantilla.to = DTO.WaTo;
							_mensajePlantilla.type = DTO.WaType;
							_mensajePlantilla.hsm = new hsm();

							_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
							_mensajePlantilla.hsm.element_name = DTO.WaBody;

							_mensajePlantilla.hsm.language = new language();
							_mensajePlantilla.hsm.language.policy = "deterministic";
							_mensajePlantilla.hsm.language.code = "es";

							if (DTO.datosPlantillaWhatsApp != null)
							{
								_mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
								foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
								{
									localizable_params _dato = new localizable_params();
									_dato.@default = listaDatos.texto;

									_mensajePlantilla.hsm.localizable_params.Add(_dato);
								}
							}

							using (WebClient client = new WebClient())
							{
								client.Encoding = Encoding.UTF8;
								var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
								var serializer = new JavaScriptSerializer();

								var serializedResult = serializer.Serialize(_mensajePlantilla);
								string myParameters = serializedResult;
								client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
								client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
								client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
								client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
								resultado = client.UploadString(urlToPost, myParameters);
							}

							break;
					}
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			System.Threading.Thread.Sleep(5000);



		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ImportarExcel([FromForm] IFormFile files)
		{
			var ListaPostulante = new List<PostulanteImportadoDTO>();
			var ListaPostulanteRepetido = new List<PostulanteImportadoDTO>();
			CsvFile file = new CsvFile();
			try
			{
				integraDBContext integraDB = new integraDBContext();
				CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(integraDB);
				PostulanteRepositorio postulanteRep = new PostulanteRepositorio();
				int index = 0;
				int NregistrosNuevo = 0;
				int NregistrosRepetido = 0;
				StreamReader sw = new StreamReader(files.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));

				using (var cvs = new CsvReader(sw))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						index++;
						PostulanteImportadoDTO postulante = new PostulanteImportadoDTO();
						bool ExisteDNI = postulanteRep.Exist(x => x.NroDocumento.Trim().Equals(cvs.GetField<string>("NroDocumento").Trim()));
						bool ExisteEmailApellido = postulanteRep.Exist(x => (x.ApellidoPaterno.ToLower().Equals(cvs.GetField<string>("ApellidoPaterno").ToLower()) && x.Email.ToLower().Equals(cvs.GetField<string>("Email").ToLower())) || (x.Email.ToLower().Equals(cvs.GetField<string>("Email").ToLower())));
						if (ExisteDNI || ExisteEmailApellido)
						{
							NregistrosRepetido += 2;
							postulante.Nombre = cvs.GetField<string>("Nombre");
							postulante.ApellidoPaterno = cvs.GetField<string>("ApellidoPaterno");
							postulante.ApellidoMaterno = cvs.GetField<string>("ApellidoMaterno");
							postulante.IdTipoDocumento = cvs.GetField<int?>("IdTipoDocumento");
							postulante.NroDocumento = cvs.GetField<string>("NroDocumento");
							postulante.Celular = cvs.GetField<string>("Celular");
							postulante.Email = cvs.GetField<string>("Email");
							postulante.IdPais = cvs.GetField<int?>("IdPais");
							postulante.IdCiudad = cvs.GetField<int?>("IdCiudad");
							postulante.IdEstadoEtapaProcesoSeleccion = cvs.GetField<int?>("IdEstadoEtapa");
							postulante.IdPostulanteNivelPotencial = cvs.GetField<int?>("IdNivelPotencial");
							postulante.Origen = "Excel";

							ListaPostulanteRepetido.Add(postulante);

							var postulanteRepetido = postulanteRep.GetBy(x => x.NroDocumento.Trim().Equals(postulante.NroDocumento.Trim()) || (x.Email.ToLower().Equals(postulante.Email.ToLower()) && x.ApellidoPaterno.ToLower().Equals(postulante.ApellidoPaterno.ToLower()))).FirstOrDefault();
							postulante = new PostulanteImportadoDTO();
							postulante.Nombre = postulanteRepetido.Nombre;
							postulante.ApellidoPaterno = postulanteRepetido.ApellidoPaterno;
							postulante.ApellidoMaterno = postulanteRepetido.ApellidoMaterno;
							postulante.IdTipoDocumento = postulanteRepetido.IdTipoDocumento;
							postulante.NroDocumento = postulanteRepetido.NroDocumento;
							postulante.Celular = postulanteRepetido.Celular;
							postulante.Email = postulanteRepetido.Email;
							postulante.IdPais = postulanteRepetido.IdPais;
							postulante.IdCiudad = postulanteRepetido.IdCiudad;
							postulante.IdEstadoEtapaProcesoSeleccion = null;
							postulante.IdPostulanteNivelPotencial = null;
							postulante.Origen = "Integra";

							ListaPostulanteRepetido.Add(postulante);


						}
						else
						{
							NregistrosNuevo++;
							postulante.Nombre = cvs.GetField<string>("Nombre");
							postulante.ApellidoPaterno = cvs.GetField<string>("ApellidoPaterno");
							postulante.ApellidoMaterno = cvs.GetField<string>("ApellidoMaterno");
							postulante.IdTipoDocumento = cvs.GetField<int?>("IdTipoDocumento");
							postulante.NroDocumento = cvs.GetField<string>("NroDocumento");
							postulante.Celular = cvs.GetField<string>("Celular");
							postulante.Email = cvs.GetField<string>("Email");
							postulante.IdPais = cvs.GetField<int?>("IdPais");
							postulante.IdCiudad = cvs.GetField<int?>("IdCiudad");
							postulante.IdEstadoEtapaProcesoSeleccion = cvs.GetField<int?>("IdEstadoEtapa");
							postulante.IdPostulanteNivelPotencial = cvs.GetField<int?>("IdNivelPotencial");
							postulante.Origen = "NUEVO";

							var IdRespuestas = cvs.GetField<string>("IdFactorDesaprobatorio");

							if (IdRespuestas != null)
							{
								var x = IdRespuestas.Split("/");
								foreach (var idRespuesta in x)
								{
									if (!idRespuesta.Equals(""))
									{
										if (postulante.ListaRespuestaDesaprobatoria == null)
										{
											postulante.ListaRespuestaDesaprobatoria = new List<int>();
										}
										int y = Int32.Parse(idRespuesta);
										postulante.ListaRespuestaDesaprobatoria.Add(Int32.Parse(idRespuesta));
									}
								}
							}


							ListaPostulante.Add(postulante);
						}
					}
				}
				var Nregistros = index;
				return Ok(new { ListaPostulante, ListaPostulanteRepetido, NregistrosNuevo, NregistrosRepetido });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerProcesoSeleccionCombo()
		{
			try
			{
				ProcesoSeleccionRepositorio repProcesoSeleccionRep = new ProcesoSeleccionRepositorio();
				return Ok(repProcesoSeleccionRep.GetBy(x => x.Estado == true && x.Activo == true, x => new { x.Id, Nombre = x.Nombre }).ToList());
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPostulantePorImportacion(PostulanteProcesoSeleccionConsolidadoDTO lista)
		{
			try
			{
				var insertadoCorrecto = 0;
				var insertadosError = 0;
				ExamenAsignadoBO examenPostulanteBO = new ExamenAsignadoBO();
				List<PostulanteInformacionDTO> ListaInformacionPostulante = new List<PostulanteInformacionDTO>();
				foreach (var item in lista.listaPostulante)
				{
					PostulanteInformacionDTO postulante = new PostulanteInformacionDTO();
					postulante.Nombre = item.Nombre;
					postulante.ApellidoPaterno = item.ApellidoPaterno;
					postulante.ApellidoMaterno = item.ApellidoMaterno;
					postulante.NroDocumento = item.NroDocumento;
					postulante.Celular = item.Celular;
					postulante.Email = item.Email;
					postulante.IdTipoDocumento = item.IdTipoDocumento;
					postulante.IdPais = item.IdPais;
					postulante.IdCiudad = item.IdCiudad;
					postulante.IdProcesoSeleccion = lista.IdProcesoSeleccion;
					postulante.IdPostulanteNivelPotencial = item.IdPostulanteNivelPotencial;
					postulante.IdProveedor = lista.IdProveedor;
					postulante.IdPersonal_OperadorProceso = lista.IdPersonal_Operador;
					postulante.IdConvocatoriaPersonal = lista.IdConvocatoria;
					postulante.IdProcesoSeleccionEtapa = lista.IdEtapaProcesoSeleccion;
					postulante.IdEstadoEtapaProcesoSeleccion = item.IdEstadoEtapaProcesoSeleccion;
					postulante.ListaRespuestaDesaprobatoria = item.ListaRespuestaDesaprobatoria;
					ListaInformacionPostulante.Add(postulante);
				}

				foreach (var item in ListaInformacionPostulante)
				{
					PostulanteFormularioDTO usuarioPostulante = new PostulanteFormularioDTO();
					usuarioPostulante.Usuario = lista.Usuario;
					usuarioPostulante.InformacionPostulante = item;
					bool insertado = examenPostulanteBO.InsertarPostulanteNuevo(usuarioPostulante, _integraDBContext);
					if (insertado)
					{
						insertadoCorrecto++;
					}
					else
					{
						insertadosError++;
					}
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EnviarPlantillaEmailMasivo([FromBody] EnvioPlantillaPostulanteDTO Postulantes)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
				PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);

				var personalUsuario = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Postulantes.Usuario);
				var personal = _repPersonal.FirstById(personalUsuario.Id);
				foreach (var idPostulanteProcesoSeleccion in Postulantes.ListaIdPostulanteProcesoSeleccion)
				{
					var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.ObtenerPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);
					if (Postulantes.IdPlantilla == 1255)
					{
						if (postulanteProcesoSeleccion.Token == null)
						{
							TokenPostulanteProcesoSeleccionBO tokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionBO();
							var token = tokenPostulanteProcesoSeleccion.GenerarClave(8);
							var tokenHash = Crypto.HashPassword(token);
							var tokenPostulante = _repTokenPostulanteProcesoSeleccion.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

							TokenPostulanteProcesoSeleccionBO tokenNueva = new TokenPostulanteProcesoSeleccionBO()
							{
								IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
								Token = token,
								TokenHash = tokenHash,
								GuidAccess = Guid.NewGuid(),
								Activo = true,
								Estado = true,
								UsuarioCreacion = Postulantes.Usuario,
								UsuarioModificacion = Postulantes.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								FechaEnvioAccesos = DateTime.Now
							};

							_repTokenPostulanteProcesoSeleccion.Insert(tokenNueva);
							postulanteProcesoSeleccion.Token = token;
							postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
						}
					}


					var postulante = _repPostulante.FirstById(postulanteProcesoSeleccion.IdPostulante);
					if (idPostulanteProcesoSeleccion > 0 && idPostulanteProcesoSeleccion != null)
					{
						DateTime? fecha = null;
						if (Postulantes.Fecha.HasValue)
						{
							fecha = Postulantes.Fecha.Value.AddHours(-5);
						}
						var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
						{
							IdPlantilla = Postulantes.IdPlantilla,
							IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
							Personal = personal,
							FechaGP = fecha
						};
						_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccion();

						var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
						List<string> correosPersonalizadosCopiaOculta = new List<string>
						{
							personal.Email
						};

						List<string> correosPersonalizados = new List<string>{
							//proveedor.Email
							postulante.Email.Trim()
						};
						TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
						{
							Sender = personal.Email,
							Recipient = string.Join(",", correosPersonalizados.Distinct()),
							Subject = emailCalculado.Asunto,
							Message = emailCalculado.CuerpoHTML,
							Cc = "",
							Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
							AttachedFiles = emailCalculado.ListaArchivosAdjuntos
						};
						var mailServie = new TMK_MailServiceImpl();

						mailServie.SetData(mailDataPersonalizado);
						mailServie.SendMessageTask();

						var gmailCorreo = new GmailCorreoBO
						{
							IdEtiqueta = 1,//sent:1 , inbox:2
							Asunto = emailCalculado.Asunto,
							Fecha = DateTime.Now,
							EmailBody = emailCalculado.CuerpoHTML,
							Seen = false,
							Remitente = personal.Email,
							Cc = "",
							Bcc = "",
							Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
							IdPersonal = personal.Id,
							Estado = true,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							UsuarioCreacion = "SYSTEM",
							UsuarioModificacion = "SYSTEM",
							IdClasificacionPersona = 5
						};
						var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
						_repGmailCorreo.Insert(gmailCorreo);
					}
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EnviarMensajeWhatsAppPostulante([FromBody] EnvioPlantillaPostulanteDTO Postulantes)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
				PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
				PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
				TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
				List<string> Incorrectos = new List<string>();
				List<string> Incorrectos2 = new List<string>();
				var personalUsuario = _repIntegraAspNetUsers.ObtenerIdentidadUsusario(Postulantes.Usuario);
				var personal = _repPersonal.FirstById(personalUsuario.Id);
				foreach (var idPostulanteProcesoSeleccion in Postulantes.ListaIdPostulanteProcesoSeleccion)
				{
					var postulanteProcesoSeleccion = _repPostulanteProcesoSeleccion.ObtenerPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

					if (Postulantes.IdPlantilla == 1300)
					{
						if (postulanteProcesoSeleccion.Token == null)
						{
							TokenPostulanteProcesoSeleccionBO tokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionBO();
							var token = tokenPostulanteProcesoSeleccion.GenerarClave(8);
							var tokenHash = Crypto.HashPassword(token);
							var tokenPostulante = _repTokenPostulanteProcesoSeleccion.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

							TokenPostulanteProcesoSeleccionBO tokenNueva = new TokenPostulanteProcesoSeleccionBO()
							{
								IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
								Token = token,
								TokenHash = tokenHash,
								GuidAccess = Guid.NewGuid(),
								Activo = true,
								Estado = true,
								UsuarioCreacion = Postulantes.Usuario,
								UsuarioModificacion = Postulantes.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now,
								FechaEnvioAccesos = DateTime.Now
							};

							_repTokenPostulanteProcesoSeleccion.Insert(tokenNueva);
							postulanteProcesoSeleccion.Token = token;
							postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
						}
					}

					var postulante = _repPostulante.FirstById(postulanteProcesoSeleccion.IdPostulante);
					if (idPostulanteProcesoSeleccion > 0 && idPostulanteProcesoSeleccion != null)
					{
						DateTime? fecha = null;
						if (Postulantes.Fecha.HasValue)
						{
							fecha = Postulantes.Fecha.Value.AddHours(-5);
						}
						var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
						{
							IdPlantilla = Postulantes.IdPlantilla,
							IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
							Personal = personal,
							FechaGP = fecha
						};
						_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccion();
						var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;
						var celular = postulante.ObtenerNumeroWhatsApp(postulante.IdPais.Value, postulante.Celular);
						ValidarNumerosWhatsAppAsyncDTO arr = new ValidarNumerosWhatsAppAsyncDTO();
						List<string> contacts = new List<string>();
						contacts.Add(("+" + celular));
						arr.contacts = contacts;
						try
						{
							this.EnvioAutomaticoPlantilla(whatsAppCalculado.Plantilla, whatsAppCalculado.ListaEtiquetas, Postulantes.IdPlantilla, celular, postulante.IdPais.Value, personal.Id, postulante.Id, Postulantes.Usuario);
						}
						catch (Exception e)
						{
							Incorrectos.Add(postulante.NroDocumento);
						}

					}
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		private void EnvioAutomaticoPlantilla(string PlantillaReemplazada, List<datoPlantillaWhatsApp> PlantillaWhatsApp, int IdPlantilla, string Celular, int IdPais, int IdPersonal, int IdPostulante, string Usuario)
		{
			ValidarNumerosWhatsAppAsyncDTO arr = new ValidarNumerosWhatsAppAsyncDTO();
			PostulanteBO postulante = new PostulanteBO();
			List<string> contacts = new List<string>();
			contacts.Add(("+" + Celular));
			arr.contacts = contacts;
			var res = postulante.ValidarNumeroEnvioWhatsApp(IdPersonal, IdPais, arr);

			if (res)
			{
				PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);
				bool banderaLogin = false;
				string _tokenComunicacion = string.Empty;
				var Plantilla = _repPlantilla.ObtenerPlantillaPorId(IdPlantilla);
				WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
				{
					Id = 0,
					WaTo = Celular,
					WaType = "hsm",
					WaTypeMensaje = 8,
					WaRecipientType = "hsm",
					WaBody = Plantilla.Descripcion,
					WaCaption = PlantillaReemplazada,
					datosPlantillaWhatsApp = PlantillaWhatsApp,
				};

				try
				{
					ServicePointManager.ServerCertificateValidationCallback =
					delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
					{
						return true;
					};

					WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
					WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

					var _credencialesHost = _repCredenciales.ObtenerCredencialHost(IdPais);
					//personal debe tener accesos
					var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, IdPais);

					string urlToPost = _credencialesHost.UrlWhatsApp;

					string resultado = string.Empty, _waType = string.Empty;

					WhatsAppMensajeEnviadoPostulanteBO mensajeEnviado = new WhatsAppMensajeEnviadoPostulanteBO();

					if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
					{
						string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

						var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

						var client = new RestClient(urlToPostUsuario);
						var request = new RestSharp.RestRequest(Method.POST);
						request.AddHeader("cache-control", "no-cache");
						request.AddHeader("Content-Length", "");
						request.AddHeader("Accept-Encoding", "gzip, deflate");
						request.AddHeader("Host", _credencialesHost.IpHost);
						request.AddHeader("Cache-Control", "no-cache");
						request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
						request.AddHeader("Content-Type", "application/json");

					}
					else
					{
						_tokenComunicacion = tokenValida.UserAuthToken;
						banderaLogin = true;
					}

					if (banderaLogin)
					{
						switch (DTO.WaType.ToLower())
						{
							case "text":
								urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
								_waType = "text";

								MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

								_mensajeTexto.to = DTO.WaTo;
								_mensajeTexto.type = DTO.WaType;
								_mensajeTexto.recipient_type = DTO.WaRecipientType;
								_mensajeTexto.text = new text();

								_mensajeTexto.text.body = DTO.WaBody;

								using (WebClient client = new WebClient())
								{
									//client.Encoding = Encoding.UTF8;
									var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
									var serializer = new JavaScriptSerializer();

									var serializedResult = serializer.Serialize(_mensajeTexto);
									string myParameters = serializedResult;
									client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
									client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
									client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
									client.Headers[HttpRequestHeader.ContentType] = "application/json";
									resultado = client.UploadString(urlToPost, myParameters);
								}

								break;
							case "hsm":
								urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
								_waType = "hsm";

								MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

								_mensajePlantilla.to = DTO.WaTo;
								_mensajePlantilla.type = DTO.WaType;
								_mensajePlantilla.hsm = new hsm();

								_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
								_mensajePlantilla.hsm.element_name = DTO.WaBody;

								_mensajePlantilla.hsm.language = new language();
								_mensajePlantilla.hsm.language.policy = "deterministic";
								_mensajePlantilla.hsm.language.code = "es";

								if (DTO.datosPlantillaWhatsApp != null)
								{
									_mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
									foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
									{
										localizable_params _dato = new localizable_params();
										_dato.@default = listaDatos.texto;

										_mensajePlantilla.hsm.localizable_params.Add(_dato);
									}
								}

								using (WebClient client = new WebClient())
								{
									client.Encoding = Encoding.UTF8;
									var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
									var serializer = new JavaScriptSerializer();

									var serializedResult = serializer.Serialize(_mensajePlantilla);
									string myParameters = serializedResult;
									client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
									client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
									client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
									client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
									resultado = client.UploadString(urlToPost, myParameters);
								}

								break;
							case "image":
								urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
								_waType = "image";

								MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
								_mensajeImagen.to = DTO.WaTo;
								_mensajeImagen.type = DTO.WaType;
								_mensajeImagen.recipient_type = DTO.WaRecipientType;

								_mensajeImagen.image = new image();

								_mensajeImagen.image.caption = DTO.WaCaption;
								_mensajeImagen.image.link = DTO.WaLink;

								using (WebClient client = new WebClient())
								{
									client.Encoding = Encoding.UTF8;
									var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
									var serializer = new JavaScriptSerializer();

									var serializedResult = serializer.Serialize(_mensajeImagen);
									string myParameters = serializedResult;
									client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
									client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
									client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
									client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
									resultado = client.UploadString(urlToPost, myParameters);
								}

								break;
							case "document":
								urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
								_waType = "document";

								MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
								_mensajeDocumento.to = DTO.WaTo;
								_mensajeDocumento.type = DTO.WaType;
								_mensajeDocumento.recipient_type = DTO.WaRecipientType;

								_mensajeDocumento.document = new document();

								_mensajeDocumento.document.caption = DTO.WaCaption;
								_mensajeDocumento.document.link = DTO.WaLink;
								_mensajeDocumento.document.filename = DTO.WaFileName;

								using (WebClient client = new WebClient())
								{
									client.Encoding = Encoding.UTF8;
									var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
									var serializer = new JavaScriptSerializer();

									var serializedResult = serializer.Serialize(_mensajeDocumento);
									string myParameters = serializedResult;
									client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
									client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
									client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
									client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
									resultado = client.UploadString(urlToPost, myParameters);
								}

								break;
						}
						var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);
						foreach (var itemGuardar in datoRespuesta.messages)
						{
							WhatsAppMensajeEnviadoPostulanteRepositorio _repMensajeEnviadoPostulante = new WhatsAppMensajeEnviadoPostulanteRepositorio(_integraDBContext);

							mensajeEnviado.WaId = itemGuardar.id;
							mensajeEnviado.WaTo = DTO.WaTo;
							mensajeEnviado.WaType = _waType;
							mensajeEnviado.WaRecipientType = DTO.WaRecipientType;
							mensajeEnviado.WaBody = DTO.WaBody;
							mensajeEnviado.WaCaption = DTO.WaCaption;
							mensajeEnviado.WaLink = DTO.WaLink;
							mensajeEnviado.WaFileName = DTO.WaFileName;
							mensajeEnviado.IdPais = IdPais;
							if (IdPostulante != 0)
							{
								mensajeEnviado.IdPostulante = IdPostulante;
							}
							else
							{
								mensajeEnviado.IdPostulante = null;
							}

							mensajeEnviado.IdPersonal = IdPersonal;
							mensajeEnviado.Estado = true;
							mensajeEnviado.FechaCreacion = DateTime.Now;
							mensajeEnviado.FechaModificacion = DateTime.Now;
							mensajeEnviado.UsuarioCreacion = Usuario;
							mensajeEnviado.UsuarioModificacion = Usuario;

							_repMensajeEnviadoPostulante.Insert(mensajeEnviado);
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
				System.Threading.Thread.Sleep(5000);
			}

		}



		[Route("[action]")]
		[HttpPost]
		public ActionResult IncorporarPostulante([FromBody] int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);

				if (_repPostulante.Exist(IdPostulante))
				{
					IncorporarPostulanteBO incorporarPostulante = new IncorporarPostulanteBO(_integraDBContext);
					if (incorporarPostulante.IncorporarPostulantePersonal(IdPostulante) == true)
					{
						return Ok();
					}
					else
					{
						BadRequest("El Postulante ya es un personal Activo");
						return BadRequest(ModelState);
					}

				}
				else
				{
					BadRequest("No se encontró al postulante");
					return BadRequest(ModelState);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}")]
		public ActionResult ObtenerPostulanteFormacion(int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteFormacionRepositorio _repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
				var postulanteFormacion = _repPostulanteFormacion.ObtenerPostulanteFormacionV2(IdPostulante);
				return Ok(postulanteFormacion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult InsertarPostulanteFormacion([FromBody] PostulanteFormacionFormularioDTO formacionPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteFormacionRepositorio _repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
				PostulanteFormacionBO formacionPostulanteBO = new PostulanteFormacionBO();
				var cantidadRegistros = _repPostulanteFormacion.GetBy(x => x.IdPostulante == formacionPostulante.FormacionPostulante.IdPostulante).ToList();
				if (cantidadRegistros.Count < 5)
				{
					formacionPostulanteBO.IdPostulante = formacionPostulante.FormacionPostulante.IdPostulante;
					formacionPostulanteBO.IdCentroEstudio = formacionPostulante.FormacionPostulante.IdCentroEstudio;
					formacionPostulanteBO.IdTipoEstudio = formacionPostulante.FormacionPostulante.IdTipoEstudio;
					formacionPostulanteBO.IdAreaFormacion = formacionPostulante.FormacionPostulante.IdAreaFormacion;
					formacionPostulanteBO.IdEstadoEstudio = formacionPostulante.FormacionPostulante.IdEstadoEstudio;
					formacionPostulanteBO.FechaInicio = formacionPostulante.FormacionPostulante.FechaInicio;
					formacionPostulanteBO.FechaFin = formacionPostulante.FormacionPostulante.FechaFin;
					formacionPostulanteBO.OtraInstitucion = formacionPostulante.FormacionPostulante.OtraInstitucion;
					formacionPostulanteBO.OtraCarrera = formacionPostulante.FormacionPostulante.OtraCarrera;
					formacionPostulanteBO.AlaActualidad = formacionPostulante.FormacionPostulante.AlaActualidad;
					formacionPostulanteBO.TurnoEstudio = formacionPostulante.FormacionPostulante.TurnoEstudio;
					formacionPostulanteBO.IdPais = formacionPostulante.FormacionPostulante.IdPais;
					formacionPostulanteBO.Estado = true;
					formacionPostulanteBO.UsuarioCreacion = formacionPostulante.Usuario;
					formacionPostulanteBO.UsuarioModificacion = formacionPostulante.Usuario;
					formacionPostulanteBO.FechaCreacion = DateTime.Now;
					formacionPostulanteBO.FechaModificacion = DateTime.Now;

					_repPostulanteFormacion.Insert(formacionPostulanteBO);
					return Ok(true);
				}
				else
				{
					return BadRequest("Solo puede tener 5 registros de Formacion");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarPostulanteFormacion([FromBody] PostulanteFormacionFormularioDTO formacionPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteFormacionRepositorio _repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
				PostulanteFormacionLogRepositorio _repPostulanteFormacionLog = new PostulanteFormacionLogRepositorio(_integraDBContext);
				PostulanteFormacionBO formacionPostulanteBO = new PostulanteFormacionBO();
				PostulanteFormacionLogBO formacionPostulanteLogBO = new PostulanteFormacionLogBO();
				bool bandera = false;
				formacionPostulanteBO = _repPostulanteFormacion.FirstById(formacionPostulante.FormacionPostulante.Id);
				if (formacionPostulanteBO.IdCentroEstudio != formacionPostulante.FormacionPostulante.IdCentroEstudio)
				{
					formacionPostulanteLogBO.IdCentroEstudio = formacionPostulante.FormacionPostulante.IdCentroEstudio;
					bandera = true;
				}
				if (formacionPostulanteBO.IdTipoEstudio != formacionPostulante.FormacionPostulante.IdTipoEstudio)
				{
					formacionPostulanteLogBO.IdTipoEstudio = formacionPostulante.FormacionPostulante.IdTipoEstudio;
					bandera = true;
				}
				if (formacionPostulanteBO.IdAreaFormacion != formacionPostulante.FormacionPostulante.IdAreaFormacion)
				{
					formacionPostulanteLogBO.IdAreaFormacion = formacionPostulante.FormacionPostulante.IdAreaFormacion;
					bandera = true;
				}
				if (formacionPostulanteBO.IdEstadoEstudio != formacionPostulante.FormacionPostulante.IdEstadoEstudio)
				{
					formacionPostulanteLogBO.IdEstadoEstudio = formacionPostulante.FormacionPostulante.IdEstadoEstudio;
					bandera = true;
				}
				var Fecha = formacionPostulanteBO.FechaInicio.ToString();
				var temp1 = Fecha.Split(" ");
				var FechaBD = formacionPostulante.FormacionPostulante.FechaInicio.ToString();
				var temp2 = FechaBD.Split(" ");
				if (temp1[0] != temp2[0])
				{
					formacionPostulanteLogBO.FechaInicio = formacionPostulante.FormacionPostulante.FechaInicio;
					bandera = true;
				}
				Fecha = formacionPostulanteBO.FechaFin.ToString();
				temp1 = Fecha.Split(" ");
				FechaBD = formacionPostulante.FormacionPostulante.FechaFin.ToString();
				temp2 = FechaBD.Split(" ");
				if (temp1[0] != temp2[0])
				{
					formacionPostulanteLogBO.FechaFin = formacionPostulante.FormacionPostulante.FechaFin;
					bandera = true;
				}
				if (formacionPostulanteBO.OtraInstitucion != formacionPostulante.FormacionPostulante.OtraInstitucion)
				{
					formacionPostulanteLogBO.OtraInstitucion = formacionPostulante.FormacionPostulante.OtraInstitucion;
					bandera = true;
				}
				if (formacionPostulanteBO.OtraCarrera != formacionPostulante.FormacionPostulante.OtraCarrera)
				{
					formacionPostulanteLogBO.OtraCarrera = formacionPostulante.FormacionPostulante.OtraCarrera;
					bandera = true;
				}
				if (formacionPostulanteBO.AlaActualidad != formacionPostulante.FormacionPostulante.AlaActualidad)
				{
					formacionPostulanteLogBO.AlaActualidad = formacionPostulante.FormacionPostulante.AlaActualidad;
					bandera = true;
				}
				if (formacionPostulanteBO.TurnoEstudio != formacionPostulante.FormacionPostulante.TurnoEstudio)
				{
					formacionPostulanteLogBO.TurnoEstudio = formacionPostulante.FormacionPostulante.TurnoEstudio;
					bandera = true;
				}
				if (formacionPostulanteBO.IdPais != formacionPostulante.FormacionPostulante.IdPais)
				{
					formacionPostulanteLogBO.IdPais = formacionPostulante.FormacionPostulante.IdPais;
					bandera = true;
				}
				formacionPostulanteBO.IdCentroEstudio = formacionPostulante.FormacionPostulante.IdCentroEstudio;
				formacionPostulanteBO.IdTipoEstudio = formacionPostulante.FormacionPostulante.IdTipoEstudio;
				formacionPostulanteBO.IdAreaFormacion = formacionPostulante.FormacionPostulante.IdAreaFormacion;
				formacionPostulanteBO.IdEstadoEstudio = formacionPostulante.FormacionPostulante.IdEstadoEstudio;
				formacionPostulanteBO.FechaInicio = formacionPostulante.FormacionPostulante.FechaInicio;
				formacionPostulanteBO.FechaFin = formacionPostulante.FormacionPostulante.FechaFin;
				formacionPostulanteBO.OtraInstitucion = formacionPostulante.FormacionPostulante.OtraInstitucion;
				formacionPostulanteBO.OtraCarrera = formacionPostulante.FormacionPostulante.OtraCarrera;
				formacionPostulanteBO.AlaActualidad = formacionPostulante.FormacionPostulante.AlaActualidad;
				formacionPostulanteBO.TurnoEstudio = formacionPostulante.FormacionPostulante.TurnoEstudio;
				formacionPostulanteBO.IdPais = formacionPostulante.FormacionPostulante.IdPais;
				formacionPostulanteBO.UsuarioModificacion = formacionPostulante.Usuario;
				formacionPostulanteBO.FechaModificacion = DateTime.Now;
				_repPostulanteFormacion.Update(formacionPostulanteBO);
				if (bandera)
				{
					formacionPostulanteLogBO.IdPostulante = formacionPostulante.FormacionPostulante.IdPostulante;
					formacionPostulanteLogBO.IdPostulanteFormacion = formacionPostulanteBO.Id;
					formacionPostulanteLogBO.TipoActualizacion = "Editado";
					formacionPostulanteLogBO.Estado = true;
					formacionPostulanteLogBO.UsuarioCreacion = formacionPostulante.Usuario;
					formacionPostulanteLogBO.UsuarioModificacion = formacionPostulante.Usuario;
					formacionPostulanteLogBO.FechaCreacion = DateTime.Now;
					formacionPostulanteLogBO.FechaModificacion = DateTime.Now;
					_repPostulanteFormacionLog.Insert(formacionPostulanteLogBO);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult EliminarPostulanteFormacion([FromBody] EliminarDTO formacionPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteFormacionRepositorio _repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
				PostulanteFormacionLogRepositorio _repPostulanteFormacionLog = new PostulanteFormacionLogRepositorio(_integraDBContext);
				PostulanteFormacionBO formacionPostulanteBO = new PostulanteFormacionBO();
				PostulanteFormacionLogBO formacionPostulanteLogBO = new PostulanteFormacionLogBO();

				formacionPostulanteBO = _repPostulanteFormacion.FirstById(formacionPostulante.Id);

				formacionPostulanteLogBO.IdPostulante = formacionPostulanteBO.IdPostulante;
				formacionPostulanteLogBO.IdPostulanteFormacion = formacionPostulanteBO.Id;
				formacionPostulanteLogBO.IdCentroEstudio = formacionPostulanteBO.IdCentroEstudio;
				formacionPostulanteLogBO.IdTipoEstudio = formacionPostulanteBO.IdTipoEstudio;
				formacionPostulanteLogBO.IdAreaFormacion = formacionPostulanteBO.IdAreaFormacion;
				formacionPostulanteLogBO.IdEstadoEstudio = formacionPostulanteBO.IdEstadoEstudio;
				formacionPostulanteLogBO.FechaInicio = formacionPostulanteBO.FechaInicio;
				formacionPostulanteLogBO.FechaFin = formacionPostulanteBO.FechaFin;
				formacionPostulanteLogBO.OtraInstitucion = formacionPostulanteBO.OtraInstitucion;
				formacionPostulanteLogBO.OtraCarrera = formacionPostulanteBO.OtraCarrera;
				formacionPostulanteLogBO.AlaActualidad = formacionPostulanteBO.AlaActualidad;
				formacionPostulanteLogBO.TurnoEstudio = formacionPostulanteBO.TurnoEstudio;
				formacionPostulanteLogBO.IdPais = formacionPostulanteBO.IdPais;
				formacionPostulanteLogBO.TipoActualizacion = "Eliminado";
				formacionPostulanteLogBO.Estado = true;
				formacionPostulanteLogBO.UsuarioCreacion = formacionPostulante.NombreUsuario;
				formacionPostulanteLogBO.UsuarioModificacion = formacionPostulante.NombreUsuario;
				formacionPostulanteLogBO.FechaCreacion = DateTime.Now;
				formacionPostulanteLogBO.FechaModificacion = DateTime.Now;
				_repPostulanteFormacionLog.Insert(formacionPostulanteLogBO);

				_repPostulanteFormacion.Delete(formacionPostulante.Id, formacionPostulante.NombreUsuario);

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Historial Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}")]
		public ActionResult ObtenerHistorialPostulanteFormacion(int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteFormacionLogRepositorio _repPostulanteFormacionLog = new PostulanteFormacionLogRepositorio(_integraDBContext);
				var postulanteFormacionLog = _repPostulanteFormacionLog.ObtenerPostulanteFormacionLog(IdPostulante);
				return Ok(postulanteFormacionLog);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Experiencia de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}")]
		public ActionResult ObtenerPostulanteExperiencia(int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
				var postulanteExperiencia = _repPostulanteExperiencia.GetBy(x => x.IdPostulante == IdPostulante);

				return Ok(postulanteExperiencia);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta Experiencia de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult InsertarPostulanteExperiencia([FromBody] PostulanteExperienciaFormularioDTO experienciaPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
				var cantidadRegistros = _repPostulanteExperiencia.GetBy(x => x.IdPostulante == experienciaPostulante.ExperienciaPostulante.IdPostulante).ToList();
				if (cantidadRegistros.Count < 5)
				{
					PostulanteExperienciaBO experienciaPostulanteBO = new PostulanteExperienciaBO();
					experienciaPostulanteBO.IdPostulante = experienciaPostulante.ExperienciaPostulante.IdPostulante;
					experienciaPostulanteBO.IdEmpresa = experienciaPostulante.ExperienciaPostulante.IdEmpresa;
					experienciaPostulanteBO.OtraEmpresa = experienciaPostulante.ExperienciaPostulante.OtraEmpresa;
					experienciaPostulanteBO.IdCargo = experienciaPostulante.ExperienciaPostulante.IdCargo;
					experienciaPostulanteBO.IdAreaTrabajo = experienciaPostulante.ExperienciaPostulante.IdAreaTrabajo;
					experienciaPostulanteBO.IdIndustria = experienciaPostulante.ExperienciaPostulante.IdIndustria;
					experienciaPostulanteBO.FechaInicio = experienciaPostulante.ExperienciaPostulante.FechaInicio;
					experienciaPostulanteBO.FechaFin = experienciaPostulante.ExperienciaPostulante.FechaFin;
					experienciaPostulanteBO.NombreJefe = experienciaPostulante.ExperienciaPostulante.NombreJefe;
					experienciaPostulanteBO.NumeroJefe = experienciaPostulante.ExperienciaPostulante.NumeroJefe;
					experienciaPostulanteBO.AlaActualidad = experienciaPostulante.ExperienciaPostulante.AlaActualidad;
					experienciaPostulanteBO.EsUltimoEmpleo = experienciaPostulante.ExperienciaPostulante.EsUltimoEmpleo;
					experienciaPostulanteBO.Salario = experienciaPostulante.ExperienciaPostulante.Salario;
					experienciaPostulanteBO.Funcion = experienciaPostulante.ExperienciaPostulante.Funcion;
					experienciaPostulanteBO.SalarioComision = experienciaPostulante.ExperienciaPostulante.SalarioComision;
					experienciaPostulanteBO.IdMoneda = experienciaPostulante.ExperienciaPostulante.IdMoneda;
					experienciaPostulanteBO.Estado = true;
					experienciaPostulanteBO.UsuarioCreacion = experienciaPostulante.Usuario;
					experienciaPostulanteBO.UsuarioModificacion = experienciaPostulante.Usuario;
					experienciaPostulanteBO.FechaCreacion = DateTime.Now;
					experienciaPostulanteBO.FechaModificacion = DateTime.Now;

					_repPostulanteExperiencia.Insert(experienciaPostulanteBO);
					return Ok(true);
				}
				else
				{
					return BadRequest("Solo puede tener 5 registros de Experiencia");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Experiencia de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarPostulanteExperiencia([FromBody] PostulanteExperienciaFormularioDTO experienciaPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
				PostulanteExperienciaLogRepositorio _repPostulanteExperienciaLog = new PostulanteExperienciaLogRepositorio(_integraDBContext);
				PostulanteExperienciaBO experienciaPostulanteBO = new PostulanteExperienciaBO();
				PostulanteExperienciaLogBO experienciaPostulanteLogBO = new PostulanteExperienciaLogBO();
				bool bandera = false;
				experienciaPostulanteBO = _repPostulanteExperiencia.FirstById(experienciaPostulante.ExperienciaPostulante.Id);

				if (experienciaPostulanteBO.IdEmpresa != experienciaPostulante.ExperienciaPostulante.IdEmpresa)
				{
					experienciaPostulanteLogBO.IdEmpresa = experienciaPostulante.ExperienciaPostulante.IdEmpresa;
					bandera = true;
				}
				if (experienciaPostulanteBO.OtraEmpresa != experienciaPostulante.ExperienciaPostulante.OtraEmpresa)
				{
					experienciaPostulanteLogBO.OtraEmpresa = experienciaPostulante.ExperienciaPostulante.OtraEmpresa;
					bandera = true;
				}
				if (experienciaPostulanteBO.IdCargo != experienciaPostulante.ExperienciaPostulante.IdCargo)
				{
					experienciaPostulanteLogBO.IdCargo = experienciaPostulante.ExperienciaPostulante.IdCargo;
					bandera = true;
				}
				if (experienciaPostulanteBO.IdAreaTrabajo != experienciaPostulante.ExperienciaPostulante.IdAreaTrabajo)
				{
					experienciaPostulanteLogBO.IdAreaTrabajo = experienciaPostulante.ExperienciaPostulante.IdAreaTrabajo;
					bandera = true;
				}
				if (experienciaPostulanteBO.IdIndustria != experienciaPostulante.ExperienciaPostulante.IdIndustria)
				{
					experienciaPostulanteLogBO.IdIndustria = experienciaPostulante.ExperienciaPostulante.IdIndustria;
					bandera = true;
				}
				var Fecha = experienciaPostulanteBO.FechaInicio.ToString();
				var temp1 = Fecha.Split(" ");
				var FechaBD = experienciaPostulante.ExperienciaPostulante.FechaInicio.ToString();
				var temp2 = FechaBD.Split(" ");
				if (temp1[0] != temp2[0])
				{
					experienciaPostulanteLogBO.FechaInicio = experienciaPostulante.ExperienciaPostulante.FechaInicio;
					bandera = true;
				}
				Fecha = experienciaPostulanteBO.FechaFin.ToString();
				temp1 = Fecha.Split(" ");
				FechaBD = experienciaPostulante.ExperienciaPostulante.FechaFin.ToString();
				temp2 = FechaBD.Split(" ");
				if (temp1[0] != temp2[0])
				{
					experienciaPostulanteLogBO.FechaFin = experienciaPostulante.ExperienciaPostulante.FechaFin;
					bandera = true;
				}
				if (experienciaPostulanteBO.NombreJefe != experienciaPostulante.ExperienciaPostulante.NombreJefe)
				{
					experienciaPostulanteLogBO.NombreJefe = experienciaPostulante.ExperienciaPostulante.NombreJefe;
					bandera = true;
				}
				if (experienciaPostulanteBO.NumeroJefe != experienciaPostulante.ExperienciaPostulante.NumeroJefe)
				{
					experienciaPostulanteLogBO.NumeroJefe = experienciaPostulante.ExperienciaPostulante.NumeroJefe;
					bandera = true;
				}
				if (experienciaPostulanteBO.AlaActualidad != experienciaPostulante.ExperienciaPostulante.AlaActualidad)
				{
					experienciaPostulanteLogBO.AlaActualidad = experienciaPostulante.ExperienciaPostulante.AlaActualidad;
					bandera = true;
				}
				if (experienciaPostulanteBO.EsUltimoEmpleo != experienciaPostulante.ExperienciaPostulante.EsUltimoEmpleo)
				{
					experienciaPostulanteLogBO.EsUltimoEmpleo = experienciaPostulante.ExperienciaPostulante.EsUltimoEmpleo;
					bandera = true;
				}
				if (experienciaPostulanteBO.Salario != experienciaPostulante.ExperienciaPostulante.Salario)
				{
					experienciaPostulanteLogBO.Salario = experienciaPostulante.ExperienciaPostulante.Salario;
					bandera = true;
				}
				if (experienciaPostulanteBO.Funcion != experienciaPostulante.ExperienciaPostulante.Funcion)
				{
					experienciaPostulanteLogBO.Funcion = experienciaPostulante.ExperienciaPostulante.Funcion;
					bandera = true;
				}
				if (experienciaPostulanteBO.SalarioComision != experienciaPostulante.ExperienciaPostulante.SalarioComision)
				{
					experienciaPostulanteLogBO.SalarioComision = experienciaPostulante.ExperienciaPostulante.SalarioComision;
					bandera = true;
				}
				if (experienciaPostulanteBO.IdMoneda != experienciaPostulante.ExperienciaPostulante.IdMoneda)
				{
					experienciaPostulanteLogBO.IdMoneda = experienciaPostulante.ExperienciaPostulante.IdMoneda;
					bandera = true;
				}
				experienciaPostulanteBO.IdPostulante = experienciaPostulante.ExperienciaPostulante.IdPostulante;
				experienciaPostulanteBO.IdEmpresa = experienciaPostulante.ExperienciaPostulante.IdEmpresa;
				experienciaPostulanteBO.OtraEmpresa = experienciaPostulante.ExperienciaPostulante.OtraEmpresa;
				experienciaPostulanteBO.IdCargo = experienciaPostulante.ExperienciaPostulante.IdCargo;
				experienciaPostulanteBO.IdAreaTrabajo = experienciaPostulante.ExperienciaPostulante.IdAreaTrabajo;
				experienciaPostulanteBO.IdIndustria = experienciaPostulante.ExperienciaPostulante.IdIndustria;
				experienciaPostulanteBO.FechaInicio = experienciaPostulante.ExperienciaPostulante.FechaInicio;
				experienciaPostulanteBO.FechaFin = experienciaPostulante.ExperienciaPostulante.FechaFin;
				experienciaPostulanteBO.NombreJefe = experienciaPostulante.ExperienciaPostulante.NombreJefe;
				experienciaPostulanteBO.NumeroJefe = experienciaPostulante.ExperienciaPostulante.NumeroJefe;
				experienciaPostulanteBO.AlaActualidad = experienciaPostulante.ExperienciaPostulante.AlaActualidad;
				experienciaPostulanteBO.EsUltimoEmpleo = experienciaPostulante.ExperienciaPostulante.EsUltimoEmpleo;
				experienciaPostulanteBO.Salario = experienciaPostulante.ExperienciaPostulante.Salario;
				experienciaPostulanteBO.Funcion = experienciaPostulante.ExperienciaPostulante.Funcion;
				experienciaPostulanteBO.SalarioComision = experienciaPostulante.ExperienciaPostulante.SalarioComision;
				experienciaPostulanteBO.IdMoneda = experienciaPostulante.ExperienciaPostulante.IdMoneda;
				experienciaPostulanteBO.UsuarioModificacion = experienciaPostulante.Usuario;
				experienciaPostulanteBO.FechaModificacion = DateTime.Now;
				_repPostulanteExperiencia.Update(experienciaPostulanteBO);
				if (bandera)
				{
					experienciaPostulanteLogBO.IdPostulante = experienciaPostulante.ExperienciaPostulante.IdPostulante;
					experienciaPostulanteLogBO.IdPostulanteExperiencia = experienciaPostulanteBO.Id;
					experienciaPostulanteLogBO.TipoActualizacion = "Editado";
					experienciaPostulanteLogBO.Estado = true;
					experienciaPostulanteLogBO.UsuarioCreacion = experienciaPostulante.Usuario;
					experienciaPostulanteLogBO.UsuarioModificacion = experienciaPostulante.Usuario;
					experienciaPostulanteLogBO.FechaCreacion = DateTime.Now;
					experienciaPostulanteLogBO.FechaModificacion = DateTime.Now;
					_repPostulanteExperienciaLog.Insert(experienciaPostulanteLogBO);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 19/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina Experiencia de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult EliminarPostulanteExperiencia([FromBody] EliminarDTO experienciaPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
				PostulanteExperienciaLogRepositorio _repPostulanteExperienciaLog = new PostulanteExperienciaLogRepositorio(_integraDBContext);
				PostulanteExperienciaBO experienciaPostulanteBO = new PostulanteExperienciaBO();
				PostulanteExperienciaLogBO experienciaPostulanteLogBO = new PostulanteExperienciaLogBO();


				experienciaPostulanteBO = _repPostulanteExperiencia.FirstById(experienciaPostulante.Id);

				experienciaPostulanteLogBO.IdPostulante = experienciaPostulanteBO.IdPostulante;
				experienciaPostulanteLogBO.IdPostulanteExperiencia = experienciaPostulanteBO.Id;
				experienciaPostulanteLogBO.IdEmpresa = experienciaPostulanteBO.IdEmpresa;
				experienciaPostulanteLogBO.OtraEmpresa = experienciaPostulanteBO.OtraEmpresa;
				experienciaPostulanteLogBO.IdCargo = experienciaPostulanteBO.IdCargo;
				experienciaPostulanteLogBO.IdAreaTrabajo = experienciaPostulanteBO.IdAreaTrabajo;
				experienciaPostulanteLogBO.IdIndustria = experienciaPostulanteBO.IdIndustria;
				experienciaPostulanteLogBO.FechaInicio = experienciaPostulanteBO.FechaInicio;
				experienciaPostulanteLogBO.FechaFin = experienciaPostulanteBO.FechaFin;
				experienciaPostulanteLogBO.NombreJefe = experienciaPostulanteBO.NombreJefe;
				experienciaPostulanteLogBO.NumeroJefe = experienciaPostulanteBO.NumeroJefe;
				experienciaPostulanteLogBO.AlaActualidad = experienciaPostulanteBO.AlaActualidad;
				experienciaPostulanteLogBO.EsUltimoEmpleo = experienciaPostulanteBO.EsUltimoEmpleo;
				experienciaPostulanteLogBO.Salario = experienciaPostulanteBO.Salario;
				experienciaPostulanteLogBO.Funcion = experienciaPostulanteBO.Funcion;
				experienciaPostulanteLogBO.SalarioComision = experienciaPostulanteBO.SalarioComision;
				experienciaPostulanteLogBO.IdMoneda = experienciaPostulanteBO.IdMoneda;
				experienciaPostulanteLogBO.TipoActualizacion = "Eliminado";
				experienciaPostulanteLogBO.Estado = true;
				experienciaPostulanteLogBO.UsuarioCreacion = experienciaPostulante.NombreUsuario;
				experienciaPostulanteLogBO.UsuarioModificacion = experienciaPostulante.NombreUsuario;
				experienciaPostulanteLogBO.FechaCreacion = DateTime.Now;
				experienciaPostulanteLogBO.FechaModificacion = DateTime.Now;
				_repPostulanteExperienciaLog.Insert(experienciaPostulanteLogBO);

				_repPostulanteExperiencia.Delete(experienciaPostulante.Id, experienciaPostulante.NombreUsuario);

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Historial Formacion de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}")]
		public ActionResult ObtenerHistorialPostulanteExperiencia(int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteExperienciaLogRepositorio _repPostulanteExperienciaLog = new PostulanteExperienciaLogRepositorio(_integraDBContext);
				var postulanteExperienciaLog = _repPostulanteExperienciaLog.ObtenerPostulanteExperienciaLog(IdPostulante);
				return Ok(postulanteExperienciaLog);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Historial de datos de los Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}/{Clave}")]
		public ActionResult ObtenerHistorialPostulante(int IdPostulante, string Clave)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var postulanteHistorial = _repPostulante.ObtenerHistorialPostulante(IdPostulante, Clave);
				return Ok(postulanteHistorial);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 18/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Valida el correo Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}")]
		public ActionResult ValidarCorreoPostulante(int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var postulanteHistorial = _repPostulante.ValidarCorreoPostulante(IdPostulante);
				return Ok(postulanteHistorial);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: GET
		/// Autor: Jashin Salazar.
		/// Fecha: 20/11/2021
		/// Versión: 1.0
		/// <summary>
		/// Valida examenes identicos y aprobados en dos procesos
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpGet]
		[Route("[Action]/{IdPostulante}/{ProcesoOrigen}/{ProcesoDestino}")]
		public ActionResult CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var postulanteHistorial = _repPostulante.CompararProcesosSeleccion(IdPostulante, ProcesoOrigen, ProcesoDestino);
				return Ok(postulanteHistorial);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 01/12/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza al nuevo proceso de un postulante
		/// </summary>
		/// <returns> true or false</returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarProcesoPostulante([FromBody] PostulanteProcesoNuevoDTO Informacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var postulanteHistorial = _repPostulante.ActualizarProcesoPostulante(Informacion);
				PostulanteLogRepositorio _repPostulanteLog = new PostulanteLogRepositorio(_integraDBContext);
				ProcesoSeleccionRepositorio _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
				PostulanteLogBO postulanteLog = new PostulanteLogBO();
				postulanteLog.IdPostulante = Informacion.IdPostulante;
				postulanteLog.Estado = true;
				postulanteLog.UsuarioCreacion = Informacion.Usuario;
				postulanteLog.UsuarioModificacion = Informacion.Usuario;
				postulanteLog.FechaCreacion = DateTime.Now;
				postulanteLog.FechaModificacion = DateTime.Now;
				postulanteLog.Clave = "ProcesoSeleccion";
				postulanteLog.Valor = _repProcesoSeleccion.FirstById((int)Informacion.IdProcesoSeleccionDestino).Nombre;
				_repPostulanteLog.Insert(postulanteLog);

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Jashin Salazar.
		/// Fecha: 01/12/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza al nuevo proceso de un postulante sin nota
		/// </summary>
		/// <returns> true or false</returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarProcesoPostulanteSinNota([FromBody] PostulanteProcesoNuevoDTO Informacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
				var postulanteHistorial = _repPostulante.ActualizarProcesoPostulanteSinNota(Informacion);
				PostulanteLogRepositorio _repPostulanteLog = new PostulanteLogRepositorio(_integraDBContext);
				ProcesoSeleccionRepositorio _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
				PostulanteLogBO postulanteLog = new PostulanteLogBO();
				postulanteLog.IdPostulante = Informacion.IdPostulante;
				postulanteLog.Estado = true;
				postulanteLog.UsuarioCreacion = Informacion.Usuario;
				postulanteLog.UsuarioModificacion = Informacion.Usuario;
				postulanteLog.FechaCreacion = DateTime.Now;
				postulanteLog.FechaModificacion = DateTime.Now;
				postulanteLog.Clave = "ProcesoSeleccion";
				postulanteLog.Valor = _repProcesoSeleccion.FirstById((int)Informacion.IdProcesoSeleccionDestino).Nombre;
				_repPostulanteLog.Insert(postulanteLog);

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}