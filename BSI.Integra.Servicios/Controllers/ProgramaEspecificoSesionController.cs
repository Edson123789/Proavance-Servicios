using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProgramaEspecificoSesion")]
    public class ProgramaEspecificoSesionController : BaseController<TPespecificoSesion, ValidadorProgramaEspecificoSesionDTO>
    {
        public ProgramaEspecificoSesionController(IIntegraRepository<TPespecificoSesion> repositorio, ILogger<BaseController<TPespecificoSesion, ValidadorProgramaEspecificoSesionDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarExpositorAmbienteDeSesionesByPEspecifico ([FromBody] Dictionary<string, string> dto)
        {
            try
            {
                PespecificoSesionBO Objeto = new PespecificoSesionBO();
                bool rpta;

                using (TransactionScope scope = new TransactionScope())
                {
                    rpta = Objeto.ActualizarExpositorAmbienteDeSesionesByPEspecifico(dto);
                    scope.Complete();
                }

                return Ok(rpta);
            }
            catch (Exception ex)
            {
               return NotFound(ex.Message);
            }       

        }
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarDatosCronogramaSesiones([FromBody]InformacionCronogramaSesionesDTO Objeto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
                PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio();
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
                Objeto.FechaHoraInicio = Objeto.FechaHoraInicio.AddHours(-5);
				var sesion = _repPespecificoSesion.FirstById(Objeto.Id);
				double duracion = Convert.ToDouble(sesion.Duracion);
				var fechasCruces = _repPespecificoSesion.ValidarCrucesSesiones(Objeto, duracion);
				bool estadoCruce = false;
				if (fechasCruces.Count != 0)
				{
					estadoCruce = true;
					return Ok(new { EstadoCruce = estadoCruce, Cruces = fechasCruces });
				}
				else
				{
					var estadoFur = false;
					string Detalle = "";
					FurRepositorio _repFur = new FurRepositorio();

					if (Objeto.IdAmbiente != null) { 
						if (Objeto.IdAmbiente == -1)
						{
							sesion.IdAmbiente = null;
						}
						else
						{
							sesion.IdAmbiente = Objeto.IdAmbiente;
						}
					}
					if (Objeto.IdProveedor != null)
					{
						if (Objeto.AplicarCambios && Objeto.IdProveedor != sesion.IdProveedor)
						{
							var furs = _repFur.ObtenerFurProgramaEspecifico(sesion.IdPespecifico, 1, true);
							ClasificacionPersonaRepositorio _repClasificacionPersona = new ClasificacionPersonaRepositorio();
							HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();

							foreach (var item in furs)
							{
								var fur = _repFur.FirstById(item.Id);
								//var clasificacionPersonaExpositor = _repClasificacionPersona.FirstBy(x => x.IdTablaOriginal == Objeto.IdExpositor && x.IdTipoPersona == 3);
								//var clasificacionPersonaProveedor = _repClasificacionPersona.FirstBy(x => x.IdPersona == clasificacionPersonaExpositor.IdPersona && x.IdTipoPersona == 4);
								if (Objeto.IdProveedor != null)
								{
									var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(fur.IdProducto.Value, Objeto.IdProveedor.Value);
									if (detalleFur != null)
									{
										ProductoRepositorio _repProducto = new ProductoRepositorio();
										var producto = _repProducto.ObtenerProductoCuentaContable(fur.IdProducto.Value).FirstOrDefault();


										fur.UsuarioSolicitud = Objeto.Usuario;
										fur.NumeroCuenta = detalleFur.NumeroCuenta;
										fur.Cuenta = detalleFur.CuentaDescripcion;
										fur.IdProveedor = Objeto.IdProveedor;
										fur.IdProducto = fur.IdProducto.Value;
										fur.Descripcion = producto.DescripcionProducto;
										fur.IdProductoPresentacion = producto.IdProductoPresentacion;
										fur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
										fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
										fur.IdMonedaProveedor = detalleFur.IdMoneda;
										fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * fur.Cantidad);
										fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
										fur.Monto = fur.PrecioTotalMonedaOrigen;
										fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * fur.Cantidad);
										fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * fur.Cantidad;
										fur.PagoDolares = detalleFur.PrecioDolares * fur.Cantidad;
										fur.IdMonedaPagoReal = detalleFur.IdMonedaPago;
										fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
										fur.UsuarioModificacion = Objeto.Usuario;
										fur.FechaModificacion = DateTime.Now;
										estadoFur = _repFur.Update(fur);
									}
									else
									{
										Detalle = "   • No existe asociación entre Producto y Proveedor\n";
									}
								}
								else
								{
									Detalle += "   • El docente seleccionado no existe como Proveedor\n";
								}
							}
						}


						if (Objeto.IdProveedor == -1)
						{
							sesion.IdProveedor = null;
						}
						else
						{
							sesion.IdProveedor = Objeto.IdProveedor;
						}

						//_repFur
					}

					if (Objeto.GrupoSesion != null)
						sesion.GrupoSesion = Objeto.GrupoSesion;

                   
						

					sesion.UsuarioModificacion = Objeto.Usuario;
					sesion.FechaModificacion = DateTime.Now;
					if(sesion.FechaHoraInicio != Objeto.FechaHoraInicio)
					{
						sesion.UrlWebex = null;
						sesion.CuentaWebex = null;
						if (Objeto.AplicarCambios)
						{
							var furs = _repFur.ObtenerFurProgramaEspecifico(sesion.IdPespecifico, 1, false);
							if (furs.Count > 0)
							{
								foreach (var item in furs)
								{
									var fur = _repFur.FirstById(item.Id);
									if (fur.IdFurFaseAprobacion1 == ValorEstatico.IdFurProyectado || fur.IdFurFaseAprobacion1 == ValorEstatico.IdFurEstadoPorAprobar)
									{
										var nroSemana = fur.obtenerNumeroSemana(Objeto.FechaHoraInicio);
										fur.FechaLimite = Objeto.FechaHoraInicio;
										fur.NumeroSemana = nroSemana;
										fur.UsuarioModificacion = Objeto.Usuario;
										fur.FechaModificacion = DateTime.Now;
										_repFur.Update(fur);
									}
								}
							}
						}	
					}
					sesion.FechaHoraInicio = Objeto.FechaHoraInicio;
					sesion.MostrarPortalWeb = Objeto.MostrarPortalWeb;
					_repPespecificoSesion.Update(sesion);

                    if (Objeto.IdModalidadCurso != null && Objeto.IdModalidadCurso != -1)
                    {
                        _repPespecificoSesion.ActualizarModalidadSesion(sesion.IdPespecifico, sesion.Grupo, Objeto.IdModalidadCurso.Value, Objeto.Usuario);
                    }

                    if (sesion.Id == _repPespecificoSesion.ObtenerSesionInicial(sesion.IdPespecifico, sesion.Grupo))
                    {
                        ProveedorBO Proveedor = new ProveedorBO();
                        if (sesion.IdProveedor != null)
                        {
                            Proveedor = _repProveedor.FirstById(sesion.IdProveedor.Value);
                        }
                        PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositorBO = new PEspecificoParticipacionExpositorBO();
                        if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo))
                        {
                            pEspecificoParticipacionExpositorBO = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo);
                            pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                            pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                            pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.UsuarioModificacion = Objeto.Usuario;

                        }
                        else
                        {
                            pEspecificoParticipacionExpositorBO.IdPespecifico = sesion.IdPespecifico;
                            pEspecificoParticipacionExpositorBO.IdExpositorGrupo = sesion.IdExpositor;
                            pEspecificoParticipacionExpositorBO.ExpositorGrupo = sesion.IdProveedor == null ? null : Proveedor.Nombre1 + " " + Proveedor.Nombre2 + " " + Proveedor.ApePaterno + " " + Proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositorBO.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                            pEspecificoParticipacionExpositorBO.Grupo = sesion.Grupo;
                            pEspecificoParticipacionExpositorBO.Estado = true;
                            pEspecificoParticipacionExpositorBO.FechaCreacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositorBO.UsuarioCreacion = Objeto.Usuario;
                            pEspecificoParticipacionExpositorBO.UsuarioModificacion = Objeto.Usuario;
                        }
                        _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositorBO);
                    }


                    return Ok(new { EstadoCuce = estadoCruce, IdProgramaEspecificoSesion = sesion.Id, Detalle });
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdProgramaEspecificoSesion}/{Usuario}")]
		[HttpGet]
		public ActionResult EstablecerSesionInicial(int IdProgramaEspecificoSesion, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoSesionRepositorio _repProgramaEspecificoSesion = new PespecificoSesionRepositorio();
				PespecificoPadrePespecificoHijoRepositorio _repProgramaEspecificoPadreHijo = new PespecificoPadrePespecificoHijoRepositorio();
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();

				var programaEspecificoSesion = _repProgramaEspecificoSesion.FirstById(IdProgramaEspecificoSesion);
				var programaEspecificoPadre = _repProgramaEspecificoPadreHijo.GetBy(x => x.PespecificoHijoId == programaEspecificoSesion.IdPespecifico).FirstOrDefault();

				int pespecificoId = 0;
				if (programaEspecificoSesion.Grupo == 1)
				{
					if(programaEspecificoPadre != null)
					{
						pespecificoId = programaEspecificoPadre.PespecificoPadreId;
					}
					else
					{
						pespecificoId = programaEspecificoSesion.IdPespecifico;
					}
					var pespecifico = _repPespecifico.FirstById(pespecificoId);
					pespecifico.IdSesionInicio = programaEspecificoSesion.Id;
					pespecifico.UsuarioModificacion = Usuario;
					pespecifico.FechaModificacion = DateTime.Now;
					_repPespecifico.Update(pespecifico);
				}

				if(programaEspecificoPadre != null)
				{
					var listaIdSesiones = _repProgramaEspecificoSesion.ObtenerIdSesiones(programaEspecificoPadre.PespecificoPadreId, programaEspecificoSesion.Grupo).Select(x => x.Id);
					var sesiones = _repProgramaEspecificoSesion.GetBy(x => listaIdSesiones.Contains(x.Id)).ToList();
					foreach(var item in sesiones)
					{
						item.EsSesionInicio = false;
						item.UsuarioModificacion = Usuario;
						item.FechaModificacion = DateTime.Now;
					}
					_repProgramaEspecificoSesion.Update(sesiones);
				}
				else
				{
					var listaIdSesionesIndividuales = _repProgramaEspecificoSesion.ObtenerIdSesionesIndividuales(programaEspecificoSesion.IdPespecifico, programaEspecificoSesion.Grupo).Select(x => x.Id);
					var sesionesIndividuales = _repProgramaEspecificoSesion.GetBy(x => listaIdSesionesIndividuales.Contains(x.Id)).ToList();
					foreach (var item in sesionesIndividuales)
					{
						item.EsSesionInicio = false;
						item.UsuarioModificacion = Usuario;
						item.FechaModificacion = DateTime.Now;
					}
					_repProgramaEspecificoSesion.Update(sesionesIndividuales);
				}

				var estado = false;
				var entidad = _repProgramaEspecificoSesion.FirstById(IdProgramaEspecificoSesion);
				entidad.EsSesionInicio = true;
				entidad.UsuarioModificacion = Usuario;
				entidad.FechaModificacion = DateTime.Now;
				estado = _repProgramaEspecificoSesion.Update(entidad);
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdProgramaEspecifico}/{IdProgramaEspecificoSesion}/{Usuario}")]
		[HttpGet]
		public ActionResult EliminarSesion(int IdProgramaEspecifico, int IdProgramaEspecificoSesion, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repProgramaEspecifico = new PespecificoRepositorio();
				PespecificoSesionRepositorio _repProgramaEspecificoSesion = new PespecificoSesionRepositorio();

				var programaEspecifico = _repProgramaEspecifico.FirstById(IdProgramaEspecifico);
				var programaEspecificoSesion = _repProgramaEspecificoSesion.FirstById(IdProgramaEspecificoSesion);
				programaEspecifico.Duracion = (Convert.ToDecimal(programaEspecifico.Duracion) - programaEspecificoSesion.Duracion).ToString();
				programaEspecifico.UsuarioModificacion = Usuario;
				programaEspecifico.FechaModificacion = DateTime.Now;
				_repProgramaEspecifico.Update(programaEspecifico);
				var estado = false;
				estado = _repProgramaEspecificoSesion.Delete(IdProgramaEspecificoSesion,Usuario);

				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosProgramaEspecifico()
		{				
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ProductoRepositorio _repProducto = new ProductoRepositorio();
				ProductoPresentacionRepositorio _repProductoPresentacion = new ProductoPresentacionRepositorio();
				PgeneralRepositorio _pgeneralRepositorio = new PgeneralRepositorio();
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
				ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio();
				TroncalPgeneralRepositorio _repTroncal = new TroncalPgeneralRepositorio();
				AmbienteRepositorio _repAmbiente = new AmbienteRepositorio();
				OrigenProgramaRepositorio _repOrigenPrograma = new OrigenProgramaRepositorio();
				LocacionRepositorio _repLocacion = new LocacionRepositorio();
				ExpositorRepositorio _repoExpositor = new ExpositorRepositorio();
				FrecuenciaRepositorio _repFrecuencia = new FrecuenciaRepositorio();
				EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				CiudadRepositorio _repCiudad = new CiudadRepositorio();
				PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();

				var _repMaterialTipo = new MaterialTipoRepositorio();
				var _repTipoPersona = new TipoPersonaRepositorio();
				var _repMaterialEstado = new MaterialEstadoRepositorio();
				var _repProveedor = new ProveedorRepositorio();

				var producto = _repProducto.ObtenerListaProductoParaCombo();
				var proveedor = _repProveedor.ObtenerProveedorParaCombo();
				var proveedorCurso = _repProveedor.ObtenerProveedorParaFiltro();
				var productoPresentacion = _repProductoPresentacion.ObtenerProductoPresentacionParaCombo();
				var pGeneral = _pgeneralRepositorio.ObtenerProgramasFiltro();
				var centroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro();
				var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();
				var locacionTroncal = _repTroncal.ObtenerListaLocacionTroncal();
				var ambiente = _repAmbiente.ObtenerAmbientesCiudad();
				var origenProgramas = _repOrigenPrograma.ObtenerOrigenProgramas();
				var locacion = _repLocacion.ObtenerLocacionParaFiltro();
				var expositores = _repoExpositor.ObtenerExpositoresFiltro();
				var frecuencia = _repFrecuencia.ObtenerListaFrecuenciaPlanificacion();
				var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
				var personalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerAreaTrabajoPersonalNombre();
				var ciudad = _repCiudad.ObtenerCiudadFiltro();
				var plantillaCorreo = _repPlantilla.ObtenerListaPlantillasCorreos();
				var plantillaWhatsApp = _repPlantilla.ObtenerListaPlantillasWhatsApp();

				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
				ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
				PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
				AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio();
				SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio();


				var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
				var area = _repArea.ObtenerAreaCapacitacionFiltro();
				var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
				var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(null);
				var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadres(null);
				var programaEspecificoHijos = _repPespecifico.ObtenerProgramaEspecificoHijos();				
				var centroCostoPersonalizado = _repCentroCosto.ObtenerCentroCostoPadres(null);
				var programaespecificoWebinar = _repPespecifico.ObtenerListaPEspecificoWebinar();


				return Ok(new {
					Producto = producto,
					Proveedor = proveedor,
                    proveedorCurso = proveedorCurso,
					ProductoPresentacion = productoPresentacion,
					ProgramaGeneral = pGeneral,
					CentroCosto = centroCosto,
					Modalidad = modalidades,
					LocacionTroncal = locacionTroncal,
					Ambiente = ambiente,
					Origen = origenProgramas,
					Locacion = locacion,
					Expositor = expositores,
					Frecuencia = frecuencia,
					Estado = estadoPespecifico,
					AreasTrabajo = personalAreaTrabajo,
					Ciudad = ciudad,					
					CiudadBS = ciudadBs,
					Area = area,
					SubArea = subArea,
					ProgramaGeneralP = programaGeneral,
					ProgramaEspecifico = programaEspecifico,
                    ProgramaEspecificoHijos = programaEspecificoHijos,
					CentroCostoP = centroCostoPersonalizado,
					ProgramaEspecificoWebinar = programaespecificoWebinar,
					PlantillaCorreo= plantillaCorreo,
					PlantillaWhatsApp= plantillaWhatsApp
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosProgramaEspecificoProveedor()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
                EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio();
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio();
                ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio();

                AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio();
				SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio();
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();

                ProveedorRepositorio _repProveedorRep = new ProveedorRepositorio();
                
				var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
				var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
                var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();

				var area = _repArea.ObtenerAreaCapacitacionFiltro();
				var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
				var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(null);
				var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadresV2(null);
				var centroCostoPersonalizado = _repCentroCosto.ObtenerCentroCostoPadres(null);

                var proveedorHonorario = _repProveedorRep.ObtenerNombreProveedorParaHonorario();

                return Ok(new
                {
                    Estado = estadoPespecifico,
                    CiudadBS = ciudadBs,
                    Modalidad = modalidades,

                    Area = area,
                    SubArea = subArea,
                    ProgramaGeneralP = programaGeneral,
                    ProgramaEspecifico = programaEspecifico,
                    CentroCostoP = centroCostoPersonalizado,

                    ProveedorHonorario = proveedorHonorario
                });
            }
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdProgramaEspecifico}/{Grupo}")]
		[HttpGet]
		public ActionResult ObtenerFurProgramaEspecifico(int IdProgramaEspecifico, int Grupo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			try
			{
				FurRepositorio _repFur = new FurRepositorio();
				var furSesiones = _repFur.ObtenerFurProgramaEspecifico(IdProgramaEspecifico, Grupo,false);
				return Ok(furSesiones);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]/{IdPEspecifico}/{IdMatriculaCabecera}")]
		[HttpGet]
		public ActionResult ObtenerSesionesAsociadosPEspecifico(int IdPEspecifico, int IdMatriculaCabecera)
		{
			try
			{
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();

				var listaSesiones = _repPespecificoSesion.ObtenerSesionesPorPEspecifico(IdPEspecifico, IdMatriculaCabecera).OrderBy(w => w.FechaInicio);

				return Ok(listaSesiones);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarRecuperacion([FromForm] List<RecuperacionSesionDTO> sesiones, [FromForm] int idMatriculaCabecera, [FromForm] string UsuarioRecuperacionSesion)
		{
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
			{
				RecuperacionSesionRepositorio _repRecuperacionSesion = new RecuperacionSesionRepositorio();
				List<RecuperacionSesionBO> listado = new List<RecuperacionSesionBO>();
				using (TransactionScope scope = new TransactionScope())
				{
					foreach (var sesion in sesiones)
					{
                        if (sesion.Recupera)
                        {
                            if (sesion.IdRecuperacionSesion!= null && _repRecuperacionSesion.Exist(w => w.Id == sesion.IdRecuperacionSesion)){
								RecuperacionSesionBO recuperacionSesionBO = _repRecuperacionSesion.FirstById(sesion.IdRecuperacionSesion.Value);
								recuperacionSesionBO.IdMatriculaCabecera = sesion.IdMatriculaCabecera;
								recuperacionSesionBO.IdPespecificoSesion = sesion.IdPespecificoSesion;

								recuperacionSesionBO.Estado = true;
								recuperacionSesionBO.FechaModificacion = DateTime.Now;
								recuperacionSesionBO.UsuarioModificacion = sesion.Usuario;
								_repRecuperacionSesion.Update(recuperacionSesionBO);
							}
                            else
                            {
								RecuperacionSesionBO recuperacionSesionBO = new RecuperacionSesionBO();
								recuperacionSesionBO.IdMatriculaCabecera = sesion.IdMatriculaCabecera;
								recuperacionSesionBO.IdPespecificoSesion = sesion.IdPespecificoSesion;

								recuperacionSesionBO.UsuarioCreacion = sesion.Usuario;
								recuperacionSesionBO.UsuarioModificacion = sesion.Usuario;
								recuperacionSesionBO.Estado = true;
								recuperacionSesionBO.FechaCreacion = DateTime.Now;
								recuperacionSesionBO.FechaModificacion = DateTime.Now;

								_repRecuperacionSesion.Insert(recuperacionSesionBO);
							}
                        }
                        else
                        {
							if (sesion.IdRecuperacionSesion != null && _repRecuperacionSesion.Exist(w => w.Id == sesion.IdRecuperacionSesion)){
								_repRecuperacionSesion.Delete(sesion.IdRecuperacionSesion.Value, sesion.Usuario);													
							}
						}					
					}								
					scope.Complete();
				}
				return Ok(sesiones);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult CancelarWebinar([FromBody] CancelarWebinarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repProgramaEspecifico = new PespecificoRepositorio();
				PespecificoSesionRepositorio _repProgramaEspecificoSesion = new PespecificoSesionRepositorio();
				PespecificoSesionBO Objeto = new PespecificoSesionBO();

				///var programaEspecifico = _repProgramaEspecifico.FirstById(Json.IdPEspespecifico);
				var programaEspecificoSesion = _repProgramaEspecificoSesion.FirstById(Json.IdPEspecificoSesion);

				using (TransactionScope scope = new TransactionScope())
				{
					programaEspecificoSesion.FechaCancelacionWebinar = DateTime.Now;
					programaEspecificoSesion.ComentarioCancelacionWebinar = Json.ComentarioCancelacion;
					programaEspecificoSesion.UsuarioModificacion = Json.Usuario;
					programaEspecificoSesion.EsWebinarConfirmado = Json.Confirmo;
					_repProgramaEspecificoSesion.Update(programaEspecificoSesion);
					scope.Complete();
				}			

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[Action]")]
		[HttpPost]
		public ActionResult ConfirmarWebinar([FromBody] ConfirmarWebinarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{				
				PespecificoSesionRepositorio _repProgramaEspecificoSesion = new PespecificoSesionRepositorio();
				PespecificoSesionBO Objeto = new PespecificoSesionBO();
				
				var programaEspecificoSesion = _repProgramaEspecificoSesion.FirstById(Json.IdPEspecificoSesion);

				using (TransactionScope scope = new TransactionScope())
				{
					programaEspecificoSesion.EsWebinarConfirmado = Json.Confirmo;					
					programaEspecificoSesion.UsuarioModificacion = Json.Usuario;
					_repProgramaEspecificoSesion.Update(programaEspecificoSesion);
					scope.Complete();
				}

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}

	public class ValidadorProgramaEspecificoSesionDTO : AbstractValidator<TPespecificoSesion>
    {
        public static ValidadorProgramaEspecificoSesionDTO Current = new ValidadorProgramaEspecificoSesionDTO();
        public ValidadorProgramaEspecificoSesionDTO()
        {

        }
    }
}
