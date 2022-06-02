using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/GrupoFiltroProgramaCritico
    /// Autor: Gian Miranda
    /// Fecha: 23/04/2021
    /// <summary>
    /// Gestiona los endpoints para la manipulacion de los grupos de programas criticos y su asociacion
    /// </summary>
    [Route("api/GrupoFiltroProgramaCritico")]
    [ApiController]
    public class GrupoFiltroProgramaCriticoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        private PgeneralRepositorio _repPGeneral;
        private AreaCapacitacionRepositorio _repAreaCapacitacion;
        private SubAreaCapacitacionRepositorio _repSubAreaCapacitacion;
        private GrupoFiltroProgramaCriticoRepositorio _repGrupoFiltroProgramaCritico;
        private PersonalRepositorio _repPersonal;

        public GrupoFiltroProgramaCriticoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repPGeneral = new PgeneralRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
            _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
            _repGrupoFiltroProgramaCritico = new GrupoFiltroProgramaCriticoRepositorio(_integraDBContext);
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la grilla inicial para el modulo
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase GrupoFiltroProgramaCriticoDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                return Ok(_repGrupoFiltroProgramaCritico.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de los grupos de filtro programa critico
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase DatosPersonalAsesorDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerComboGrupoFiltroProgramaCritico()
        {
            try
            {
                List<DatosPersonalAsesorDTO> personalAsesores = new List<DatosPersonalAsesorDTO>();
                personalAsesores = _repPersonal.ObtenerTodoPersonalAsesoresFiltro();

                return Ok(personalAsesores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los asesores por grupo de programa critico
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase DatosPersonalAsesorPorGrupoIdDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerAsesoresPorGrupoId(int IdGrupo)
        {
            try
            {
                List<DatosPersonalAsesorPorGrupoIdDTO> listaPersonalAsesoresGrupoId = new List<DatosPersonalAsesorPorGrupoIdDTO>();
                listaPersonalAsesoresGrupoId = _repPersonal.ObtenerAsesoresPorGrupoId(IdGrupo);

                return Ok(listaPersonalAsesoresGrupoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos de areas y subareas de capacitacion
        /// </summary>
        /// <returns>Response 200 con la lista de objetos de clase GrupoFiltroProgramaCriticoCombosDTO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosAreaSubArea()
        {
            try
            {
                GrupoFiltroProgramaCriticoCombosDTO combos = new GrupoFiltroProgramaCriticoCombosDTO()
                {
                    ListaAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro(),
                    ListaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerTodoFiltro()
                };

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los centros de costo asociados segun el id de grupo de programa critico
        /// </summary>
        /// <param name="IdGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Response 200 con el objeto anonimo (Lista de objetos de clase CentroCostoSubAreaDTO, Lista de objetos de clase CentroCostoSubAreaDTO), caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerCentroCostoAsociados(int IdGrupo)
        {
            try
            {
                GrupoFiltroProgramaCriticoCentroCostoRepositorio _repGrupoFiltroProgramaCriticoCentroCosto = new GrupoFiltroProgramaCriticoCentroCostoRepositorio(_integraDBContext);
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);

                var listaCentroCosto = _repCentroCosto.ObtenerCentroCostoPorSubArea(IdGrupo);
                var listaCentroCostoPorGrupo = _repGrupoFiltroProgramaCriticoCentroCosto.ObtenerPorIdGrupo(IdGrupo);

                return Ok(new { ListaCentroCosto = listaCentroCosto, ListaCentroCostoPorGrupo = listaCentroCosto });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los programas generales asociados segun el id de grupo de programa critico
        /// </summary>
        /// <param name="IdGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Response 200 con el objeto anonimo (Lista de objetos de clase PGeneralSubAreaDTO, Lista de objetos de clase PGeneralSubAreaDTO), caso contrario response 400 con el mensaje de excepcion</returns>
        [Route("[Action]/{IdGrupo}")]
        [HttpGet]
        public ActionResult ObtenerPGeneralAsociado(int IdGrupo)
        {
            try
            {
                GrupoFiltroProgramaCriticoPgeneralRepositorio _repGrupoFiltroProgramaCriticoPGeneral = new GrupoFiltroProgramaCriticoPgeneralRepositorio(_integraDBContext);
                var listaPGeneral = _repPGeneral.ObtenerPGeneralProgramaCriticoPorSubArea();
                var listaPGeneralPorGrupo = _repGrupoFiltroProgramaCriticoPGeneral.ObtenerPorIdGrupo(IdGrupo);

                return Ok(new { ListaPGeneral = listaPGeneral, ListaPGeneralPorGrupo = listaPGeneralPorGrupo });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo grupo de programa critico con sus asesores asociados
        /// </summary>
        /// <param name="Json">Objeto de clase CompuestoGrupoFiltroProgramaCriticoDTO</param>
        /// <returns>Response 200 con el objeto de clase GrupoFiltroProgramaCriticoBO, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoFiltroProgramaCriticoBO grupoFiltro = new GrupoFiltroProgramaCriticoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    grupoFiltro.Nombre = Json.GrupoFiltroProgramaCritico.Nombre;
                    grupoFiltro.Descripcion = Json.GrupoFiltroProgramaCritico.Descripcion;
                    grupoFiltro.Estado = true;
                    grupoFiltro.UsuarioCreacion = Json.Usuario;
                    grupoFiltro.UsuarioModificacion = Json.Usuario;
                    grupoFiltro.FechaCreacion = DateTime.Now;
                    grupoFiltro.FechaModificacion = DateTime.Now;

                    grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor = new List<GrupoFiltroProgramaCriticoPorAsesorBO>();

                    foreach (var item in Json.GrupoFiltroProgramaCriticoPorAsesor)
                    {
                        GrupoFiltroProgramaCriticoPorAsesorBO grupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesorBO();
                        grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                        grupoFiltroProgramaCriticoPorAsesor.UsuarioCreacion = Json.Usuario;
                        grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                        grupoFiltroProgramaCriticoPorAsesor.FechaCreacion = DateTime.Now;
                        grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                        grupoFiltroProgramaCriticoPorAsesor.Estado = true;

                        grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor.Add(grupoFiltroProgramaCriticoPorAsesor);
                    }
                    _repGrupoFiltroProgramaCritico.Insert(grupoFiltro);
                    scope.Complete();
                }

                return Ok(grupoFiltro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un grupo de programa critico con sus asesores asociados
        /// </summary>
        /// <param name="Json">Objeto de clase CompuestoGrupoFiltroProgramaCriticoDTO</param>
        /// <returns>Response 200 con valor true, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                GrupoFiltroProgramaCriticoRepositorio _repGrupoFiltroProgramaCritico = new GrupoFiltroProgramaCriticoRepositorio(contexto);
                GrupoFiltroProgramaCriticoPorAsesorRepositorio _repGrupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesorRepositorio(contexto);
                GrupoFiltroProgramaCriticoBO grupoFiltro = new GrupoFiltroProgramaCriticoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repGrupoFiltroProgramaCritico.Exist(Json.GrupoFiltroProgramaCritico.Id))
                    {
                        _repGrupoFiltroProgramaCriticoPorAsesor.EliminacionLogicoPorGrupoFiltro(Json.GrupoFiltroProgramaCritico.Id, Json.Usuario, Json.GrupoFiltroProgramaCriticoPorAsesor);

                        grupoFiltro = _repGrupoFiltroProgramaCritico.FirstById(Json.GrupoFiltroProgramaCritico.Id);
                        grupoFiltro.Nombre = Json.GrupoFiltroProgramaCritico.Nombre;
                        grupoFiltro.Descripcion = Json.GrupoFiltroProgramaCritico.Descripcion;
                        grupoFiltro.UsuarioModificacion = Json.Usuario;
                        grupoFiltro.FechaModificacion = DateTime.Now;

                        grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor = new List<GrupoFiltroProgramaCriticoPorAsesorBO>();

                        foreach (var item in Json.GrupoFiltroProgramaCriticoPorAsesor)
                        {
                            GrupoFiltroProgramaCriticoPorAsesorBO grupoFiltroProgramaCriticoPorAsesor;
                            if (_repGrupoFiltroProgramaCriticoPorAsesor.Exist(x => x.IdGrupoFiltroProgramaCritico == Json.IdGrupo && x.IdPersonal == item))
                            {
                                grupoFiltroProgramaCriticoPorAsesor = _repGrupoFiltroProgramaCriticoPorAsesor.FirstBy(x => x.IdPersonal == item && x.IdGrupoFiltroProgramaCritico == Json.IdGrupo);
                                grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                grupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesorBO();
                                grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioCreacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.FechaCreacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPorAsesor.Estado = true;
                            }
                            grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor.Add(grupoFiltroProgramaCriticoPorAsesor);
                        }

                        _repGrupoFiltroProgramaCritico.Update(grupoFiltro);
                        scope.Complete();
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina un grupo de programa critico con sus asesores asociados
        /// </summary>
        /// <param name="Json">Objeto de clase CompuestoGrupoFiltroProgramaCriticoDTO</param>
        /// <returns>Response 200 con valor true, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Eliminar(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoFiltroProgramaCriticoPorAsesorRepositorio _repGrupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesorRepositorio(_integraDBContext);
                GrupoFiltroProgramaCriticoPgeneralRepositorio _repGrupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneralRepositorio(_integraDBContext);

                if (_repGrupoFiltroProgramaCritico.Exist(Json.GrupoFiltroProgramaCritico.Id))
                {
                    var hijosGrupoAsesores = _repGrupoFiltroProgramaCriticoPorAsesor.GetBy(x => x.IdGrupoFiltroProgramaCritico == Json.GrupoFiltroProgramaCritico.Id);
                    var hijosGrupoPgeneral = _repGrupoFiltroProgramaCriticoPgeneral.GetBy(x => x.IdGrupoFiltroProgramaCritico == Json.GrupoFiltroProgramaCritico.Id);

                    foreach (var hijo in hijosGrupoAsesores)
                        _repGrupoFiltroProgramaCriticoPorAsesor.Delete(hijo.Id, Json.Usuario);

                    foreach (var hijo in hijosGrupoPgeneral)
                        _repGrupoFiltroProgramaCriticoPgeneral.Delete(hijo.Id, Json.Usuario);

                    _repGrupoFiltroProgramaCritico.Delete(Json.GrupoFiltroProgramaCritico.Id, Json.Usuario);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 23/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda la asociacion de asesores de un grupo de programa critico con sus asesores asociados
        /// </summary>
        /// <param name="Json">Objeto de clase AsociacionGrupoFiltroPGeneralDTO</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GuardarAsociacion(AsociacionGrupoFiltroPGeneralDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoFiltroProgramaCriticoPgeneralRepositorio _repGrupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneralRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        _repGrupoFiltroProgramaCriticoPgeneral.EliminadoLogicoPorGrupo(Json.IdGrupo, Json.Usuario, Json.ListaPGeneral);

                        GrupoFiltroProgramaCriticoPgeneralBO grupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneralBO();

                        foreach (var item in Json.ListaPGeneral)
                        {
                            grupoFiltroProgramaCriticoPgeneral = _repGrupoFiltroProgramaCriticoPgeneral.FirstBy(x => x.IdGrupoFiltroProgramaCritico == Json.IdGrupo && x.IdPgeneral == item.Id);
                            if (grupoFiltroProgramaCriticoPgeneral == null)
                            {
                                grupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneralBO();
                                grupoFiltroProgramaCriticoPgeneral.IdPGeneral = item.Id;
                                grupoFiltroProgramaCriticoPgeneral.IdGrupoFiltroProgramaCritico = Json.IdGrupo;
                                grupoFiltroProgramaCriticoPgeneral.Estado = true;
                                grupoFiltroProgramaCriticoPgeneral.UsuarioCreacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.FechaCreacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPgeneral.FechaModificacion = DateTime.Now;
                                _repGrupoFiltroProgramaCriticoPgeneral.Insert(grupoFiltroProgramaCriticoPgeneral);
                            }
                            else
                            {
                                grupoFiltroProgramaCriticoPgeneral.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPgeneral.FechaModificacion = DateTime.Now;
                                _repGrupoFiltroProgramaCriticoPgeneral.Update(grupoFiltroProgramaCriticoPgeneral);
                            }
                        }
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        return BadRequest(ex);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class ValidadorGrupoFiltroProgramaCriticoDTO : AbstractValidator<GrupoFiltroProgramaCriticoBO>
        {
            public static ValidadorGrupoFiltroProgramaCriticoDTO Current = new ValidadorGrupoFiltroProgramaCriticoDTO();
            public ValidadorGrupoFiltroProgramaCriticoDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");
                RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio");
            }
        }
    }
}
