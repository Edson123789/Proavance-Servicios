using System;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Planificacion/SubAreaCapacitacion
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura las acciones para todo lo relacionado con la configuracion de relacion entre un asesor y su chat correspondiente
    /// </summary>
    
    [Route("api/AsesorChatDetalle")]
    public class AsesorChatDetalleController : Controller
    {
        [Route("[action]/{IdPais}/{IdProgramaGeneral}")]
        [HttpGet]
        public ActionResult ObtenerAsesorChatDetallePorPaisyProgramaGeneral(int IdPais, int IdProgramaGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsesorChatDetalleBO asesorChatDetalle = new AsesorChatDetalleBO();
				AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio();
				var res = _repAsesorChatDetalle.ObtenerAsesorChatDetallePorPaisyProgramaGeneral(IdPais, IdProgramaGeneral);
				return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 23/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Id Alumno Chat Soporte
        /// </summary>
        /// <returns>Objeto:IdAlumnoSoporteChatDTO<returns>
        [Route("[action]/{correo}")]
        [HttpGet]
        public ActionResult ObtenerIdAlumnoChatSoporte(string correo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                AsesorChatDetalleRepositorio repAsesorChatDetalle = new AsesorChatDetalleRepositorio();
                var res = repAsesorChatDetalle.ObtenerIdAlumnoChatSoporte(correo);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerIdPersonalSoporte()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio();
                var res = _repAsesorChatDetalle.ObtenerIdPersonalSoporte();
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerListaProgramasAsignadosPorAsesor(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
				AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio();
				//AsesorChatDetalleBO asesorChats = new AsesorChatDetalleBO();
				var res = _repAsesorChatDetalle.ObtenerListaProgramasAsignadosPorAsesor(IdPersonal);

				return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de configuracion de los chats
        /// </summary>
        /// <param name="IdAsesorChat">Id del asesorchat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Objeto de clase AsesorChatDetalleDetalleDTO</returns>
        [Route("[action]/{IdAsesorChat}")]
        [HttpGet]
        public ActionResult ObtenerDatosConfiguracionChat(int IdAsesorChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio();
                AsesorChatDetalleDetalleDTO asesorChatDetalleDetalle = new AsesorChatDetalleDetalleDTO()
                {
                    listadoIdsPais = _repAsesorChatDetalle.ObtenerPaisesPorIdAsesorChat(IdAsesorChat),
                    listadoIdsProgramaGeneral = _repAsesorChatDetalle.ObtenerProgramasGeneralesPorIdAsesorChat(IdAsesorChat),
                    listadoIdsAreaCapacitacion = _repAsesorChatDetalle.ObtenerAreasCapacitacionPorIdAsesorChat(IdAsesorChat),
                    listadoIdsSubAreaCapacitacion = _repAsesorChatDetalle.ObtenerSubAreasCapacitacionPorIdAsesorChat(IdAsesorChat)
                };
                return Ok(asesorChatDetalleDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta los detalles del AsesorChat (grillas)
        /// </summary>
        /// <param name="DTO">Objeto de clase CompuestoInsertarAsesorChatDTO</param>
        /// <returns>Response 200 con el objeto de clase AsesorChatDetalleDetalleDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDetalles([FromBody] CompuestoInsertarAsesorChatDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();

                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio(_integraDBContext);
                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio(_integraDBContext);
                
                using (TransactionScope scope = new TransactionScope())
                {
                    AsesorChatBO asesorChat = new AsesorChatBO()
                    {
                        IdPersonal = DTO.AsesorChat.IdPersonal,
                        NombreAsesor = DTO.AsesorChat.NombreAsesor,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = DTO.AsesorChat.NombreUsuario,
                        UsuarioModificacion = DTO.AsesorChat.NombreUsuario
                    };

                    if (DTO.ListaProgramaGeneral == null || DTO.ListaProgramaGeneral.Count == 0)
                    {
                        if (DTO.ListaSubAreasCapacitacion == null || DTO.ListaSubAreasCapacitacion.Count == 0)
                        {
                            if (DTO.ListaAreasCapacitacion == null || DTO.ListaAreasCapacitacion.Count == 0)
                            {
                            //si no selecciono ninguna area /subarea/programa
                                DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosIds().Select(x => x.Id).ToList();
                            }
                            else//selecciono una area
                            {
                                DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosPorIdArea(DTO.ListaAreasCapacitacion).Select(x => x.Id).ToList();
                            }
                        }
                        else
                        {
                            DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosPorIdSubArea(DTO.ListaSubAreasCapacitacion).Select(x => x.Id).ToList();
                        }
                    }
                    if (DTO.ListaPais == null || DTO.ListaPais.Count == 0)
                    {
                        DTO.ListaPais = _repPais.ObtenerTodoCodigoPais().Select(x => x.Codigo ).ToList();
                    }

                    if (!asesorChat.HasErrors)
                    {
                        //Insertamos el asesor chat
                        _repAsesorChat.Insert(asesorChat);
                        //Insertamos los detalles
                        //_repAsesorChatDetalle.ActualizarAsesorChaDetalleYLog(asesorChat.Id, DTO.AsesorChat.NombreUsuario, String.Join(",", DTO.ListaProgramaGeneral), String.Join(",", DTO.ListaPais ));
                        _repAsesorChatDetalle.ActualizarAsesorChaDetalleYLog(asesorChat.Id, asesorChat.IdPersonal.Value, DTO.AsesorChat.NombreUsuario, String.Join(",", DTO.ListaProgramaGeneral), String.Join(",", DTO.ListaPais));

                    }
                    scope.Complete();
                    return Ok(asesorChat);
                }
                //return Json(asesorChat.InsertDetalles(DTO.ListaProgramaGeneral, DTO.ListaPais));//TODO
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Version: 1.0
        /// <summary>
        /// Inserta los detalles del AsesorChat (grillas)
        /// </summary>
        /// <param name="DTO">Objeto de clase CompuestoInsertarAsesorChatDTO</param>
        /// <returns>Response 200 con el objeto de clase AsesorChatDetalleDetalleDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDetalles([FromBody] CompuestoInsertarAsesorChatDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();

                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                AsesorChatDetalleRepositorio _repAsesorChatDetalle = new AsesorChatDetalleRepositorio(_integraDBContext);
                AsesorChatRepositorio _repAsesorChat = new AsesorChatRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repAsesorChat.Exist(DTO.AsesorChat.Id))
                    {
                        var asesorChat = _repAsesorChat.FirstById(DTO.AsesorChat.Id);
                        asesorChat.IdPersonal = DTO.AsesorChat.IdPersonal;
                        asesorChat.NombreAsesor = DTO.AsesorChat.NombreAsesor;
                        asesorChat.FechaModificacion = DateTime.Now;
                        asesorChat.UsuarioModificacion = DTO.AsesorChat.NombreUsuario;

                        if (DTO.ListaProgramaGeneral == null || DTO.ListaProgramaGeneral.Count == 0)
                        {
                            if (DTO.ListaSubAreasCapacitacion == null || DTO.ListaSubAreasCapacitacion.Count == 0)
                            {
                                if (DTO.ListaAreasCapacitacion == null || DTO.ListaAreasCapacitacion.Count == 0)
                                {
                                    //si no selecciono ninguna area /subarea/programa
                                    DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosIds().Select(x => x.Id).ToList();
                                }
                                else//selecciono una area
                                {
                                    DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosPorIdArea(DTO.ListaAreasCapacitacion).Select(x => x.Id).ToList();
                                }
                            }
                            else
                            {
                                DTO.ListaProgramaGeneral = _repPGeneral.ObtenerTodosPorIdSubArea(DTO.ListaSubAreasCapacitacion).Select(x => x.Id).ToList();
                            }
                        }
                        if (DTO.ListaPais == null || DTO.ListaPais.Count == 0)
                        {
                            DTO.ListaPais = _repPais.ObtenerTodoCodigoPais().Select(x => x.Codigo).ToList();
                        }

                        if (!asesorChat.HasErrors)
                        {
                            _repAsesorChatDetalle.EliminarAsesorChatDetalle(asesorChat.Id, DTO.AsesorChat.NombreUsuario);
                            //Actualizamos el asesor chat
                            _repAsesorChat.Update(asesorChat);
                            //Insertamos los detalles
                            _repAsesorChatDetalle.ActualizarAsesorChaDetalleYLog(asesorChat.Id, asesorChat.IdPersonal.Value, DTO.AsesorChat.NombreUsuario, String.Join(",", DTO.ListaProgramaGeneral), String.Join(",", DTO.ListaPais));
                        }
                        //return Json(asesorChat.InsertDetalles(DTO.ListaProgramaGeneral, DTO.ListaPais));//TODO
                        scope.Complete();
                        return Ok(asesorChat);
                    }
                    else
                    {
                        return BadRequest("No existe el asesor chat");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]/{idProgramaGeneralAlumno}/{idcursoprogramageneralalumno}/{idCentroCosto}/{idMatriculaCabecera}/{IdCoordinadora}/{idCapitulo}/{idSesion}/{idAlumno}")]
        [HttpGet]
        public ActionResult InsertarObtenerDatosChatAulaVirtual(int idProgramaGeneralAlumno, int idcursoprogramageneralalumno, int idCentroCosto, int idMatriculaCabecera, int IdCoordinadora, int idCapitulo, int idSesion, int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext integraDBContext = new integraDBContext();
                AsesorChatDetalleRepositorio repAsesorChatDetalle = new AsesorChatDetalleRepositorio(integraDBContext);
                InformacionAlumnoChatRepositorio repInformacionAlumnoChat = new InformacionAlumnoChatRepositorio(integraDBContext);
                InformacionAlumnoChatLogRepositorio repInformacionAlumnoChatLog = new InformacionAlumnoChatLogRepositorio(integraDBContext);
                PgeneralRepositorio reppgeneral = new PgeneralRepositorio();
                InformacionAlumnoChatBO avance = new InformacionAlumnoChatBO();
                InformacionAlumnoChatLogBO avanceLog = new InformacionAlumnoChatLogBO();
                InformacionChatSoporteAlumnoDTO ResultadoInformacionAlumno = new InformacionChatSoporteAlumnoDTO();
                //Si al menos llega un campo
                //if (idProgramaGeneralAlumno != 0 || idcursoprogramageneralalumno != 0 || idCentroCosto != 0 || idMatriculaCabecera != 0 || IdCoordinadora != 0 || idCapitulo != 0 || idSesion != 0 || idAlumno != 0)
                if (idProgramaGeneralAlumno != 0 || idcursoprogramageneralalumno != 0 || idCentroCosto != 0 || idMatriculaCabecera != 0 || IdCoordinadora != 0 || idCapitulo != 0 || idSesion != 0)
                {
                    var informacionAlumno = repInformacionAlumnoChat.FirstBy(x => x.IdAlumno == idAlumno); //Recupero el registro de la tabla padre segun el idalumno                   
                    using (TransactionScope scope = new TransactionScope())
                    {
                        
                        if (informacionAlumno != null)//Si se tiene registro en la tabla padre se actualiza el registro segun los nuevos campos y se inserta en el log
                        {
                            avance = repInformacionAlumnoChat.FirstById(informacionAlumno.Id);
                            avance.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avance.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avance.IdCentroCosto = idCentroCosto;
                            avance.IdMatriculaCabecera = idMatriculaCabecera;
                            avance.IdAlumno = idAlumno;
                            avance.IdCoordinadora = IdCoordinadora;
                            avance.IdCapitulo = idCapitulo;
                            avance.IdSesion = idSesion;
                            repInformacionAlumnoChat.Update(avance);

                            avanceLog.IdInformacionAlumnoChatLog = informacionAlumno.Id;
                            avanceLog.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avanceLog.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avanceLog.IdCentroCosto = idCentroCosto;
                            avanceLog.IdMatriculaCabecera = idMatriculaCabecera;
                            avanceLog.IdAlumno = idAlumno;
                            avanceLog.IdCoordinadora = IdCoordinadora;
                            avanceLog.IdCapitulo = idCapitulo;
                            avanceLog.IdSesion = idSesion;
                            avanceLog.Estado = true;
                            avanceLog.UsuarioCreacion = "system";
                            avanceLog.FechaCreacion = DateTime.Now;
                            avanceLog.UsuarioModificacion = "system";
                            avanceLog.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChatLog.Insert(avanceLog);
                        }
                        else
                        {                            
                            avance.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avance.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avance.IdCentroCosto = idCentroCosto;
                            avance.IdMatriculaCabecera = idMatriculaCabecera;
                            avance.IdAlumno = idAlumno;
                            avance.IdCoordinadora = IdCoordinadora;
                            avance.IdCapitulo = idCapitulo;
                            avance.IdSesion = idSesion;
                            avance.Estado = true;
                            avance.UsuarioCreacion = "system";
                            avance.FechaCreacion = DateTime.Now;
                            avance.UsuarioModificacion = "system";
                            avance.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChat.Insert(avance);

                            avanceLog.IdInformacionAlumnoChatLog = avance.Id;
                            avanceLog.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avanceLog.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avanceLog.IdCentroCosto = idCentroCosto;
                            avanceLog.IdMatriculaCabecera = idMatriculaCabecera;
                            avanceLog.IdAlumno = idAlumno;
                            avanceLog.IdCoordinadora = IdCoordinadora;
                            avanceLog.IdCapitulo = idCapitulo;
                            avanceLog.IdSesion = idSesion;
                            avanceLog.Estado = true;
                            avanceLog.UsuarioCreacion = "system";
                            avanceLog.FechaCreacion = DateTime.Now;
                            avanceLog.UsuarioModificacion = "system";
                            avanceLog.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChatLog.Insert(avanceLog);
                        }
                        scope.Complete();
                    }
                    
                    var obtenerInformacionAlumno = repInformacionAlumnoChat.obtenerInformacionAlumnoChatSoporte(avance.Id);                    
                    if (idcursoprogramageneralalumno != 0 && idSesion == 0)
                    {
                        var pgeneralCurso = reppgeneral.FirstById(idcursoprogramageneralalumno);
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                        ResultadoInformacionAlumno.CentroCosto = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = null;
                        ResultadoInformacionAlumno.Sesion = null;
                    }
                    if (idcursoprogramageneralalumno != 0 && idSesion != 0)
                    {
                        var pgeneralCurso = reppgeneral.FirstById(idcursoprogramageneralalumno);
                        var obtenerCapitulo = repInformacionAlumnoChat.obtenerCapituloAlumnoChatSoporte(idcursoprogramageneralalumno, idSesion);
                        var obtenerSesion = repInformacionAlumnoChat.obtenerSesionAlumnoChatSoporte(idcursoprogramageneralalumno, idSesion);
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                        ResultadoInformacionAlumno.CentroCosto = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = obtenerCapitulo.Capitulo;
                        ResultadoInformacionAlumno.Sesion = obtenerSesion.Sesion;
                    }
                    if(idcursoprogramageneralalumno == 0 && idSesion == 0)
                    {                        
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = null;
                        ResultadoInformacionAlumno.CentroCosto = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerInformacionAlumno == null ? null : obtenerInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = null;
                        ResultadoInformacionAlumno.Sesion = null;
                    }
                    if (idAlumno != 0) 
                    {
                        var obtenerNombreAlumno = repInformacionAlumnoChat.obtenerNombreAlumnoChatSoporte(idAlumno);
                        ResultadoInformacionAlumno.NombreAlumno = obtenerNombreAlumno.NombreAlumno;
                    }
                    



                }
                else
                {
                    var informacionAlumno = repInformacionAlumnoChat.FirstBy(x => x.IdAlumno == idAlumno); //Recupero el registro de la tabla padre segun el idalumno                   
                    using (TransactionScope scope = new TransactionScope())
                    {

                        if (informacionAlumno != null)//Si se tiene registro en la tabla padre se actualiza el registro segun los nuevos campos y se inserta en el log
                        {
                            avance = repInformacionAlumnoChat.FirstById(informacionAlumno.Id);
                            avance.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avance.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avance.IdCentroCosto = idCentroCosto;
                            avance.IdMatriculaCabecera = idMatriculaCabecera;
                            avance.IdAlumno = idAlumno;
                            avance.IdCoordinadora = IdCoordinadora;
                            avance.IdCapitulo = idCapitulo;
                            avance.IdSesion = idSesion;
                            repInformacionAlumnoChat.Update(avance);

                            avanceLog.IdInformacionAlumnoChatLog = informacionAlumno.Id;
                            avanceLog.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avanceLog.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avanceLog.IdCentroCosto = idCentroCosto;
                            avanceLog.IdMatriculaCabecera = idMatriculaCabecera;
                            avanceLog.IdAlumno = idAlumno;
                            avanceLog.IdCoordinadora = IdCoordinadora;
                            avanceLog.IdCapitulo = idCapitulo;
                            avanceLog.IdSesion = idSesion;
                            avanceLog.Estado = true;
                            avanceLog.UsuarioCreacion = "system";
                            avanceLog.FechaCreacion = DateTime.Now;
                            avanceLog.UsuarioModificacion = "system";
                            avanceLog.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChatLog.Insert(avanceLog);
                        }
                        else
                        {
                            avance.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avance.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avance.IdCentroCosto = idCentroCosto;
                            avance.IdMatriculaCabecera = idMatriculaCabecera;
                            avance.IdAlumno = idAlumno;
                            avance.IdCoordinadora = IdCoordinadora;
                            avance.IdCapitulo = idCapitulo;
                            avance.IdSesion = idSesion;
                            avance.Estado = true;
                            avance.UsuarioCreacion = "system";
                            avance.FechaCreacion = DateTime.Now;
                            avance.UsuarioModificacion = "system";
                            avance.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChat.Insert(avance);

                            avanceLog.IdInformacionAlumnoChatLog = avance.Id;
                            avanceLog.IdProgramaGeneralPadre = idProgramaGeneralAlumno;
                            avanceLog.IdProgramaGeneralHijo = idcursoprogramageneralalumno;
                            avanceLog.IdCentroCosto = idCentroCosto;
                            avanceLog.IdMatriculaCabecera = idMatriculaCabecera;
                            avanceLog.IdAlumno = idAlumno;
                            avanceLog.IdCoordinadora = IdCoordinadora;
                            avanceLog.IdCapitulo = idCapitulo;
                            avanceLog.IdSesion = idSesion;
                            avanceLog.Estado = true;
                            avanceLog.UsuarioCreacion = "system";
                            avanceLog.FechaCreacion = DateTime.Now;
                            avanceLog.UsuarioModificacion = "system";
                            avanceLog.FechaModificacion = DateTime.Now;
                            repInformacionAlumnoChatLog.Insert(avanceLog);
                        }
                        scope.Complete();
                    }
                    var obtenerUltimaInformacionAlumno = repInformacionAlumnoChat.obtenerUltimaInformacionAlumno(idAlumno);
                    var logInformacion = repInformacionAlumnoChat.obtenerLogInformacion(idAlumno);
                    if (logInformacion == null)
                    {
                        if (idAlumno != 0)
                        {
                            var obtenerNombreAlumno = repInformacionAlumnoChat.obtenerNombreAlumnoChatSoporte(idAlumno);
                            ResultadoInformacionAlumno.NombreAlumno = obtenerNombreAlumno.NombreAlumno;
                        }
                        ResultadoInformacionAlumno.CodigoMatricula = null;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = null;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = null;
                        ResultadoInformacionAlumno.CentroCosto = null;
                        ResultadoInformacionAlumno.Coordinadora = null;
                        ResultadoInformacionAlumno.Capitulo = null;
                        ResultadoInformacionAlumno.Sesion = null;
                    }
                    else
                    {
                        if (logInformacion.IdProgramaGeneral_Hijo != 0 && logInformacion.IdSesion == 0)
                        {
                            var pgeneralCurso = reppgeneral.FirstById(logInformacion.IdProgramaGeneral_Hijo);
                            ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                            ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                            ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                            ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                            ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                            ResultadoInformacionAlumno.Capitulo = null;
                            ResultadoInformacionAlumno.Sesion = null;
                        }
                        if (logInformacion.IdProgramaGeneral_Hijo != 0 && logInformacion.IdSesion != 0)
                        {
                            var pgeneralCurso = reppgeneral.FirstById(logInformacion.IdProgramaGeneral_Hijo);
                            var obtenerCapitulo = repInformacionAlumnoChat.obtenerCapituloAlumnoChatSoporte(logInformacion.IdProgramaGeneral_Hijo, logInformacion.IdSesion);
                            var obtenerSesion = repInformacionAlumnoChat.obtenerSesionAlumnoChatSoporte(logInformacion.IdProgramaGeneral_Hijo, logInformacion.IdSesion);
                            ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                            ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                            ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                            ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                            ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                            ResultadoInformacionAlumno.Capitulo = obtenerCapitulo.Capitulo;
                            ResultadoInformacionAlumno.Sesion = obtenerSesion.Sesion;
                        }
                        if (logInformacion.IdProgramaGeneral_Hijo == 0 && logInformacion.IdSesion == 0)
                        {
                            ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                            ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                            ResultadoInformacionAlumno.ProgramaGeneralCurso = null;
                            ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                            ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                            ResultadoInformacionAlumno.Capitulo = null;
                            ResultadoInformacionAlumno.Sesion = null;
                        }
                        if (idAlumno != 0)
                        {
                            var obtenerNombreAlumno = repInformacionAlumnoChat.obtenerNombreAlumnoChatSoporte(idAlumno);
                            ResultadoInformacionAlumno.NombreAlumno = obtenerNombreAlumno.NombreAlumno;
                        }
                    }
                   
                }


                return Ok(ResultadoInformacionAlumno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerTodosValoresAlumnoChatSoporte(int idAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext integraDBContext = new integraDBContext();
                AsesorChatDetalleRepositorio repAsesorChatDetalle = new AsesorChatDetalleRepositorio(integraDBContext);
                InformacionAlumnoChatRepositorio repInformacionAlumnoChat = new InformacionAlumnoChatRepositorio(integraDBContext);
                InformacionAlumnoChatLogRepositorio repInformacionAlumnoChatLog = new InformacionAlumnoChatLogRepositorio(integraDBContext);
                PgeneralRepositorio reppgeneral = new PgeneralRepositorio();
                InformacionAlumnoChatBO avance = new InformacionAlumnoChatBO();
                InformacionAlumnoChatLogBO avanceLog = new InformacionAlumnoChatLogBO();
                InformacionChatSoporteAlumnoDTO ResultadoInformacionAlumno = new InformacionChatSoporteAlumnoDTO();                
                
                var informacionAlumno = repInformacionAlumnoChat.FirstBy(x => x.IdAlumno == idAlumno); //Recupero el registro de la tabla padre segun el idalumno                   
                var obtenerNombreAlumno = repInformacionAlumnoChat.obtenerNombreAlumnoChatSoporte(idAlumno);
                var obtenerUltimaInformacionAlumno = repInformacionAlumnoChat.obtenerUltimaInformacionAlumno(idAlumno);
                var logInformacion = repInformacionAlumnoChat.obtenerLogInformacion(idAlumno);
                if (logInformacion == null)
                {
                    if (idAlumno != 0)
                    {                        
                        ResultadoInformacionAlumno.NombreAlumno = obtenerNombreAlumno.NombreAlumno;
                        ResultadoInformacionAlumno.Celular = obtenerNombreAlumno.Celular;
                        ResultadoInformacionAlumno.Correo = obtenerNombreAlumno.Correo;
                    }
                    ResultadoInformacionAlumno.CodigoMatricula = null;
                    ResultadoInformacionAlumno.ProgramaGeneralSoporte = null;
                    ResultadoInformacionAlumno.ProgramaGeneralCurso = null;
                    ResultadoInformacionAlumno.CentroCosto = null;
                    ResultadoInformacionAlumno.Coordinadora = null;
                    ResultadoInformacionAlumno.Capitulo = null;
                    ResultadoInformacionAlumno.Sesion = null;
                }
                else
                {
                    if (logInformacion.IdProgramaGeneral_Hijo != 0 && logInformacion.IdSesion == 0)
                    {
                        var pgeneralCurso = reppgeneral.FirstById(logInformacion.IdProgramaGeneral_Hijo);
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                        ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = null;
                        ResultadoInformacionAlumno.Sesion = null;
                        ResultadoInformacionAlumno.Celular = obtenerNombreAlumno.Celular;
                        ResultadoInformacionAlumno.Correo = obtenerNombreAlumno.Correo;
                    }
                    if (logInformacion.IdProgramaGeneral_Hijo != 0 && logInformacion.IdSesion != 0)
                    {
                        var pgeneralCurso = reppgeneral.FirstById(logInformacion.IdProgramaGeneral_Hijo);
                        var obtenerCapitulo = repInformacionAlumnoChat.obtenerCapituloAlumnoChatSoporte(logInformacion.IdProgramaGeneral_Hijo, logInformacion.IdSesion);
                        var obtenerSesion = repInformacionAlumnoChat.obtenerSesionAlumnoChatSoporte(logInformacion.IdProgramaGeneral_Hijo, logInformacion.IdSesion);
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = pgeneralCurso == null ? null : pgeneralCurso.Nombre;
                        ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = obtenerCapitulo.Capitulo;
                        ResultadoInformacionAlumno.Sesion = obtenerSesion.Sesion;
                        ResultadoInformacionAlumno.Celular = obtenerNombreAlumno.Celular;
                        ResultadoInformacionAlumno.Correo = obtenerNombreAlumno.Correo;
                    }
                    if (logInformacion.IdProgramaGeneral_Hijo == 0 && logInformacion.IdSesion == 0)
                    {
                        ResultadoInformacionAlumno.CodigoMatricula = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CodigoMatricula;
                        ResultadoInformacionAlumno.ProgramaGeneralSoporte = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.ProgramaGeneralSoporte;
                        ResultadoInformacionAlumno.ProgramaGeneralCurso = null;
                        ResultadoInformacionAlumno.CentroCosto = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.CentroCosto;
                        ResultadoInformacionAlumno.Coordinadora = obtenerUltimaInformacionAlumno == null ? null : obtenerUltimaInformacionAlumno.Coordinadora;
                        ResultadoInformacionAlumno.Capitulo = null;
                        ResultadoInformacionAlumno.Sesion = null;
                        ResultadoInformacionAlumno.Celular = obtenerNombreAlumno.Celular;
                        ResultadoInformacionAlumno.Correo = obtenerNombreAlumno.Correo;
                    }
                    if (idAlumno != 0)
                    {                        
                        ResultadoInformacionAlumno.NombreAlumno = obtenerNombreAlumno.NombreAlumno;
                    }
                }

                


            return Ok(ResultadoInformacionAlumno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
