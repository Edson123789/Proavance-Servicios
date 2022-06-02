using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Clases;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/HistorialCambioAlumno")]
    public class HistorialCambioAlumnoOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltrado(string TextoBuscar)
        {
            try
            {
                RaHistorialCambioAlumnoRepositorio _repRaHistorialCambioAlumno = new RaHistorialCambioAlumnoRepositorio();
                return Ok(_repRaHistorialCambioAlumno.ListadoCentroCostoConAsistenciaMensual(TextoBuscar));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            try
            {
                RaHistorialCambioAlumnoRepositorio _repRaHistorialCambioAlumno = new RaHistorialCambioAlumnoRepositorio();
                RaHistorialCambioAlumnoTipoRepositorio _repRaHistorialTipoCambioAlumno = new RaHistorialCambioAlumnoTipoRepositorio();

                if (!_repRaHistorialCambioAlumno.Exist(Id))
                {
                    return BadRequest("Registro no existente.");
                }
                var objeto = _repRaHistorialCambioAlumno.FirstById(Id);
                var detalle = new DetalleHistorialCambioAlumnoDTO()
                {
                    Id = objeto.Id,
                    CentroCostoOrigen = objeto.CentroCostoOrigen,
                    CentroCostoDestino = objeto.CentroCostoDestino,
                    Aprobado = objeto.Aprobado,
                    Cancelado = objeto.Cancelado,
                    CodigoAlumno = objeto.CodigoAlumno,
                    UsuarioAprobacion = objeto.UsuarioAprobacion,
                    ComentarioSolicitud = objeto.ComentarioSolicitud,
                    FechaCreacion = objeto.FechaCreacion,
                    FechaModificacion = objeto.FechaModificacion,
                    NombreHistorialCambioAlumnoTipo = _repRaHistorialTipoCambioAlumno.FirstById(objeto.IdRaHistorialCambioAlumnoTipo).Nombre,
                    UsuarioCreacion = objeto.UsuarioCreacion,
                    UsuarioModificacion = objeto.UsuarioModificacion,
                    UsuarioSolicitud = objeto.UsuarioSolicitud,
                };
                return Ok(detalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] HistorialCambioAlumnoDTO HistorialCambioAlumno)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                RaHistorialCambioAlumnoRepositorio _repRaHistorialCambioAlumno = new RaHistorialCambioAlumnoRepositorio();
                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                RaAlumnoRepositorio _repRaAlumno = new RaAlumnoRepositorio();

                var coordinadoraSolicitante = _repCoordinadora.ObtenerCoordinador(HistorialCambioAlumno.NombreUsuario);
                if (coordinadoraSolicitante == null || string.IsNullOrEmpty(coordinadoraSolicitante.Password))
                {
                    return BadRequest("Su usuario no tiene registrada información para enviar correos, no se registró la solicitud.");
                }

                if (!_repRaCentroCosto.ExistePorNombre(HistorialCambioAlumno.CentroCostoDestino))
                {
                    return BadRequest("El Centro de Costo de Destino no existe en Integra.");
                }

                if (_repRaHistorialCambioAlumno.ValidarRegistroSolictud(HistorialCambioAlumno.CodigoAlumno))
                {
                    RaHistorialCambioAlumnoBO raHistorialCambioAlumnoBO = new RaHistorialCambioAlumnoBO()
                    {
                        CodigoAlumno = HistorialCambioAlumno.CodigoAlumno,
                        IdRaHistorialCambioAlumnoTipo = HistorialCambioAlumno.IdRaHistorialCambioAlumnoTipo,
                        CentroCostoOrigen = HistorialCambioAlumno.CentroCostoOrigen,
                        CentroCostoDestino = HistorialCambioAlumno.CentroCostoDestino,
                        Cancelado = HistorialCambioAlumno.Cancelado,
                        ComentarioSolicitud = HistorialCambioAlumno.ComentarioSolicitud,
                        UsuarioSolicitud = HistorialCambioAlumno.NombreUsuario,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = HistorialCambioAlumno.NombreUsuario,
                        UsuarioModificacion = HistorialCambioAlumno.NombreUsuario
                    };
                    if (!raHistorialCambioAlumnoBO.HasErrors)
                    {
                        _repRaHistorialCambioAlumno.Insert(raHistorialCambioAlumnoBO);
                        //Enviar correos
                        var alumno = _repRaAlumno.ObtenerAlumnoMatriculadoPorCodigoMatricula(HistorialCambioAlumno.CodigoAlumno);
                        var solicitud = _repRaHistorialCambioAlumno.ObtenerHistorialCambioAlumno(raHistorialCambioAlumnoBO.Id);
                        EmailCoordinador email = new EmailCoordinador();
                        email.EnviarHistorialCambioAlumnoRegistro(solicitud, alumno, coordinadoraSolicitante);
                    }
                    else
                    {
                        return BadRequest(raHistorialCambioAlumnoBO.ActualesErrores);
                    }
                    return Ok(raHistorialCambioAlumnoBO);
                }
                else {
                    return BadRequest("No se puede tener más de 1 Solicitud de Cambio en proceso.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromBody] AprobarHistorialCambioDTO AprobarHistorialCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaHistorialCambioAlumnoRepositorio _repRaHistorialCambioAlumno = new RaHistorialCambioAlumnoRepositorio();

                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                RaAlumnoRepositorio _repRaAlumno = new RaAlumnoRepositorio();
                if (_repRaHistorialCambioAlumno.Exist(AprobarHistorialCambio.Id))
                {
                    //aprobar
                    var historialCambioAlumnoBO = _repRaHistorialCambioAlumno.FirstById(AprobarHistorialCambio.Id);
                    if (!(historialCambioAlumnoBO.Cancelado && historialCambioAlumnoBO.Aprobado != null))
                    {
                        historialCambioAlumnoBO.Aprobado = AprobarHistorialCambio.Aprobar;
                        historialCambioAlumnoBO.UsuarioAprobacion = AprobarHistorialCambio.NombreUsuario;
                        historialCambioAlumnoBO.UsuarioModificacion = AprobarHistorialCambio.NombreUsuario;
                        historialCambioAlumnoBO.FechaModificacion = DateTime.Now;

                        if (historialCambioAlumnoBO.HasErrors)
                        {
                            return BadRequest(historialCambioAlumnoBO.GetErrors(null));
                        }
                        else
                        {
                            _repRaHistorialCambioAlumno.Update(historialCambioAlumnoBO);
                            //correo
                            //var coordinadoraSolicitante = _repCoordinadora.ObtenerCoordinador(AprobarHistorialCambio.NombreUsuario);

                            //var alumno = _repRaAlumno.ObtenerAlumnoMatriculadoPorCodigoMatricula(historialCambioAlumnoBO.CodigoAlumno);
                            //var solicitud = _repRaHistorialCambioAlumno.ObtenerHistorialCambioAlumno(AprobarHistorialCambio.Id);
                            //EmailCoordinador email = new EmailCoordinador();
                            //email.EnviarHistorialCambioAlumnoAprobar(solicitud, alumno, coordinadoraSolicitante);
                            return Ok(historialCambioAlumnoBO);
                        }
                    }
                    else
                    {
                        return BadRequest("El registro ya fue Cancelado y/o ya se aprobó/desaprobó, no se puede Aprobar.");
                    }
                }
                else
                {
                    return BadRequest("El registro no existe!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult Cancelar([FromBody] CancelarHistoricoCambioAlumnoDTO CancelarHistorialCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RaHistorialCambioAlumnoRepositorio _repRaHistorialCambioAlumno = new RaHistorialCambioAlumnoRepositorio();

                RaCentroCostoRepositorio _repRaCentroCosto = new RaCentroCostoRepositorio();
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                RaAlumnoRepositorio _repRaAlumno = new RaAlumnoRepositorio();
                if (_repRaHistorialCambioAlumno.Exist(CancelarHistorialCambio.Id))
                {
                    //aprobar
                    var historialCambioAlumnoBO = _repRaHistorialCambioAlumno.FirstById(CancelarHistorialCambio.Id);
                    if (!(historialCambioAlumnoBO.Cancelado && historialCambioAlumnoBO.Aprobado != null))
                    {
                        historialCambioAlumnoBO.Cancelado = true;
                        historialCambioAlumnoBO.UsuarioModificacion = CancelarHistorialCambio.NombreUsuario;
                        historialCambioAlumnoBO.FechaModificacion = DateTime.Now;

                        if (historialCambioAlumnoBO.HasErrors)
                        {
                            return BadRequest(historialCambioAlumnoBO.GetErrors(null));
                        }
                        else
                        {
                            _repRaHistorialCambioAlumno.Update(historialCambioAlumnoBO);
                            //correo
                            //var coordinadoraSolicitante = _repCoordinadora.ObtenerCoordinador(CancelarHistorialCambio.NombreUsuario);

                            //var alumno = _repRaAlumno.ObtenerAlumnoMatriculadoPorCodigoMatricula(historialCambioAlumnoBO.CodigoAlumno);
                            //var solicitud = _repRaHistorialCambioAlumno.ObtenerHistorialCambioAlumno(CancelarHistorialCambio.Id);
                            //EmailCoordinador email = new EmailCoordinador();
                            //email.EnviarHistorialCambioAlumnoCancelar(solicitud, alumno, coordinadoraSolicitante);
                            return Ok(historialCambioAlumnoBO);
                        }
                    }
                    else
                    {
                        return BadRequest("El registro ya fue Cancelado y/o ya se aprobó/desaprobó, no se puede Aprobar.");
                    }
                }
                else
                {
                    return BadRequest("El registro no existe!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}