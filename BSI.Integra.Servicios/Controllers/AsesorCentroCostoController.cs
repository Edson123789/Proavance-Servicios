using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Transactions;
using BSI.Integra.Aplicacion.Marketing.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/AsesorCentroCosto
    /// Autor: Gian Miranda
    /// Fecha: 15/01/2021
    /// <summary>
    /// Configura los parametros de asignacion de oportunidades segun centro de costo y asesor.
    /// </summary>
    [Route("api/AsesorCentroCosto")]
    public class AsesorCentroCostoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AsesorCentroCostoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la grilla principal
        /// </summary>
        /// <returns>Lista de PersonalAsesorCentroCostoDTO en formato JSON</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerAsesorCentroCosto()
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                return Ok(_repPersonal.ObtenerPersonalAsesorCentroCosto());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Todos los filtros del modulo
        /// </summary>
        /// <returns>Filtros para rellenar los Multiselect en formato JSON</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrosAsesorCentroCosto()
        {
            try
            {
                //AsesorCentroCostoOcurrenciaRepositorio _repAsesorCentroCostoOcurrencia = new AsesorCentroCostoOcurrenciaRepositorio(_integraDBContext);
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPW = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                AsesorGrupoFiltroProgramaCriticoDetalleRepositorio _repAsesorGrupoFiltroProgramaCriticoDetalle = new AsesorGrupoFiltroProgramaCriticoDetalleRepositorio(_integraDBContext);

                FiltrosAsesorCentroCostoDTO filtroAsesorCentroCosto = new FiltrosAsesorCentroCostoDTO()
                {
                    //ListaAsesorCentroCostoOcurrencias = _repAsesorCentroCostoOcurrencia.ObtenerAsesorCentroCostoOcurrencia(),
                    ListaAreasCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltroWeb(),
                    ListaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerSubAreasParaFiltroWeb(),
                    ListaProgramaGeneral = _repPGeneral.ObtenerProgramaSubAreaFiltroSoloPortal(),
                    ListaPais = _repPais.ObtenerTodoFiltro(),
                    ListaGrupoFiltroProgramaCritico = _repAsesorGrupoFiltroProgramaCriticoDetalle.ObtenerGrupoFiltroProgramaCriticoFiltro(),
                    ListaTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro(),
                    ListaProbabilidad = _repProbabilidadRegistroPW.ObtenerTodoFiltro(),
                    ListaFaseOportunidad = _repFaseOportunidad.ObtenerTodoFiltro(),
                    ListaCategoriaOrigen = _repCategoriaOrigen.ObtenerTodoParaFitro()
                };

                return Ok(filtroAsesorCentroCosto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene en formato JSON los datos necesarios para rellenar grilla de verificación (Deprecada)
        /// </summary>
        /// <returns>Lista de GridVerificacioDTO en formato JSON</returns>
        [Route("[Action]/{IdAsesorCentroCosto}/{Tipo}")]
        [HttpGet]
        public ActionResult ObtenerGridVerificacionId(int IdAsesorCentroCosto, string Tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsesorFaseOpoProbabilidadDetalleRepositorio _repAsesorFaseOpoProbabilidadDetalle = new AsesorFaseOpoProbabilidadDetalleRepositorio(_integraDBContext);
                return Ok(_repAsesorFaseOpoProbabilidadDetalle.ObtenerGridVerificacionId(IdAsesorCentroCosto, Tipo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene en formato JSON los datos guardados para un determinado AsesorCentroCosto
        /// </summary>
        /// <returns>Lista de diferentes DTOs, varia dependiendo de lo que se desea retornar</returns>
        [Route("[Action]/{IdAsesorCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerDetallesPorAsesorCentroCosto(int IdAsesorCentroCosto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Nuevos repositorios
                AsesorAreaCapacitacionDetalleRepositorio _repAsesorAreaCapacitacionDetalle = new AsesorAreaCapacitacionDetalleRepositorio(_integraDBContext);
                AsesorSubAreaCapacitacionDetalleRepositorio _repAsesorSubAreaCapacitacionDetalle = new AsesorSubAreaCapacitacionDetalleRepositorio(_integraDBContext);
                AsesorProgramaGeneralDetalleRepositorio _repAsesorProgramaGeneralDetalle = new AsesorProgramaGeneralDetalleRepositorio(_integraDBContext);
                AsesorGrupoFiltroProgramaCriticoDetalleRepositorio _repAsesorGrupoFiltroProgramaCriticoDetalle = new AsesorGrupoFiltroProgramaCriticoDetalleRepositorio(_integraDBContext);
                AsesorTipoCategoriaOrigenDetalleRepositorio _repAsesorTipoCategoriaOrigenDetalle = new AsesorTipoCategoriaOrigenDetalleRepositorio(_integraDBContext);
                AsesorPaisDetalleRepositorio _repAsesorPaisDetalle = new AsesorPaisDetalleRepositorio(_integraDBContext);
                AsesorProbabilidadDetalleRepositorio _repAsesorProbabilidadDetalle = new AsesorProbabilidadDetalleRepositorio(_integraDBContext);

                var asesorAreaCapacitacionDetalle = _repAsesorAreaCapacitacionDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorSubAreaCapacitacionDetalle = _repAsesorSubAreaCapacitacionDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorProgramaGeneralDetalle = _repAsesorProgramaGeneralDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorGrupoFiltroProgramaCriticoDetalle = _repAsesorGrupoFiltroProgramaCriticoDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorTipoCategoriaOrigenDetalle = _repAsesorTipoCategoriaOrigenDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorPaisDetalle = _repAsesorPaisDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);
                var asesorProbabilidadDetalle = _repAsesorProbabilidadDetalle.GetBy(x => x.IdAsesorCentroCosto == IdAsesorCentroCosto);

                return Ok(new { asesorAreaCapacitacionDetalle, asesorSubAreaCapacitacionDetalle, asesorProgramaGeneralDetalle, asesorGrupoFiltroProgramaCriticoDetalle, asesorTipoCategoriaOrigenDetalle, asesorPaisDetalle, asesorProbabilidadDetalle });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza los datos en la tabla correspondiente y en las tablas hijas enlazadas a la tabla mkt.T_AsesorCentroCosto
        /// </summary>
        /// <returns>Lista de diferentes DTOs, varia dependiendo de lo que se desea retornar</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarActualizarTodo([FromBody] AsesorCentroCostoListaDetalleDTO JsonDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 15, 0)))
                {
                    AsesorCentroCostoRepositorio _repAsesorCentroCosto = new AsesorCentroCostoRepositorio(_integraDBContext);
                    AsesorAreaCapacitacionDetalleRepositorio _repAsesorAreaCapacitacionDetalle = new AsesorAreaCapacitacionDetalleRepositorio(_integraDBContext);
                    AsesorSubAreaCapacitacionDetalleRepositorio _repAsesorSubAreaCapacitacionDetalle = new AsesorSubAreaCapacitacionDetalleRepositorio(_integraDBContext);
                    AsesorProgramaGeneralDetalleRepositorio _repAsesorProgramaGeneralDetalle = new AsesorProgramaGeneralDetalleRepositorio(_integraDBContext);
                    AsesorGrupoFiltroProgramaCriticoDetalleRepositorio _repAsesorGrupoFiltroProgramaCriticoDetalle = new AsesorGrupoFiltroProgramaCriticoDetalleRepositorio(_integraDBContext);
                    AsesorTipoCategoriaOrigenDetalleRepositorio _repAsesorTipoCategoriaOrigenDetalle = new AsesorTipoCategoriaOrigenDetalleRepositorio(_integraDBContext);
                    AsesorPaisDetalleRepositorio _repAsesorPaisDetalle = new AsesorPaisDetalleRepositorio(_integraDBContext);
                    AsesorProbabilidadDetalleRepositorio _repAsesorProbabilidadDetalle = new AsesorProbabilidadDetalleRepositorio(_integraDBContext);

                    var listaAsesorAreaCapacitacionDetalle = _repAsesorAreaCapacitacionDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorAreaCapacitacionDetalle.Any())
                        _repAsesorAreaCapacitacionDetalle.Delete(listaAsesorAreaCapacitacionDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorSubAreaCapacitacionDetalle = _repAsesorSubAreaCapacitacionDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorSubAreaCapacitacionDetalle.Any())
                        _repAsesorSubAreaCapacitacionDetalle.Delete(listaAsesorSubAreaCapacitacionDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorProgramaGeneralDetalle = _repAsesorProgramaGeneralDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorProgramaGeneralDetalle.Any())
                        _repAsesorProgramaGeneralDetalle.Delete(listaAsesorProgramaGeneralDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorGrupoFiltroProgramaCriticoDetalle = _repAsesorGrupoFiltroProgramaCriticoDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorGrupoFiltroProgramaCriticoDetalle.Any())
                        _repAsesorGrupoFiltroProgramaCriticoDetalle.Delete(listaAsesorGrupoFiltroProgramaCriticoDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorTipoCategoriaOrigenDetalle = _repAsesorTipoCategoriaOrigenDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorTipoCategoriaOrigenDetalle.Any())
                        _repAsesorTipoCategoriaOrigenDetalle.Delete(listaAsesorTipoCategoriaOrigenDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorPaisDetalle = _repAsesorPaisDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorPaisDetalle.Any())
                        _repAsesorPaisDetalle.Delete(listaAsesorPaisDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    var listaAsesorProbabilidadDetalle = _repAsesorProbabilidadDetalle.GetBy(x => x.IdAsesorCentroCosto == JsonDTOs.AsesorCentroCosto.Id, x => new { x.Id });
                    if (listaAsesorProbabilidadDetalle.Any())
                        _repAsesorProbabilidadDetalle.Delete(listaAsesorProbabilidadDetalle.Select(x => x.Id), JsonDTOs.AsesorCentroCosto.NombreUsuario);

                    AsesorCentroCostoBO asesorCentroCostoTemp = null;
                    if (_repAsesorCentroCosto.Exist(JsonDTOs.AsesorCentroCosto.Id))
                    {
                        // Actualizar
                        asesorCentroCostoTemp = _repAsesorCentroCosto.FirstById(JsonDTOs.AsesorCentroCosto.Id);
                        asesorCentroCostoTemp.AsignacionMax = JsonDTOs.AsesorCentroCosto.AsignacionMaxima;
                        asesorCentroCostoTemp.IdPersonal = JsonDTOs.AsesorCentroCosto.IdPersonal;
                        asesorCentroCostoTemp.Habilitado = JsonDTOs.AsesorCentroCosto.Habilitado;
                        asesorCentroCostoTemp.Estado = true;
                        asesorCentroCostoTemp.FechaCreacion = DateTime.Now;
                        asesorCentroCostoTemp.FechaModificacion = DateTime.Now;
                        asesorCentroCostoTemp.UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario;
                        asesorCentroCostoTemp.UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario;
                        // Agregado
                        asesorCentroCostoTemp.AsignacionMaxBnc = JsonDTOs.AsesorCentroCosto.AsignacionMaximaBnc;
                        asesorCentroCostoTemp.AsignacionMin = JsonDTOs.AsesorCentroCosto.AsignacionMinima;
                        asesorCentroCostoTemp.AsignacionPais = JsonDTOs.AsesorCentroCosto.AsignacionPais;

                        _repAsesorCentroCosto.Update(asesorCentroCostoTemp);
                    }
                    else
                    {
                        // Insertar
                        asesorCentroCostoTemp = new AsesorCentroCostoBO
                        {
                            AsignacionMax = JsonDTOs.AsesorCentroCosto.AsignacionMaxima,
                            IdPersonal = JsonDTOs.AsesorCentroCosto.IdPersonal,
                            Habilitado = JsonDTOs.AsesorCentroCosto.Habilitado,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                            UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                            //Agregado
                            AsignacionMaxBnc = JsonDTOs.AsesorCentroCosto.AsignacionMaximaBnc,
                            AsignacionMin = JsonDTOs.AsesorCentroCosto.AsignacionMinima,
                            AsignacionPais = JsonDTOs.AsesorCentroCosto.AsignacionPais
                        };

                        _repAsesorCentroCosto.Insert(asesorCentroCostoTemp);
                    }

                    // Insercion de listas
                    if (JsonDTOs.ListaAsesorAreaDetalle != null && JsonDTOs.ListaAsesorAreaDetalle.Any())
                    {
                        foreach (var asesorAreaDetalle in JsonDTOs.ListaAsesorAreaDetalle)
                        {
                            var asesorAreaDetalleTemp = new AsesorAreaCapacitacionDetalleBO
                            {
                                IdAreaCapacitacion = asesorAreaDetalle.IdAreaCapacitacion,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = asesorAreaDetalle.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };

                            _repAsesorAreaCapacitacionDetalle.Insert(asesorAreaDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorSubAreaDetalle != null && JsonDTOs.ListaAsesorSubAreaDetalle.Any())
                    {
                        foreach (var asesorSubAreaDetalle in JsonDTOs.ListaAsesorSubAreaDetalle)
                        {
                            var asesorSubAreaDetalleTemp = new AsesorSubAreaCapacitacionDetalleBO
                            {
                                IdSubAreaCapacitacion = asesorSubAreaDetalle.IdSubAreaCapacitacion,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = asesorSubAreaDetalle.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };

                            _repAsesorSubAreaCapacitacionDetalle.Insert(asesorSubAreaDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorPGeneralDetalle != null && JsonDTOs.ListaAsesorPGeneralDetalle.Any())
                    {
                        foreach (var asesorPGeneralDetalle in JsonDTOs.ListaAsesorPGeneralDetalle)
                        {
                            var asesorPGeneralDetalleTemp = new AsesorProgramaGeneralDetalleBO
                            {
                                IdPGeneral = asesorPGeneralDetalle.IdProgramaGeneral,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = asesorPGeneralDetalle.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };

                            _repAsesorProgramaGeneralDetalle.Insert(asesorPGeneralDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorGrupoFiltroPCriticoDetalle != null && JsonDTOs.ListaAsesorGrupoFiltroPCriticoDetalle.Any())
                    {
                        foreach (var asesorGrupoFiltroPCriticoDetalle in JsonDTOs.ListaAsesorGrupoFiltroPCriticoDetalle)
                        {
                            var asesorGrupoFiltroPCriticoDetalleTemp = new AsesorGrupoFiltroProgramaCriticoDetalleBO
                            {
                                IdGrupoFiltroProgramaCritico = asesorGrupoFiltroPCriticoDetalle.IdGrupoProgramaCritico,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = asesorGrupoFiltroPCriticoDetalle.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };

                            _repAsesorGrupoFiltroProgramaCriticoDetalle.Insert(asesorGrupoFiltroPCriticoDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorGrupoDetalle != null && JsonDTOs.ListaAsesorGrupoDetalle.Any())
                    {
                        foreach (var asesorGrupoDetalle in JsonDTOs.ListaAsesorGrupoDetalle)
                        {
                            var asesorGrupoDetalleTemp = new AsesorTipoCategoriaOrigenDetalleBO
                            {
                                IdTipoCategoriaOrigen = asesorGrupoDetalle.IdGrupo,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = asesorGrupoDetalle.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };

                            _repAsesorTipoCategoriaOrigenDetalle.Insert(asesorGrupoDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorPaisDetalle != null && JsonDTOs.ListaAsesorPaisDetalle.Any())
                    {
                        foreach (var item in JsonDTOs.ListaAsesorPaisDetalle)
                        {
                            var asesorPaisDetalleTemp = new AsesorPaisDetalleBO()
                            {
                                IdPais = item.IdPais,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = item.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };
                            _repAsesorPaisDetalle.Insert(asesorPaisDetalleTemp);
                        }
                    }
                    if (JsonDTOs.ListaAsesorProbabilidadDetalle != null && JsonDTOs.ListaAsesorProbabilidadDetalle.Any())
                    {
                        foreach (var item in JsonDTOs.ListaAsesorProbabilidadDetalle)
                        {
                            var asesorProbabilidadDetalleTemp = new AsesorProbabilidadDetalleBO()
                            {
                                IdProbabilidadRegistroPw = item.IdProbabilidadRegistroPw,
                                IdAsesorCentroCosto = asesorCentroCostoTemp.Id,
                                Prioridad = item.Prioridad,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = JsonDTOs.AsesorCentroCosto.NombreUsuario,
                                UsuarioModificacion = JsonDTOs.AsesorCentroCosto.NombreUsuario
                            };
                            _repAsesorProbabilidadDetalle.Insert(asesorProbabilidadDetalleTemp);
                        }
                    }

                    scope.Complete();
                    return Ok(asesorCentroCostoTemp);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 21/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de los Centros de Costo mediante el Area y SubArea (Actualmente deprecada)
        /// </summary>
        /// <returns>JSON con la lista de Centros de Costo que cumplen las condiciones</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorAreaSubArea([FromBody] ListasAreaSubAreaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                return Ok(_repCentroCosto.ObtenerCentroCostoPorAreaSubArea(Json.ListaArea, Json.ListaSubarea));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

