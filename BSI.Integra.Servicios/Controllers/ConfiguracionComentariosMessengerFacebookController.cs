using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using System.Transactions;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionComentariosMessengerFacebook")]//Hace referencia el BO "MessengerConfiguracionChat"
    public class ConfiguracionComentariosMessengerFacebookController : ControllerBase
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsesor()
        {
            try
            {
                PersonalRepositorio personalRepositorio = new PersonalRepositorio();
                return Ok(personalRepositorio.ObtenerAsesoresVentas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaCapacitacionFacebook()
        {
            try
            {
                AreaCapacitacionFacebookRepositorio areaCapacitacionFacebookRepositorio = new AreaCapacitacionFacebookRepositorio();
                return Ok(areaCapacitacionFacebookRepositorio.ObtenerAreas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAreaCapacitacion()
        {
            try
            {
                AreaCapacitacionRepositorio areaCapacitacionRepositorio = new AreaCapacitacionRepositorio();
                return Ok(areaCapacitacionRepositorio.ObtenerAreaCapacitacionFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSubAreaCapacitacion()
        {
            try
            {
                SubAreaCapacitacionRepositorio subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio();
                return Ok(subAreaCapacitacionRepositorio.ObtenerSubAreasParaFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdMessengerAsesor}")]
        [HttpGet]
        public ActionResult ObtenerAreasPorMessengerAsesor(int IdMessengerAsesor)
        {
            try
            {
                MessengerAsesorDetalleRepositorio messengerAsesorDetalleRepositorio = new MessengerAsesorDetalleRepositorio();
                return Ok(messengerAsesorDetalleRepositorio.ObtenerAreasPorMessengerAsesor(IdMessengerAsesor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPanel()
        {
            try
            {
                MessengerAsesorRepositorio messengerAsesorRepositorio = new MessengerAsesorRepositorio();
                return Ok(messengerAsesorRepositorio.ObtenerPanel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MessengerAsesorDTO messengerAsesorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext context = new integraDBContext();
                MessengerAsesorRepositorio messengerAsesorRepositorio = new MessengerAsesorRepositorio(context);
                MessengerAsesorDetalleRepositorio messengerAsesorDetalleRepositorio = new MessengerAsesorDetalleRepositorio(context);
                MessengerHistorialAsesorRepositorio messengerHistorialAsesorRepositorio = new MessengerHistorialAsesorRepositorio(context);

                if (messengerAsesorRepositorio.Exist(x => x.IdPersonal == messengerAsesorDTO.IdPersonal))
                {
                    return BadRequest("El Asesor ya tiene Configuración.");
                }

                MessengerAsesorBO messengerAsesorBO = new MessengerAsesorBO();
                messengerAsesorBO.IdPersonal = messengerAsesorDTO.IdPersonal;
                messengerAsesorBO.ConteoClientesAsignados = 0;
                messengerAsesorBO.Estado = true;
                messengerAsesorBO.FechaCreacion = DateTime.Now;
                messengerAsesorBO.FechaModificacion = DateTime.Now;
                messengerAsesorBO.UsuarioCreacion = messengerAsesorDTO.Usuario;
                messengerAsesorBO.UsuarioModificacion = messengerAsesorDTO.Usuario;

                if (messengerAsesorBO.HasErrors) return BadRequest(messengerAsesorBO.GetErrors(null));

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        messengerAsesorRepositorio.Insert(messengerAsesorBO);
                        MessengerAsesorDetalleBO messengerAsesorDetalleBO;
                        MessengerHistorialAsesorBO messengerHistorialAsesorBO;
                        foreach (int idarea in messengerAsesorDTO.IdAreaCapacitacionFacebook)
                        {
                            messengerAsesorDetalleBO = new MessengerAsesorDetalleBO();
                            messengerAsesorDetalleBO.IdMessengerAsesor = messengerAsesorBO.Id;
                            messengerAsesorDetalleBO.IdPersonal = messengerAsesorDTO.IdPersonal;
                            messengerAsesorDetalleBO.IdAreaCapacitacionFacebook = idarea;
                            messengerAsesorDetalleBO.Estado = true;
                            messengerAsesorDetalleBO.FechaCreacion = DateTime.Now;
                            messengerAsesorDetalleBO.FechaModificacion = DateTime.Now;
                            messengerAsesorDetalleBO.UsuarioCreacion = messengerAsesorDTO.Usuario;
                            messengerAsesorDetalleBO.UsuarioModificacion = messengerAsesorDTO.Usuario;

                            if (messengerAsesorDetalleBO.HasErrors)
                            {
                                scope.Dispose();
                                return BadRequest(messengerAsesorDetalleBO.GetErrors(null));
                            }

                            messengerAsesorDetalleRepositorio.Insert(messengerAsesorDetalleBO);

                            messengerHistorialAsesorBO = new MessengerHistorialAsesorBO();
                            messengerHistorialAsesorBO.IdMessengerAsesorDetalle = messengerAsesorDetalleBO.Id;
                            messengerHistorialAsesorBO.IdMessengerAsesor = messengerAsesorBO.Id;
                            messengerHistorialAsesorBO.Fecha = DateTime.Now;
                            messengerHistorialAsesorBO.IdPersonal = messengerAsesorDTO.IdPersonal;
                            messengerHistorialAsesorBO.Estado = true;
                            messengerHistorialAsesorBO.FechaCreacion = DateTime.Now;
                            messengerHistorialAsesorBO.FechaModificacion = DateTime.Now;
                            messengerHistorialAsesorBO.UsuarioCreacion = messengerAsesorDTO.Usuario;
                            messengerHistorialAsesorBO.UsuarioModificacion = messengerAsesorDTO.Usuario;

                            if (messengerHistorialAsesorBO.HasErrors)
                            {
                                scope.Dispose();
                                return BadRequest(messengerHistorialAsesorBO.GetErrors(null));
                            }

                            messengerHistorialAsesorRepositorio.Insert(messengerHistorialAsesorBO);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MessengerAsesorDTO messengerAsesorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext context = new integraDBContext();
                MessengerAsesorRepositorio messengerAsesorRepositorio = new MessengerAsesorRepositorio(context);
                MessengerAsesorDetalleRepositorio messengerAsesorDetalleRepositorio = new MessengerAsesorDetalleRepositorio(context);
                MessengerHistorialAsesorRepositorio messengerHistorialAsesorRepositorio = new MessengerHistorialAsesorRepositorio(context);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        var listaDetalle = messengerAsesorDetalleRepositorio.GetBy(x => x.IdMessengerAsesor == messengerAsesorDTO.Id);

                        foreach (var detalle in listaDetalle)
                        {
                            if (!messengerAsesorDTO.IdAreaCapacitacionFacebook.Contains(detalle.IdAreaCapacitacionFacebook))
                                messengerAsesorDetalleRepositorio.Delete(detalle.Id, messengerAsesorDTO.Usuario);
                        }

                        MessengerAsesorDetalleBO messengerAsesorDetalleBO;
                        MessengerHistorialAsesorBO messengerHistorialAsesorBO;

                        foreach (int idarea in messengerAsesorDTO.IdAreaCapacitacionFacebook)
                        {
                            if(messengerAsesorDetalleRepositorio.FirstBy(x => x.IdMessengerAsesor == messengerAsesorDTO.Id && x.IdAreaCapacitacionFacebook == idarea) == null)
                            {
                                messengerAsesorDetalleBO = new MessengerAsesorDetalleBO();
                                messengerAsesorDetalleBO.IdMessengerAsesor = messengerAsesorDTO.Id;
                                messengerAsesorDetalleBO.IdPersonal = messengerAsesorDTO.IdPersonal;
                                messengerAsesorDetalleBO.IdAreaCapacitacionFacebook = idarea;
                                messengerAsesorDetalleBO.Estado = true;
                                messengerAsesorDetalleBO.FechaCreacion = DateTime.Now;
                                messengerAsesorDetalleBO.FechaModificacion = DateTime.Now;
                                messengerAsesorDetalleBO.UsuarioCreacion = messengerAsesorDTO.Usuario;
                                messengerAsesorDetalleBO.UsuarioModificacion = messengerAsesorDTO.Usuario;

                                if (messengerAsesorDetalleBO.HasErrors)
                                {
                                    scope.Dispose();
                                    return BadRequest(messengerAsesorDetalleBO.GetErrors(null));
                                }

                                messengerAsesorDetalleRepositorio.Insert(messengerAsesorDetalleBO);

                                messengerHistorialAsesorBO = new MessengerHistorialAsesorBO();
                                messengerHistorialAsesorBO.IdMessengerAsesorDetalle = messengerAsesorDetalleBO.Id;
                                messengerHistorialAsesorBO.IdMessengerAsesor = messengerAsesorDTO.Id;
                                messengerHistorialAsesorBO.Fecha = DateTime.Now;
                                messengerHistorialAsesorBO.IdPersonal = messengerAsesorDTO.IdPersonal;
                                messengerHistorialAsesorBO.Estado = true;
                                messengerHistorialAsesorBO.FechaCreacion = DateTime.Now;
                                messengerHistorialAsesorBO.FechaModificacion = DateTime.Now;
                                messengerHistorialAsesorBO.UsuarioCreacion = messengerAsesorDTO.Usuario;
                                messengerHistorialAsesorBO.UsuarioModificacion = messengerAsesorDTO.Usuario;

                                if (messengerHistorialAsesorBO.HasErrors)
                                {
                                    scope.Dispose();
                                    return BadRequest(messengerHistorialAsesorBO.GetErrors(null));
                                }

                                messengerHistorialAsesorRepositorio.Insert(messengerHistorialAsesorBO);
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
    }

}
